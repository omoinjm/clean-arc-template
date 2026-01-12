using Clean.Architecture.Template.Core.Auth;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Entity.Common;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Results;
using Clean.Architecture.Template.Core.Services;
using Clean.Architecture.Template.Core.Specs;
using Clean.Architecture.Template.Infrastructure.DbQueries.Auth;
using Clean.Architecture.Template.Infrastructure.DbQueries.Common;
using Clean.Architecture.Template.Infrastructure.DbQueries.Menu;
using Clean.Architecture.Template.Infrastructure.DbQueries.User;

namespace Clean.Architecture.Template.Infrastructure.Repository
{
    public class DBRepository(
        IPgSqlSelector sqlContext,
        ICachingInMemoryService cachingInMemoryService
    ) :
        IMenuRepository,
        ILookupRepository,
        IUserRepository
    {
        private readonly IPgSqlSelector _sqlContext = sqlContext;
        private readonly ICachingInMemoryService _cachingInMemoryService = cachingInMemoryService;

        #region Helper Methods

        /// <summary>
        /// Gets the auth token in the relevant database for the user
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        private async Task<string> GetAuthTokenAsync()
        {
            string? authToken = _cachingInMemoryService.Get<string>("AuthToken");

            var user = GetUserInfo();

            if (authToken == null)
            {
                var result = await _sqlContext.SelectQuery<dynamic>(LoginTokenQuery.Query(user.Email));

                authToken = result.FirstOrDefault()?.AuthToken;

                if (authToken == null)
                {
                    throw new ApplicationException("Cannot get authToken from UserAccount table, need to login again");
                }

                _cachingInMemoryService.Set<string>("AuthToken", authToken);
            }

            return authToken;
        }

        /// <summary>
        /// Gets the cached user info from the caching service
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        private IUserInfo GetUserInfo()
        {
            var token = _cachingInMemoryService.Get<string>("AuthToken") ?? throw new ApplicationException("Cannot get token from Caching In Memory Service, need to login again");

            return _cachingInMemoryService.Get<IUserInfo>(token) ?? throw new ApplicationException("Cannot get user info from Caching In Memory Service, need to login again");
        }

        #endregion

        #region Menu

        public async Task<List<MenuEntity>> GetMenuItems()
        {
            var items = await _sqlContext.SelectQuery<MenuEntity>(MenuQuery.Query());

            return [.. items];
        }

        #endregion

        #region Lookup

        public async Task<DataList<LookupEntity>> GetLookupList(LookupParams lookupParams)
        {
            var items = await _sqlContext.SelectQuery<LookupEntity>(LookupQuery.Query(lookupParams));

            return new DataList<LookupEntity>
            {
                Data = [.. items],
                Count = items.ToList().Count
            };
        }

        #endregion

        #region User

        public async Task<Pagination<UserEntity>> GetAllUsers(GeneralSpecParams generalSpecParams)
        {
            var items = await _sqlContext.SelectQuery<UserEntity>(new UserQuery().Query(generalSpecParams));

            return new Pagination<UserEntity>
            {
                PageIndex = generalSpecParams.PageIndex,
                PageSize = generalSpecParams.PageSize,
                Data = [.. items],
                Count = items.ToList().Count
            };
        }

        public async Task<UserEntity?> GetUserByEmail(string email)
        {
            var item = await _sqlContext.SelectFirstOrDefaultQuery<UserEntity>(UserQuery.GetUserByEmailQuery(email));

            return item ?? new UserEntity();
        }

        public async Task<UserEntity> GetUserById(int id)
        {
            var item = await _sqlContext.SelectFirstOrDefaultQuery<UserEntity>(UserQuery.GetUserByIdQuery(id));

            return item ?? new UserEntity();
        }

        public async Task<CreateRecordResult> CreateUser(UserEntity user)
        {
            var id = await _sqlContext.ExecuteScalarAsyncQuery<int>(UserQuery.CreateUserQuery(user));

            return new CreateRecordResult() { IsSuccess = true, ReturnRecordId = id };
        }

        public Task<UpdateRecordResult> UpdateUser(UserEntity user)
        {
            throw new NotImplementedException();
        }

        public Task<DeleteRecordResult> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}