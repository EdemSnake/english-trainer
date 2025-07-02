using MediatR;
using Microsoft.AspNetCore.Http;

namespace App.Application.Features.PronunciationAssessment
{
    public class AssessPronunciationCommand : IRequest<string>
    {
        public Guid UserId { get; set; }
        public Guid TextPassageId { get; set; }
        public IFormFile AudioFile { get; set; }
        public string ReferenceText { get; set; }
    }
}