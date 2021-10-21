using DTOs.LotoNumbersDTOs;
using DTOs.UserDTOs;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IUserService
    {
        void Register(RegisterUserDTO registerUserDTO);
        string Login(LoginUserDTO loginUserDTO);
        void InsertNumbers(LotoNumbersDTO numbersChoice,string userId);
    }
}
