using App.Application.Common.Mappings;
using App.Domain;
using AutoMapper;

namespace App.Application.Dtos
{
    public class TextPassageDto : IMapFrom<TextPassage>
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public string? Difficulty { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TextPassage, TextPassageDto>()
                .ForMember(d => d.Difficulty, opt => opt.MapFrom(src => src.DifficultyLevel));
        }
    }
}