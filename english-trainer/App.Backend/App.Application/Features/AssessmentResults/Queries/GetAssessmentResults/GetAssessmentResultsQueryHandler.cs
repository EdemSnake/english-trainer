using System.Collections.Generic;
using App.Application.Interfaces;
using App.Application.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.AssessmentResults.Queries.GetAssessmentResults
{
    public class GetAssessmentResultsQueryHandler : IRequestHandler<GetAssessmentResultsQuery, AssessmentResultsVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAssessmentResultsQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AssessmentResultsVm> Handle(GetAssessmentResultsQuery request, CancellationToken cancellationToken)
        {
            var assessmentResults = await _context.AssessmentResults.ToListAsync(cancellationToken);
            var assessmentResultDtos = _mapper.Map<IList<AssessmentResultDto>>(assessmentResults);
            return new AssessmentResultsVm { AssessmentResults = assessmentResultDtos };
        }
    }
}