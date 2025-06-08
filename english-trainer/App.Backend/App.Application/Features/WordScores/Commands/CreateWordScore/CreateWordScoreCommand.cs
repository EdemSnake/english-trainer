using MediatR;

namespace App.Application.Features.WordScores.Commands.CreateWordScore
{
    public class CreateWordScoreCommand : IRequest<Guid>
    {
        public Guid AssessmentResultId { get; set; }
        public string? Word { get; set; }
        public float Accuracy { get; set; }
    }
}