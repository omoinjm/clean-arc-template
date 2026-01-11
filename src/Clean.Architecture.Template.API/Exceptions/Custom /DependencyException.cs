using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Architecture.Template.API.Exceptions.Custom
{
    public class DependencyException(string message) : Exception(message) { }
}
