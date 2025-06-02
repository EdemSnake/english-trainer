using App.Application.Dtos;
using MediatR;

namespace App.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }
}