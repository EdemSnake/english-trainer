using App.Application.Dtos;
using App.Application.Interfaces;

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            return userDtos;
        }
    }
}