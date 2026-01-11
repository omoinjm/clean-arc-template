using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Architecture.Template.API.Exceptions.CustomException
{
    public class ApiCustomException : Exception
    {
        public string? AdditionalInfo { get; set; }
        public string? Type { get; set; }
        public string? Detail { get; set; }
        public string? Title { get; set; }
        public string? Instance { get; set; }
    }
}
