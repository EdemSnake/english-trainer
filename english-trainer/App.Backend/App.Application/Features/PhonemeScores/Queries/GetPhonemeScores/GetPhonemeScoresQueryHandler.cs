using App.Application.Dtos;
using App.Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.PhonemeScores.Queries.GetPhonemeScores
{
    public class GetPhonemeScoresQueryHandler : IRequestHandler<GetPhonemeScoresQuery, IEnumerable<PhonemeScoreDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetPhonemeScoresQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PhonemeScoreDto>> Handle(GetPhonemeScoresQuery request, CancellationToken cancellationToken)
        {
            var phonemeScores = await _context.PhonemeScores.ToListAsync(cancellationToken);
            var phonemeScoreDtos = _mapper.Map<IEnumerable<PhonemeScoreDto>>(phonemeScores);
            return phonemeScoreDtos;
        }
    }
}