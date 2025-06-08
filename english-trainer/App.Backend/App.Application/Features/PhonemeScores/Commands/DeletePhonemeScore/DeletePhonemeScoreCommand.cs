using MediatR;

namespace App.Application.Features.PhonemeScores.Commands.DeletePhonemeScore
{
    public class DeletePhonemeScoreCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}