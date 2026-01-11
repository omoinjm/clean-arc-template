namespace Clean.Architecture.Template.Application.Response.Auth
{
    public class AuthUserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime? ExpiryDateTime { get; set; }
        public int? UserId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}