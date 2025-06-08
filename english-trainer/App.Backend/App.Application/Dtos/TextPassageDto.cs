using App.Application.Common.Mappings;
using App.Domain;

namespace App.Application.Dtos
{
    public class TextPassageDto : IMapFrom<TextPassage>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Difficulty { get; set; }
    }
}