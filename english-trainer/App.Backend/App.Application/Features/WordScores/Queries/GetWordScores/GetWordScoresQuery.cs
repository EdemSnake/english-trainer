using App.Application.Dtos;
using MediatR;

namespace App.Application.Features.WordScores.Queries.GetWordScores
{
    public class GetWordScoresQuery : IRequest<IEnumerable<WordScoreDto>>
    {
    }
}