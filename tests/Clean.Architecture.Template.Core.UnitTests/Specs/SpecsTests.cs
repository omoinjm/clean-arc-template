using Clean.Architecture.Template.Core.Specs;

namespace Clean.Architecture.Template.Core.UnitTests.Specs;

public class PaginationTests
{
    [Fact]
    public void Pagination_WhenCreated_ShouldInitializeWithValidPageNumber()
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 10;

        // Act
        var pagination = new Pagination
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Assert
        pagination.PageNumber.Should().Be(pageNumber);
        pagination.PageSize.Should().Be(pageSize);
    }

    [Fact]
    public void Pagination_WithZeroPageSize_ShouldSetPageSize()
    {
        // Arrange & Act
        var pagination = new Pagination
        {
            PageSize = 0
        };

        // Assert
        pagination.PageSize.Should().Be(0);
    }

    [Theory]
    [InlineData(1, 10)]
    [InlineData(2, 20)]
    [InlineData(5, 50)]
    public void Pagination_WithValidPageNumbers_ShouldSetCorrectly(int pageNumber, int pageSize)
    {
        // Arrange & Act
        var pagination = new Pagination
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Assert
        pagination.PageNumber.Should().Be(pageNumber);
        pagination.PageSize.Should().Be(pageSize);
    }
}

public class GeneralSpecParamsTests
{
    [Fact]
    public void GeneralSpecParams_WhenCreated_ShouldInitializeProperties()
    {
        // Arrange & Act
        var specs = new GeneralSpecParams();

        // Assert
        specs.Should().NotBeNull();
    }

    [Fact]
    public void GeneralSpecParams_CanSetPageNumber()
    {
        // Arrange
        var specs = new GeneralSpecParams();
        const int pageNumber = 2;

        // Act
        specs.PageNumber = pageNumber;

        // Assert
        specs.PageNumber.Should().Be(pageNumber);
    }

    [Fact]
    public void GeneralSpecParams_CanSetPageSize()
    {
        // Arrange
        var specs = new GeneralSpecParams();
        const int pageSize = 25;

        // Act
        specs.PageSize = pageSize;

        // Assert
        specs.PageSize.Should().Be(pageSize);
    }

    [Fact]
    public void GeneralSpecParams_CanSetSearch()
    {
        // Arrange
        var specs = new GeneralSpecParams();
        const string search = "test search";

        // Act
        specs.Search = search;

        // Assert
        specs.Search.Should().Be(search);
    }

    [Fact]
    public void GeneralSpecParams_CanSetSort()
    {
        // Arrange
        var specs = new GeneralSpecParams();
        const string sort = "name";

        // Act
        specs.Sort = sort;

        // Assert
        specs.Sort.Should().Be(sort);
    }

    [Fact]
    public void GeneralSpecParams_WithAllProperties()
    {
        // Arrange
        const int pageNumber = 3;
        const int pageSize = 50;
        const string search = "search term";
        const string sort = "createdDate";

        // Act
        var specs = new GeneralSpecParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Search = search,
            Sort = sort
        };

        // Assert
        specs.PageNumber.Should().Be(pageNumber);
        specs.PageSize.Should().Be(pageSize);
        specs.Search.Should().Be(search);
        specs.Sort.Should().Be(sort);
    }
}

public class LookupParamsTests
{
    [Fact]
    public void LookupParams_WhenCreated_ShouldInitialize()
    {
        // Arrange & Act
        var lookupParams = new LookupParams();

        // Assert
        lookupParams.Should().NotBeNull();
    }

    [Fact]
    public void LookupParams_CanSetTableName()
    {
        // Arrange
        var lookupParams = new LookupParams();
        const string tableName = "Users";

        // Act
        lookupParams.TableName = tableName;

        // Assert
        lookupParams.TableName.Should().Be(tableName);
    }

    [Fact]
    public void LookupParams_CanSetSearchField()
    {
        // Arrange
        var lookupParams = new LookupParams();
        const string searchField = "Name";

        // Act
        lookupParams.SearchField = searchField;

        // Assert
        lookupParams.SearchField.Should().Be(searchField);
    }

    [Fact]
    public void LookupParams_CanSetSearchValue()
    {
        // Arrange
        var lookupParams = new LookupParams();
        const string searchValue = "John";

        // Act
        lookupParams.SearchValue = searchValue;

        // Assert
        lookupParams.SearchValue.Should().Be(searchValue);
    }

    [Fact]
    public void LookupParams_WithMultipleProperties()
    {
        // Arrange
        const string tableName = "Roles";
        const string searchField = "RoleName";
        const string searchValue = "Admin";

        // Act
        var lookupParams = new LookupParams
        {
            TableName = tableName,
            SearchField = searchField,
            SearchValue = searchValue
        };

        // Assert
        lookupParams.TableName.Should().Be(tableName);
        lookupParams.SearchField.Should().Be(searchField);
        lookupParams.SearchValue.Should().Be(searchValue);
    }
}

public class DataListTests
{
    [Fact]
    public void DataList_WhenCreated_ShouldInitializeEmptyList()
    {
        // Arrange & Act
        var dataList = new DataList<string>();

        // Assert
        dataList.Should().NotBeNull();
        dataList.Data.Should().BeEmpty();
    }

    [Fact]
    public void DataList_CanSetPageNumber()
    {
        // Arrange
        var dataList = new DataList<string>();
        const int pageNumber = 2;

        // Act
        dataList.PageNumber = pageNumber;

        // Assert
        dataList.PageNumber.Should().Be(pageNumber);
    }

    [Fact]
    public void DataList_CanSetPageSize()
    {
        // Arrange
        var dataList = new DataList<string>();
        const int pageSize = 25;

        // Act
        dataList.PageSize = pageSize;

        // Assert
        dataList.PageSize.Should().Be(pageSize);
    }

    [Fact]
    public void DataList_CanSetTotalRecords()
    {
        // Arrange
        var dataList = new DataList<string>();
        const int totalRecords = 100;

        // Act
        dataList.TotalRecords = totalRecords;

        // Assert
        dataList.TotalRecords.Should().Be(totalRecords);
    }

    [Fact]
    public void DataList_CanSetData()
    {
        // Arrange
        var dataList = new DataList<string>();
        var data = new List<string> { "Item1", "Item2", "Item3" };

        // Act
        dataList.Data = data;

        // Assert
        dataList.Data.Should().HaveCount(3);
        dataList.Data.Should().BeEquivalentTo(data);
    }

    [Fact]
    public void DataList_CanSetAllProperties()
    {
        // Arrange
        const int pageNumber = 2;
        const int pageSize = 20;
        const int totalRecords = 100;
        var data = new List<int> { 1, 2, 3, 4, 5 };

        // Act
        var dataList = new DataList<int>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords,
            Data = data
        };

        // Assert
        dataList.PageNumber.Should().Be(pageNumber);
        dataList.PageSize.Should().Be(pageSize);
        dataList.TotalRecords.Should().Be(totalRecords);
        dataList.Data.Should().BeEquivalentTo(data);
    }
}
