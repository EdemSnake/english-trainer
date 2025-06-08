using MediatR;

namespace App.Application.Features.TextPassages.Commands.DeleteTextPassage
{
    public class DeleteTextPassageCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}