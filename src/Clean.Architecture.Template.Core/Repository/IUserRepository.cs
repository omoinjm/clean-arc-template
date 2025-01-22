using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Results;
using Clean.Architecture.Template.Core.Specs;

namespace Clean.Architecture.Template.Core.Repository
{
    public interface IUserRepository
    {
        Task<Pagination<UserEntity>> GetAllUsers(GeneralSpecParams generalSpecParams);
        Task<UserEntity?> GetUserByEmail(string email);
        Task<UserEntity> GetUserById(int Id);

        Task<CreateRecordResult> CreateUser(UserEntity user);
        Task<UpdateRecordResult> UpdateUser(UserEntity user);
        Task<DeleteRecordResult> DeleteUser(int id);
    }
}