using App.Application.Interfaces;
using App.Domain;
using MediatR;

namespace App.Application.Features.TextPassages.Commands.CreateTextPassage
{
    public class CreateTextPassageCommandHandler : IRequestHandler<CreateTextPassageCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateTextPassageCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateTextPassageCommand request, CancellationToken cancellationToken)
        {
            var textPassage = new TextPassage
            {
                Id = Guid.NewGuid(),
                Content = request.Content ?? string.Empty,
                DifficultyLevel = request.Difficulty ?? string.Empty
            };

            await _context.TextPassages.AddAsync(textPassage, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return textPassage.Id;
        }
    }
}