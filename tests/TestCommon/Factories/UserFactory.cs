using Clean.Architecture.Template.Core.Entity;

namespace Clean.Architecture.Template.TestCommon.Factories
{
    public static class UserFactory
    {
        public static UserEntity CreateUser(
            int id = 1,
            string name = "John",
            string surname = "Doe",
            string email = "john.doe@example.com")
        {
            return new UserEntity
            {
                Id = id,
                Name = name,
                Surname = surname,
                Email = email,
                Username = "johndoe",
                Password = "password",
                Salt = "salt",
                ChangePassword = false,
                Deleted = false,
                CreatedBy = "admin",
                UpdatedAt = null,
                DeletedAt = null,
                IdNumber = "123456789",
                PhoneNumber = "1234567890",
                Role = "User",
                ForgotPasswordGuid = null,
                Otp = null,
                IsActive = true,
                LoginDate = null,
                UserStatusId = 1,
                UserStatusName = "Active",
                UserStatusSymbol = "A",
                UserStatusColor = "Green",
                Token = "token",
                ExpiryDateTime = null,
                LoginTimeStamp = null
            };
        }
    }
}
