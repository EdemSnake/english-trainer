using MediatR;

namespace App.Application.Features.TextPassages.Commands.CreateTextPassage
{
    public class CreateTextPassageCommand : IRequest<Guid>
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Difficulty { get; set; }
    }
}