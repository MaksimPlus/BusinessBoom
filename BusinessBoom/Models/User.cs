using BusinessBoom.DTO;
using BusinessBoom.Models.Base;

namespace BusinessBoom.Models
{
    public class User : BaseModel
    {
        public string Email { get; set; }
        public double Balance { get; set; }
        public UserResponseDto ToDto()
        {
            UserResponseDto dto = new UserResponseDto();
            dto.Id = Id;
            dto.Balance = Balance;
            return dto;
        }
    }
}
