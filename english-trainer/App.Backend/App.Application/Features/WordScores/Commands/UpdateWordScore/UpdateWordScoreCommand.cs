using MediatR;

namespace App.Application.Features.WordScores.Commands.UpdateWordScore
{
    public class UpdateWordScoreCommand : IRequest
    {
        public Guid Id { get; set; }
        public float Accuracy { get; set; }
    }
}