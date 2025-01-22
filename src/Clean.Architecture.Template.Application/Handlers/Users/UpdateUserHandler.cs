using Clean.Architecture.Template.Application.Commands.General;
using Clean.Architecture.Template.Application.Mapper;
using Clean.Architecture.Template.Application.Response.General;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Results;
using MediatR;

namespace Clean.Architecture.Template.Application.Handlers.Users
{
    public class UpdateUserHandler(IUserRepository repository) : IRequestHandler<UpdateCommand<UserEntity, UpdateResponse>, UpdateResponse>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<UpdateResponse> Handle(UpdateCommand<UserEntity, UpdateResponse> request, CancellationToken cancellationToken)
        {

            var result = (request.Item != null) ? await _repository.UpdateUser(request.Item) : new UpdateRecordResult() { };

            return LazyMapper.Mapper.Map<UpdateResponse>(result);
        }
    }
}