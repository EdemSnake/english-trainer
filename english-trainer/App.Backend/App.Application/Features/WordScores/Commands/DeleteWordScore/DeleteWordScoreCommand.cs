using MediatR;

namespace App.Application.Features.WordScores.Commands.DeleteWordScore
{
    public class DeleteWordScoreCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}