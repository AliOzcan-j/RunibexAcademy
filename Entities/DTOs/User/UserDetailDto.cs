using Core.Entities.Abstract;

namespace Entities.DTOs.User
{
    public class UserDetailDto : IDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Country { get; set; }
        public bool? Stasus { get; set; }
    }
}
