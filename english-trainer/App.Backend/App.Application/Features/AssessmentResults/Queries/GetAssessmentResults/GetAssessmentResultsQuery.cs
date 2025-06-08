using App.Application.Dtos;
using MediatR;

namespace App.Application.Features.AssessmentResults.Queries.GetAssessmentResults
{
    public class GetAssessmentResultsQuery : IRequest<IEnumerable<AssessmentResultDto>>
    {
    }
}