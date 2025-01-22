using Clean.Architecture.Template.Application.Commands.General;
using Clean.Architecture.Template.Application.Mapper;
using Clean.Architecture.Template.Application.Response.General;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Results;
using MediatR;

namespace Clean.Architecture.Template.Application.Handlers.Users
{
    public class CreateUserHandler(
        IUserRepository repository) :
        IRequestHandler<CreateCommand<UserEntity, CreateResponse>, CreateResponse>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<CreateResponse> Handle(CreateCommand<UserEntity, CreateResponse> request, CancellationToken cancellationToken)
        {
            request.Item.Salt = request.Item.Salt;

            var result = (request.Item != null) ? await _repository.CreateUser(request.Item) : new CreateRecordResult();

            return LazyMapper.Mapper.Map<CreateResponse>(result);
        }
    }
}