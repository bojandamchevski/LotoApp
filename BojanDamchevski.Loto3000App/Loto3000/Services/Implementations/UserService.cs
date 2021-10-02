using DataAccess.Interfaces;
using Domain.Models;
using DTOs.UserDTOs;
using Services.Interfaces;
using System.Collections.Generic;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        public IRepository<User> _userRepository { get; set; }
        public IRepository<LotoNumber> _lotoNumberRepository { get; set; }
        public UserService(IRepository<User> userRepository, IRepository<LotoNumber> lotoNumberRepository)
        {
            _userRepository = userRepository;
            _lotoNumberRepository = lotoNumberRepository;
        }

        public void InsertNumbers(List<int> numbersChoice, int id)
        {
            User user = _userRepository.GetById(id);
            for (int i = 0; i <= numbersChoice.Count; i++)
            {
                LotoNumber lotoNumber = new LotoNumber();
                lotoNumber.LotoNumberChoice = numbersChoice[i];
                lotoNumber.User = user;
                lotoNumber.UserId = user.Id;
                user.LotoNumbers.Add(lotoNumber);
                _lotoNumberRepository.Insert(lotoNumber);
            }
        }

        public void AddUser(AddUserDTO userDTO)
        {
            User newUser = new User();
            newUser.FirstName = userDTO.FirstName;
            newUser.LastName = userDTO.LastName;
            newUser.AdminId = 1;

            _userRepository.Insert(newUser);
        }
    }
}
