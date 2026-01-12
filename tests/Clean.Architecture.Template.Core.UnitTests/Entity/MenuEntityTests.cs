using Clean.Architecture.Template.Core.Entity;

namespace Clean.Architecture.Template.Core.UnitTests.Entity;

public class MenuEntityTests
{
    [Fact]
    public void MenuEntity_WhenCreated_ShouldInitializeProperties()
    {
        // Arrange & Act
        var menu = new MenuEntity
        {
            ModuleId = 1,
            ModuleItemId = 1,
            DisplayText = "Dashboard",
            Icon = "dashboard"
        };

        // Assert
        menu.Should().NotBeNull();
        menu.ModuleId.Should().Be(1);
        menu.ModuleItemId.Should().Be(1);
        menu.DisplayText.Should().Be("Dashboard");
        menu.Icon.Should().Be("dashboard");
    }

    [Fact]
    public void MenuEntity_WithRouterLink_ShouldSetRouterLinkCorrectly()
    {
        // Arrange
        const string expectedRouterLink = "/dashboard";

        // Act
        var menu = new MenuEntity
        {
            ModuleId = 1,
            RouterLink = expectedRouterLink
        };

        // Assert
        menu.RouterLink.Should().Be(expectedRouterLink);
    }

    [Fact]
    public void MenuEntity_WithSortOrder_ShouldSetSortOrderCorrectly()
    {
        // Arrange
        const int expectedSortOrder = 10;

        // Act
        var menu = new MenuEntity
        {
            SortOrder = expectedSortOrder
        };

        // Assert
        menu.SortOrder.Should().Be(expectedSortOrder);
    }

    [Fact]
    public void MenuEntity_WithModuleSidebarClass_ShouldSetModuleSidebarClassCorrectly()
    {
        // Arrange
        const string expectedClass = "menu-primary";

        // Act
        var menu = new MenuEntity
        {
            ModuleSidebarClass = expectedClass
        };

        // Assert
        menu.ModuleSidebarClass.Should().Be(expectedClass);
    }

    [Fact]
    public void MenuEntity_CanSetAllProperties()
    {
        // Arrange
        const int moduleId = 5;
        const int moduleItemId = 10;
        const string displayText = "Users";
        const string icon = "users";
        const string routerLink = "/users";
        const int sortOrder = 20;
        const int moduleSortOrder = 5;
        const string moduleIcon = "admin";
        const string moduleDisplayText = "Administration";
        const string moduleRouterLink = "/admin";
        const string sidebarClass = "sidebar-class";

        // Act
        var menu = new MenuEntity
        {
            ModuleId = moduleId,
            ModuleItemId = moduleItemId,
            DisplayText = displayText,
            Icon = icon,
            RouterLink = routerLink,
            SortOrder = sortOrder,
            ModuleSortOrder = moduleSortOrder,
            ModuleIcon = moduleIcon,
            ModuleDisplayText = moduleDisplayText,
            ModuleRouterLink = moduleRouterLink,
            ModuleSidebarClass = sidebarClass
        };

        // Assert
        menu.ModuleId.Should().Be(moduleId);
        menu.ModuleItemId.Should().Be(moduleItemId);
        menu.DisplayText.Should().Be(displayText);
        menu.Icon.Should().Be(icon);
        menu.RouterLink.Should().Be(routerLink);
        menu.SortOrder.Should().Be(sortOrder);
        menu.ModuleSortOrder.Should().Be(moduleSortOrder);
        menu.ModuleIcon.Should().Be(moduleIcon);
        menu.ModuleDisplayText.Should().Be(moduleDisplayText);
        menu.ModuleRouterLink.Should().Be(moduleRouterLink);
        menu.ModuleSidebarClass.Should().Be(sidebarClass);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void MenuEntity_WithDifferentModuleIds_ShouldSetModuleIdCorrectly(int moduleId)
    {
        // Arrange & Act
        var menu = new MenuEntity { ModuleId = moduleId };

        // Assert
        menu.ModuleId.Should().Be(moduleId);
    }

    [Theory]
    [InlineData("Dashboard")]
    [InlineData("Users")]
    [InlineData("Reports")]
    public void MenuEntity_WithDifferentDisplayText_ShouldSetDisplayTextCorrectly(string displayText)
    {
        // Arrange & Act
        var menu = new MenuEntity { DisplayText = displayText };

        // Assert
        menu.DisplayText.Should().Be(displayText);
    }

    [Fact]
    public void MenuEntity_CanHaveNullProperties()
    {
        // Arrange & Act
        var menu = new MenuEntity();

        // Assert
        menu.ModuleSidebarClass.Should().BeNull();
        menu.Icon.Should().BeNull();
        menu.DisplayText.Should().BeNull();
        menu.RouterLink.Should().BeNull();
    }
}
