using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Architecture.Template.API.Exceptions.Custom;

namespace Clean.Architecture.Template.API.Helpers
{
    public static class ExtensionMethods
    {
        public static int ToStatusCode(this Exception ex)
        {
            return ex switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ArgumentException => StatusCodes.Status400BadRequest,
                DependencyException => StatusCodes.Status424FailedDependency,
                RequestTimeoutException => StatusCodes.Status408RequestTimeout, // Handle 408
                ResourceCreatedException => StatusCodes.Status201Created, // Custom exception for created resources
                AsyncOperationException => StatusCodes.Status202Accepted, // Custom exception for accepted operations
                NotImplementedException => StatusCodes.Status501NotImplemented, // Handle Not Implemented
                _ => StatusCodes.Status500InternalServerError // Default: Internal Server Error
            };
        }
    }
}
