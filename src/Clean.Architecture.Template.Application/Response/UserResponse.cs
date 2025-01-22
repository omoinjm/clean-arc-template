using System.Text.Json.Serialization;

namespace Clean.Architecture.Template.Application.Response
{
    public class UserResponse
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }

        public string? DisplayName => $"{Name} {Surname}";

        [JsonIgnore]
        public string? Password { get; set; }
        [JsonIgnore]
        public string? Salt { get; set; }

        public int? ChangePassword { get; set; }
        public int? Deleted { get; set; }
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

        [JsonIgnore]
        public string? ForgotPasswordGuid { get; set; }
        [JsonIgnore]
        public string? Otp { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? LoginDate { get; set; }

        public int UserStatusId { get; set; }
        public string? UserStatusName { get; set; }
        public string? UserStatusSymbol { get; set; }
        public string? UserStatusColor { get; set; }
    }
}