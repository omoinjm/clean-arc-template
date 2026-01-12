using Clean.Architecture.Template.Core.Specs;

namespace Clean.Architecture.Template.Core.UnitTests.Specs;

public class PaginationTests
{
    [Fact]
    public void Pagination_WhenCreated_ShouldInitializeWithValidPageIndex()
    {
        // Arrange
        const int pageIndex = 1;
        const int pageSize = 10;
        var data = new List<string> { "item1" };

        // Act
        var pagination = new Pagination<string>(pageIndex, pageSize, 1, data);

        // Assert
        pagination.PageIndex.Should().Be(pageIndex);
        pagination.PageSize.Should().Be(pageSize);
        pagination.Count.Should().Be(1);
        pagination.Data.Should().HaveCount(1);
    }

    [Fact]
    public void Pagination_WithDefaultConstructor_ShouldInitializeEmptyData()
    {
        // Arrange & Act
        var pagination = new Pagination<string>();

        // Assert
        pagination.Data.Should().BeEmpty();
    }

    [Theory]
    [InlineData(1, 10)]
    [InlineData(2, 20)]
    [InlineData(5, 50)]
    public void Pagination_WithValidParameters_ShouldSetCorrectly(int pageIndex, int pageSize)
    {
        // Arrange
        var data = new List<string>();

        // Act
        var pagination = new Pagination<string>(pageIndex, pageSize, 0, data);

        // Assert
        pagination.PageIndex.Should().Be(pageIndex);
        pagination.PageSize.Should().Be(pageSize);
    }

    [Fact]
    public void Pagination_CanSetPropertiesAfterCreation()
    {
        // Arrange
        var pagination = new Pagination<string>();
        const int newPageIndex = 3;
        const int newPageSize = 25;

        // Act
        pagination.PageIndex = newPageIndex;
        pagination.PageSize = newPageSize;

        // Assert
        pagination.PageIndex.Should().Be(newPageIndex);
        pagination.PageSize.Should().Be(newPageSize);
    }
}

public class GeneralSpecParamsTests
{
    [Fact]
    public void GeneralSpecParams_WhenCreated_ShouldInitializeWithDefaults()
    {
        // Arrange & Act
        var specs = new GeneralSpecParams();

        // Assert
        specs.Should().NotBeNull();
        specs.PageIndex.Should().Be(1);
        specs.PageSize.Should().Be(100);
        specs.Sort.Should().Be("1");
        specs.SortAscending.Should().BeTrue();
        specs.Search.Should().Be("");
    }

