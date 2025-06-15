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
    public class AssessPronunciationCommandHandler : IRequestHandler<AssessPronunciationCommand, Unit>
    {
        private readonly IPronunciationAssessmentService _pronunciationAssessmentService;
        private readonly IAppDbContext _context;

        public AssessPronunciationCommandHandler(IPronunciationAssessmentService pronunciationAssessmentService, IAppDbContext context)
        {
            _pronunciationAssessmentService = pronunciationAssessmentService;
            _context = context;
        }

        public async Task<Unit> Handle(AssessPronunciationCommand request, CancellationToken cancellationToken)
        {
            byte[] audioData;
            using (var memoryStream = new MemoryStream())
            {
                await request.AudioFile.CopyToAsync(memoryStream, cancellationToken);
                audioData = memoryStream.ToArray();
            }

            var jsonResult = await _pronunciationAssessmentService.AssessPronunciationAsync(audioData, request.ReferenceText);

            var assessmentResultData = JsonConvert.DeserializeObject<dynamic>(jsonResult);

            var assessmentResult = new AssessmentResult
            {
                AccuracyScore = assessmentResultData.NBest[0].AccuracyScore,
                PronunciationScore = assessmentResultData.NBest[0].PronunciationScore,
                CompletenessScore = assessmentResultData.NBest[0].CompletenessScore,
                FluencyScore = assessmentResultData.NBest[0].FluencyScore
            };

            _context.AssessmentResults.Add(assessmentResult);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var word in assessmentResultData.NBest[0].Words)
            {
                var wordScore = new WordScore
                {
                    Word = word.Word,
                    AccuracyScore = word.AccuracyScore,
                    AssessmentResultId = assessmentResult.Id
                };

                _context.WordScores.Add(wordScore);
                await _context.SaveChangesAsync(cancellationToken);

                foreach (var phoneme in word.Phonemes)
                {
                    var phonemeScore = new PhonemeScore
                    {
                        Phoneme = phoneme.Phoneme,
                        AccuracyScore = phoneme.AccuracyScore,
                        WordScoreId = wordScore.Id
                    };
                    _context.PhonemeScores.Add(phonemeScore);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}