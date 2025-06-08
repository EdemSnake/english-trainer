using MediatR;

namespace App.Application.Features.PhonemeScores.Commands.UpdatePhonemeScore
{
    public class UpdatePhonemeScoreCommand : IRequest
    {
        public Guid Id { get; set; }
        public float Accuracy { get; set; }
    }
}