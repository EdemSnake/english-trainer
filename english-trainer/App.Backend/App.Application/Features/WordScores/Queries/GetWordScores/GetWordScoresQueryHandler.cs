using App.Application.Dtos;
using App.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.WordScores.Queries.GetWordScores
{
    public class GetWordScoresQueryHandler : IRequestHandler<GetWordScoresQuery, IEnumerable<WordScoreDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetWordScoresQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<WordScoreDto>> Handle(GetWordScoresQuery request, CancellationToken cancellationToken)
        {
            var wordScores = await _context.WordScores.ToListAsync(cancellationToken);
            var wordScoreDtos = _mapper.Map<IEnumerable<WordScoreDto>>(wordScores);
            return wordScoreDtos;
        }
    }
}