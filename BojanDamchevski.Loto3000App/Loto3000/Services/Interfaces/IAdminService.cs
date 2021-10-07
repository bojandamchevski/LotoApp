using DTOs.AdminDTOs;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IAdminService
    {
        void Register(RegisterAdminDTO registerAdminDTO);
        string Login(LoginAdminDTO loginAdminDTO);
        List<int> MakeDraw();
    }
}
