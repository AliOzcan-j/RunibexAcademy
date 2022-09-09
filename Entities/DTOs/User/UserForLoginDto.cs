using Core.Entities.Abstract;

namespace Entities.DTOs.User
{
    public class UserForLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
