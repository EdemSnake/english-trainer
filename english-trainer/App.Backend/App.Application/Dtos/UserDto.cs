using App.Application.Common.Mappings;
using App.Domain;

namespace App.Application.Dtos
{
    public class UserDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
    }
}