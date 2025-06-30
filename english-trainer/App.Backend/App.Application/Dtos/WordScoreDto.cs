using App.Application.Common.Mappings;
using App.Domain;
using AutoMapper;

namespace App.Application.Dtos
{
    public class WordScoreDto : IMapFrom<WordScore>
    {
        public Guid Id { get; set; }
        public Guid AssessmentResultId { get; set; }
        public string? Word { get; set; }
        public float Accuracy { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WordScore, WordScoreDto>()
                .ForMember(d => d.Accuracy, opt => opt.MapFrom(src => src.AccuracyScore));
        }
    }
}