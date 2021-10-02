using DTOs.UserDTOs;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IUserService
    {
        void InsertNumbers(List<int> numbersChoice, int id);
        void AddUser(AddUserDTO userDTO);
    }
}
