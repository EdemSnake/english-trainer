using System.Collections.Generic;
using App.Application.Dtos;

namespace App.Application.Features.TextPassages.Queries.GetTextPassages
{
    public class TextPassagesVm
    {
        public IList<TextPassageDto> TextPassages { get; set; } = new List<TextPassageDto>();
    }
}
