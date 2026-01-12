namespace Clean.Architecture.Template.TestCommon.TestConstants;

/// <summary>
/// Centralized constants used across all tests to ensure consistency and reduce duplication.
/// </summary>
public static class Constants
{
    public static class User
    {
        public const int Id = 1;
        public const string Name = "John";
        public const string Surname = "Doe";
        public const string Email = "john.doe@example.com";
        public const string Username = "johndoe";
        public const string Password = "password";
        public const string Salt = "salt";
        public const string IdNumber = "123456789";
        public const string PhoneNumber = "1234567890";
        public const string Role = "User";
        public const string Token = "token";
        public const bool IsActive = true;
    }
}
