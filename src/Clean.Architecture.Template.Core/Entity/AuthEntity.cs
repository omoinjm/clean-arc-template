using System.Text.Json.Serialization;

namespace Clean.Architecture.Template.Core.Entity
{
    /// <summary>
    /// Represents an entity that requires authorization
    /// </summary>
    public abstract class AuthEntity
    {
        [JsonIgnore]
        public string? AuthToken { get; set; }
    }
}