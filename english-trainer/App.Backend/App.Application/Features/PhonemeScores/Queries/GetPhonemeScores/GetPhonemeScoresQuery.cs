using App.Application.Dtos;
using MediatR;

namespace App.Application.Features.PhonemeScores.Queries.GetPhonemeScores
{
    public class GetPhonemeScoresQuery : IRequest<IEnumerable<PhonemeScoreDto>>
    {
    }
}