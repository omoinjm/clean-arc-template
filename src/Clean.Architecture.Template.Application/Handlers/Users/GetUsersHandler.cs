using Clean.Architecture.Template.Application.Mapper;
using Clean.Architecture.Template.Application.Queries.General;
using Clean.Architecture.Template.Application.Response;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Specs;
using MediatR;

namespace Clean.Architecture.Template.Application.Handlers.Users
{
    public class GetUsersHandler(
        IUserRepository repository) :
        IRequestHandler<ListQuery<UserResponse>, Pagination<UserResponse>>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<Pagination<UserResponse>> Handle(ListQuery<UserResponse> request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetAllUsers(request.GeneralSpecParams);

            return LazyMapper.Mapper.Map<Pagination<UserResponse>>(items);
        }
    }
}