using MediatR;

namespace App.Application.Features.PhonemeScores.Commands.CreatePhonemeScore
{
    public class CreatePhonemeScoreCommand : IRequest<Guid>
    {
        public Guid WordScoreId { get; set; }
        public string? Phoneme { get; set; }
        public float Accuracy { get; set; }
    }
}