using DTOs.AdminDTOs;
using DTOs.UserDTOs;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IAdminService
    {
        void Register(RegisterAdminDTO registerAdminDTO);
        string Login(LoginAdminDTO loginAdminDTO);
        List<int> MakeDraw(string adminId);
        List<WinnerUserDTO> GetWinners(string adminId);
    }
}
