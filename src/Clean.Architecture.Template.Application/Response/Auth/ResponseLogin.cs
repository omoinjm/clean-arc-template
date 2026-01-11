using System.Text.Json.Serialization;
using Clean.Architecture.Template.Core.Auth;

namespace Clean.Architecture.Template.Application.Response.Auth
{
    public class ResponseLogin : BaseResponse, IUserInfo
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireDate { get; set; }

        [JsonIgnore]
        public int? UserId { get; set; }

        [JsonIgnore]
        public string Role { get; set; } = string.Empty;

        public int TimeStamp { get; set; }
        public bool IsLoggedIn { get; set; }
        public string LoginMessage { get; set; } = string.Empty;
    }
}