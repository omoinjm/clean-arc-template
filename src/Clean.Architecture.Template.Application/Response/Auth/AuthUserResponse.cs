namespace Clean.Architecture.Template.Application.Response.Auth
{
    public class AuthUserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool? IsActive { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiryDateTime { get; set; }
        public int? UserId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Role { get; set; }
    }
}