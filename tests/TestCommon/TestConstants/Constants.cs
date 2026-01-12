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
        public const string Password = "Password123!";
        public const string Salt = "salt-value-123";
        public const string IdNumber = "123456789";
        public const string PhoneNumber = "1234567890";
        public const string Role = "User";
        public const string Token = "jwt-token-sample";
        public const bool IsActive = true;
    }

    public static class Menu
    {
        public const int ModuleId = 1;
        public const int ModuleItemId = 1;
        public const int SortOrder = 1;
        public const int ModuleSortOrder = 1;
        public const string DisplayText = "Dashboard";
        public const string Icon = "dashboard-icon";
        public const string ModuleDisplayText = "System";
        public const string ModuleIcon = "system-icon";
        public const string RouterLink = "/dashboard";
        public const string ModuleRouterLink = "/system";
        public const string ModuleSidebarClass = "menu-primary";
    }

    public static class Auth
    {
        public const string LoginEmail = "login@example.com";
        public const string LoginPassword = "LoginPassword123!";
        public const string RegisterEmail = "register@example.com";
        public const string RegisterPassword = "RegisterPassword123!";
        public const string RegisterFirstName = "FirstName";
        public const string RegisterLastName = "LastName";
    }

    public static class Pagination
    {
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;
        public const int TotalRecords = 50;
    }

    public static class Lookup
    {
        public const string TableName = "UserStatuses";
        public const string SearchField = "Name";
        public const string SearchValue = "Active";
    }

    public static class Query
    {
        public const string SearchTerm = "test";
        public const string SortField = "Name";
    }
}
