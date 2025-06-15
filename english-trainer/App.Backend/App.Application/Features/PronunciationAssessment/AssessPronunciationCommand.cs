using MediatR;
using Microsoft.AspNetCore.Http;

namespace App.Application.Features.PronunciationAssessment
{
    public class AssessPronunciationCommand : IRequest<Unit>
    {
        public IFormFile AudioFile { get; set; }
        public string ReferenceText { get; set; }
    }
}