using Domain.Models;
using DTOs.AdminDTOs;

namespace Mappers
{
    public static class AdminMapper
    {
        public static Admin ToAdmin(this RegisterAdminDTO registerAdminDTO)
        {
            return new Admin()
            {
                AdminName = registerAdminDTO.AdminName,
                Role = registerAdminDTO.Role,
                Username = registerAdminDTO.Username
            };
        }
    }
}
