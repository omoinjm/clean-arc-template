namespace Clean.Architecture.Template.Core.Services
{
    public interface IPgSqlSelector
    {
        dynamic ExecuteAsyncProcQuery<T>(string query, object? parameters = null);

        Task<bool> ExecuteAsyncQuery(string query, object? parameters = null);
        Task<T?> ExecuteScalarAsyncQuery<T>(string query, object? parameters = null);

        Task<List<T>> SelectQuery<T>(string query, object? parameters = null) where T : new();
        Task<T?> SelectFirstOrDefaultQuery<T>(string query, object? parameters = null) where T : new();
    }
}