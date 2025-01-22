namespace Clean.Architecture.Template.Core.Auth
{
    public interface IUserInfo
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
        public int? UserId { get; set; }
        public string Role { get; set; }
        public int TimeStamp { get; set; }
    }
}