    [Fact]
    public void GeneralSpecParams_CanSetPageIndex()
    {
        // Arrange
        var specs = new GeneralSpecParams();
        const int pageIndex = 2;

        // Act
        specs.PageIndex = pageIndex;

        // Assert
        specs.PageIndex.Should().Be(pageIndex);
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
    public void GeneralSpecParams_PageSizeExceedingMaximum_ShouldCapToMax()
    {
        // Arrange
        var specs = new GeneralSpecParams();
        const int maxPageSize = 1000;

        // Act
        specs.PageSize = 2000;

        // Assert
        specs.PageSize.Should().Be(maxPageSize);
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
    public void GeneralSpecParams_CanSetDates()
    {
        // Arrange
        var specs = new GeneralSpecParams();
        var fromDate = new DateTime(2024, 1, 1);
        var toDate = new DateTime(2024, 12, 31);

        // Act
        specs.FromDate = fromDate;
        specs.ToDate = toDate;

        // Assert
        specs.FromDate.Should().Be(fromDate);
        specs.ToDate.Should().Be(toDate);
    }

    [Fact]
    public void GeneralSpecParams_WithAllProperties()
    {
        // Arrange
        const int pageIndex = 3;
        const int pageSize = 50;
        const string search = "search term";
        const string sort = "createdDate";
        var fromDate = new DateTime(2024, 1, 1);

        // Act
        var specs = new GeneralSpecParams
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Search = search,
            Sort = sort,
            FromDate = fromDate
        };

        // Assert
        specs.PageIndex.Should().Be(pageIndex);
        specs.PageSize.Should().Be(pageSize);
        specs.Search.Should().Be(search);
        specs.Sort.Should().Be(sort);
        specs.FromDate.Should().Be(fromDate);
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
    public void LookupParams_CanSetLookupTableName()
    {
        // Arrange
        var lookupParams = new LookupParams();
        const string tableName = "Users";

        // Act
        lookupParams.LookupTableName = tableName;

        // Assert
        lookupParams.LookupTableName.Should().Be(tableName);
    }

    [Fact]
    public void LookupParams_CanSetLookupPrimaryKey()
    {
        // Arrange
        var lookupParams = new LookupParams();
        const string primaryKey = "Id";

        // Act
        lookupParams.LookupPrimaryKey = primaryKey;

        // Assert
        lookupParams.LookupPrimaryKey.Should().Be(primaryKey);
    }

    [Fact]
    public void LookupParams_CanSetLookupName()
    {
        // Arrange
        var lookupParams = new LookupParams();
        const string lookupName = "UserName";

        // Act
        lookupParams.LookupName = lookupName;

        // Assert
        lookupParams.LookupName.Should().Be(lookupName);
    }

    [Fact]
    public void LookupParams_CanSetLookupCode()
    {
        // Arrange
        var lookupParams = new LookupParams();
        const string lookupCode = "USR001";

        // Act
        lookupParams.LookupCode = lookupCode;

        // Assert
        lookupParams.LookupCode.Should().Be(lookupCode);
    }

    [Fact]
    public void LookupParams_CanSetSelectedIds()
    {
        // Arrange
        var lookupParams = new LookupParams();
        const string selectedIds = "1,2,3";

        // Act
        lookupParams.SelectedIds = selectedIds;

        // Assert
        lookupParams.SelectedIds.Should().Be(selectedIds);
    }

    [Fact]
    public void LookupParams_CanSetUseCustomQuery()
    {
        // Arrange
        var lookupParams = new LookupParams();

        // Act
        lookupParams.UseCustomQuery = true;

        // Assert
        lookupParams.UseCustomQuery.Should().BeTrue();
    }

    [Fact]
    public void LookupParams_CanSetExcludeId()
    {
        // Arrange
        var lookupParams = new LookupParams();
        const int excludeId = 5;

        // Act
        lookupParams.ExcludeId = excludeId;

        // Assert
        lookupParams.ExcludeId.Should().Be(excludeId);
    }

    [Fact]
    public void LookupParams_CanSetIncludeNoneOption()
    {
        // Arrange
        var lookupParams = new LookupParams();

        // Act
        lookupParams.IncludeNoneOption = true;

        // Assert
        lookupParams.IncludeNoneOption.Should().BeTrue();
    }

    [Fact]
    public void LookupParams_WithMultipleProperties()
    {
        // Arrange
        const string tableName = "Roles";
        const string primaryKey = "RoleId";
        const string lookupName = "RoleName";

        // Act
        var lookupParams = new LookupParams
        {
            LookupTableName = tableName,
            LookupPrimaryKey = primaryKey,
            LookupName = lookupName,
            UseCustomQuery = true,
            IncludeNoneOption = true
        };

        // Assert
        lookupParams.LookupTableName.Should().Be(tableName);
        lookupParams.LookupPrimaryKey.Should().Be(primaryKey);
        lookupParams.LookupName.Should().Be(lookupName);
        lookupParams.UseCustomQuery.Should().BeTrue();
        lookupParams.IncludeNoneOption.Should().BeTrue();
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
        dataList.Count.Should().Be(0);
    }

    [Fact]
    public void DataList_CanSetCount()
    {
        // Arrange
        var dataList = new DataList<string>();
        const long count = 42;

        // Act
        dataList.Count = count;

        // Assert
        dataList.Count.Should().Be(count);
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
    public void DataList_WithConstructorParameters()
    {
        // Arrange
        const int count = 100;
        var data = new List<string> { "Item1", "Item2", "Item3", "Item4", "Item5" };

        // Act
        var dataList = new DataList<string>(count, data);

        // Assert
        dataList.Count.Should().Be(count);
        dataList.Data.Should().BeEquivalentTo(data);
        dataList.Data.Should().HaveCount(5);
    }

    [Fact]
    public void DataList_WithMultipleUpdates()
    {
        // Arrange
        var dataList = new DataList<string>();
        var firstData = new List<string> { "A", "B" };
        var secondData = new List<string> { "X", "Y", "Z" };

        // Act
        dataList.Count = 2;
        dataList.Data = firstData;
        dataList.Count = 3;
        dataList.Data = secondData;

        // Assert
        dataList.Count.Should().Be(3);
        dataList.Data.Should().HaveCount(3);
        dataList.Data.Should().BeEquivalentTo(secondData);
    }
}
