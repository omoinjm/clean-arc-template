using Clean.Architecture.Template.Application.Mapper;
using Clean.Architecture.Template.Application.Queries.General;
using Clean.Architecture.Template.Application.Response;
using Clean.Architecture.Template.Core.Repository;
using MediatR;

namespace Clean.Architecture.Template.Application.Handlers.Users
{
    public class GetUserQueryHandler(IUserRepository repository) : IRequestHandler<ItemQuery<UserResponse>, UserResponse>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<UserResponse> Handle(ItemQuery<UserResponse> request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserById(request.Id!.Value);

            return LazyMapper.Mapper.Map<UserResponse>(user);
        }
    }
}