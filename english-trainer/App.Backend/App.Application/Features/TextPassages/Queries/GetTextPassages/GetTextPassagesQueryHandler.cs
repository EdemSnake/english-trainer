using System.Collections.Generic;
using App.Application.Interfaces;
using App.Application.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.TextPassages.Queries.GetTextPassages
{
    public class GetTextPassagesQueryHandler : IRequestHandler<GetTextPassagesQuery, TextPassagesVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetTextPassagesQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TextPassagesVm> Handle(GetTextPassagesQuery request, CancellationToken cancellationToken)
        {
            var textPassages = await _context.TextPassages.ToListAsync(cancellationToken);
            var textPassageDtos = _mapper.Map<IList<TextPassageDto>>(textPassages);
            return new TextPassagesVm { TextPassages = textPassageDtos };
        }
    }
}