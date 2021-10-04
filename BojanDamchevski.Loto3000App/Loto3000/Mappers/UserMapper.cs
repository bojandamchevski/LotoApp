using Domain.Models;
using DTOs.UserDTOs;

namespace Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserDTO registerUserDTO, string hashedPassword)
        {
            return new User()
            {
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                Username = registerUserDTO.Username,
                Password = hashedPassword
            };
        }
    }
}
