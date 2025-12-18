using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Clean.Architecture.Template.API.dto;
using Clean.Architecture.Template.API.Exceptions;
using Clean.Architecture.Template.Application.Configuration;
using Clean.Architecture.Template.Application.Handlers.Users;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Services;
using Clean.Architecture.Template.Infrastructure.Repository;
using Clean.Architecture.Template.Infrastructure.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Clean.Architecture.Template.API
{
    public class Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        public IConfiguration Configuration = configuration;
        private readonly IWebHostEnvironment _env = env;

        public void ConfigureServices(IServiceCollection services)
        {
            // Add compression services
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.Providers.Add<BrotliCompressionProvider>();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            Environment.GetEnvironmentVariable("SECRET_KEY") ?? Configuration["Values:SECRET_KEY"]!
                            )
                        )
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddSwaggerGen(setup =>
           {
               // Include 'SecurityScheme' to use JWT Authentication
               var jwtSecurityScheme = new OpenApiSecurityScheme
               {
                   BearerFormat = "JWT",
                   Name = "JWT Authentication",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.Http,
                   Scheme = JwtBearerDefaults.AuthenticationScheme,
                   Description = "Place your JWT Bearer token in the Text-Box below.",

                   Reference = new OpenApiReference
                   {
                       Id = JwtBearerDefaults.AuthenticationScheme,
                       Type = ReferenceType.SecurityScheme
                   }
               };

               setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

               setup.AddSecurityRequirement(new OpenApiSecurityRequirement
               {
                { jwtSecurityScheme, Array.Empty<string>() }
               });

           });

            services.AddCors(options =>
             {
                 if (env.IsDevelopment())
                 {
                     options.AddPolicy("ApiCorsPolicy", builder =>
                     {
                         builder.WithOrigins(
                                 "http://localhost:3000",
                                 "http://localhost:4200",
                                 "http://localhost:4201",
                                 "https://staging.app.wallety.cash"
                             )
                             .AllowAnyMethod()
                             .AllowAnyHeader()
                             .AllowCredentials();
                     });
                 }
                 else
                 {
                     options.AddPolicy("ApiCorsPolicy", builder =>
                     {
                         builder.WithOrigins(
                                 "https://app.wallety.cash"
                             )
                             .WithMethods("GET", "POST", "PUT", "DELETE")
                             .WithHeaders("Authorization", "Content-Type")
                             .AllowCredentials();
                     });

                 }
             });

            services.AddMemoryCache();

            // JSON serialization (optimized for all environments)
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.WriteIndented = _env.IsDevelopment(); // Pretty print only in dev
                });

            services.AddApiVersioning();

            services.AddHealthChecks()
                .AddNpgSql(
                    connectionString: Environment.GetEnvironmentVariable("PGSQL_CONNECTION_STRING")
                        ?? configuration.GetConnectionString("PGSQL_CONNECTION_STRING")!,
                    healthQuery: "SELECT 1;",
                    name: "postgresql",
                    tags: ["db", "pgsql"]
                );


            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clean Architecture Template API", Version = "v1" }); });

            //DI
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<ApplicationLogs>>();

            services.AddSingleton(typeof(ILogger), logger);
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            // JM: Register the global exception handler
            services.AddSingleton<IExceptionHandler, GlobalExceptionHandler>();

            // Add Azure Repository Service
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetUsersHandler).Assembly));

            //Repositories
            services.AddScoped<ILookupRepository, DBRepository>();
            services.AddScoped<IMenuRepository, DBRepository>();
            services.AddScoped<IUserRepository, DBRepository>();

            // Component Interfaces
            services.AddScoped<IPgSqlSelector>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();

                var conString = Environment.GetEnvironmentVariable("PGSQL_CONNECTION_STRING")
                    ?? configuration.GetConnectionString("PGSQL_CONNECTION_STRING");

                return new PgSqlSelector(conString!);
            });

            //service cache
            services.AddScoped<ICachingInMemoryService, CachingInMemoryService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

            services.AddSingleton<IConfigurationService, ConfigurationService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
            }

            // This setup allows for centralized handling of exceptions in an ASP.NET Core application,
            // ensuring that all unhandled exceptions are captured and processed according to the application's error handling logic.
            app.UseExceptionHandler((Action<IApplicationBuilder>)(errorApp =>
            {
                errorApp.Run((RequestDelegate)(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandlerFeature?.Error;

                    if (exception != null)
                    {
                        var handler = context.RequestServices.GetRequiredService<IExceptionHandler>();
                        await handler.TryHandleAsync(context, exception, context.RequestAborted);
                    }
                }));
            }));

            app.UseHsts();

            app.UseResponseCompression();

            app.UseAuthentication();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseCors("ApiCorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
