using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Architecture.Template.API.Exceptions.CustomException
{
    public class ErrorTitleProvider
    {
        private static readonly Dictionary<Type, string> ExceptionTitles = new()
        {
            { typeof(ArgumentNullException), "Argument cannot be null" },
            { typeof(ArgumentOutOfRangeException), "Argument out of range" },
            { typeof(NullReferenceException), "Object reference not set to an instance" },
            { typeof(IndexOutOfRangeException), "Index out of range" },
            { typeof(InvalidOperationException), "Invalid operation" },
            { typeof(NotSupportedException), "Operation not supported" },
            { typeof(TimeoutException), "Operation timed out" },
            { typeof(DbException), "Database operation failed" },
            { typeof(IOException), "IO operation failed" },
            { typeof(FormatException), "Invalid format" },
            { typeof(AggregateException), "One or more errors occurred" },
            { typeof(ArgumentException), "Invalid argument" },
            // Add more exception types and titles as needed
        };

        public static string GetErrorTitle(Exception exception)
        {
            var exceptionType = exception.GetType();

            if (ExceptionTitles.ContainsKey(exceptionType))
            {
                return ExceptionTitles[exceptionType];
            }

            // Default title if exception type is not found
            return "An Error Occurred";
        }
    }
}
