namespace Clean.Architecture.Template.Core.Entity
{
    public class UserEntity
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }
        public string? Salt { get; set; }

        public bool? ChangePassword { get; set; }
        public bool? Deleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? SyncedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? IdNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }

        public string? ForgotPasswordGuid { get; set; }
        public string? Otp { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? LoginDate { get; set; }

        public int UserStatusId { get; set; }
        public string? UserStatusName { get; set; }
        public string? UserStatusSymbol { get; set; }
        public string? UserStatusColor { get; set; }

        public string? Token { get; set; }
        public DateTime? ExpiryDateTime { get; set; }
        public DateTime? LoginTimeStamp { get; set; }
    }
}