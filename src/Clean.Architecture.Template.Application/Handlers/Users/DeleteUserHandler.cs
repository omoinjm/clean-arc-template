using Clean.Architecture.Template.Application.Commands.General;
using Clean.Architecture.Template.Application.Mapper;
using Clean.Architecture.Template.Application.Response.General;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Results;
using MediatR;

namespace Clean.Architecture.Template.Application.Handlers.Users
{
    public class DeleteUserHandler(
        IUserRepository repository) :
        IRequestHandler<DeleteCommand<UserEntity, DeleteResponse>, DeleteResponse>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<DeleteResponse> Handle(DeleteCommand<UserEntity, DeleteResponse> request, CancellationToken cancellationToken)
        {
            var result = (request.Id != null && request.Id != 0) ? await _repository.DeleteUser((int)request.Id) : new DeleteRecordResult();

            return LazyMapper.Mapper.Map<DeleteResponse>(result);
        }
    }
}