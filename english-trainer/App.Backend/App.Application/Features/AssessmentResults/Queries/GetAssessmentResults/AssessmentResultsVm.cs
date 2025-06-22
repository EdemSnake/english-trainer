using System.Collections.Generic;
using App.Application.Dtos;

namespace App.Application.Features.AssessmentResults.Queries.GetAssessmentResults
{
    public class AssessmentResultsVm
    {
        public IList<AssessmentResultDto> AssessmentResults { get; set; } = new List<AssessmentResultDto>();
    }
}
