# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Enable compression and optimize build
ENV ASPNETCORE_RESPONSECOMPRESSION_ENABLED=true
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_NOLOGO=true
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

# Copy only project files first for better layer caching
COPY ["./src/Clean.Architecture.Template.API/Clean.Architecture.Template.API.csproj", "./"]
COPY ["./src/Clean.Architecture.Template.Application/Clean.Architecture.Template.Application.csproj", "./"]
COPY ["./src/Clean.Architecture.Template.Core/Clean.Architecture.Template.Core.csproj", "./"]
COPY ["./src/Clean.Architecture.Template.Infrastructure/Clean.Architecture.Template.Infrastructure.csproj", "./"]

RUN dotnet restore "./Clean.Architecture.Template.API.csproj"

# Copy remaining files and build
COPY . .
RUN dotnet build "./src/Clean.Architecture.Template.API/Clean.Architecture.Template.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish Stage
# Publish with trimming and ready-to-run
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./src/Clean.Architecture.Template.API/Clean.Architecture.Template.API.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish 
   #  /p:UseAppHost=false \
   #  /p:PublishTrimmed=true \
   #  /p:EnableCompressionInSingleFile=true

# Final stage with minimal runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Configure compression and performance
ENV ASPNETCORE_RESPONSECOMPRESSION_ENABLED=true
ENV DOTNET_SYSTEM_NET_HTTP_SOCKETSHTTPHANDLER_HTTP2SUPPORT=true
ENV ASPNETCORE_ENVIRONMENT=Development

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Clean.Architecture.Template.API.dll"]
