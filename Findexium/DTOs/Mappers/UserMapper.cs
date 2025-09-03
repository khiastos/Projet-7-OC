using Findexium.Domain;
using Findexium.DTOs;

namespace Findexium.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDto(this User entity)
        {
            return new UserDTO
            {
                UserId = entity.UserId,
                Username = entity.Username,
                Password = entity.Password,
                Fullname = entity.Fullname,
                Role = entity.Role
            };
        }

        public static User ToEntity(this UserDTO dto)
        {
            return new User
            {
                UserId = dto.UserId,
                Username = dto.Username,
                Password = dto.Password,
                Fullname = dto.Fullname,
                Role = dto.Role
            };
        }
    }
}
