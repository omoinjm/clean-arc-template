using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.TestCommon.TestConstants;

namespace Clean.Architecture.Template.TestCommon.Factories
{
    public static class UserFactory
    {
        public static UserEntity CreateUser(
            int id = -1,
            string name = "",
            string surname = "",
            string email = "")
        {
            return new UserEntity
            {
                Id = id == -1 ? Constants.User.Id : id,
                Name = string.IsNullOrEmpty(name) ? Constants.User.Name : name,
                Surname = string.IsNullOrEmpty(surname) ? Constants.User.Surname : surname,
                Email = string.IsNullOrEmpty(email) ? Constants.User.Email : email,
                Username = Constants.User.Username,
                Password = Constants.User.Password,
                Salt = Constants.User.Salt,
                ChangePassword = false,
                Deleted = false,
                CreatedBy = "admin",
                UpdatedAt = null,
                DeletedAt = null,
                IdNumber = Constants.User.IdNumber,
                PhoneNumber = Constants.User.PhoneNumber,
                Role = Constants.User.Role,
                ForgotPasswordGuid = null,
                Otp = null,
                IsActive = Constants.User.IsActive,
                LoginDate = null,
                UserStatusId = 1,
                UserStatusName = "Active",
                UserStatusSymbol = "A",
                UserStatusColor = "Green",
                Token = Constants.User.Token,
                ExpiryDateTime = null,
                LoginTimeStamp = null
            };
        }
    }
}
