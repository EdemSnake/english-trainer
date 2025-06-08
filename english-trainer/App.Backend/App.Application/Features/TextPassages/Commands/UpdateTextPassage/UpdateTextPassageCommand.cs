using MediatR;

namespace App.Application.Features.TextPassages.Commands.UpdateTextPassage
{
    public class UpdateTextPassageCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Difficulty { get; set; }
    }
}