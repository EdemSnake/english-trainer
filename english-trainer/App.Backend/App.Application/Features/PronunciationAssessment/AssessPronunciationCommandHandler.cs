using App.Application.Interfaces;
using App.Domain;
using MediatR;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Features.PronunciationAssessment
{
    public class AssessPronunciationCommandHandler : IRequestHandler<AssessPronunciationCommand, string>
    {
        private readonly IPronunciationAssessmentService _pronunciationAssessmentService;
        private readonly IAppDbContext _context;

        public AssessPronunciationCommandHandler(IPronunciationAssessmentService pronunciationAssessmentService, IAppDbContext context)
        {
            _pronunciationAssessmentService = pronunciationAssessmentService;
            _context = context;
        }

        public async Task<string> Handle(AssessPronunciationCommand request, CancellationToken cancellationToken)
        {
            byte[] audioData;
            using (var memoryStream = new MemoryStream())
            {
                await request.AudioFile.CopyToAsync(memoryStream, cancellationToken);
                audioData = memoryStream.ToArray();
            }

            var jsonResult = await _pronunciationAssessmentService.AssessPronunciationAsync(audioData, request.ReferenceText);

            // No database saving for now, just return the JSON result
            return jsonResult;
        }
    }
}