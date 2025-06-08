using App.Application.Dtos;
using MediatR;

namespace App.Application.Features.TextPassages.Queries.GetTextPassages
{
    public class GetTextPassagesQuery : IRequest<IEnumerable<TextPassageDto>>
    {
    }
}