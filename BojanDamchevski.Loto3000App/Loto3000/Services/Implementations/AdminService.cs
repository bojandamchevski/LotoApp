using DataAccess.Interfaces;
using Domain.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Services.Implementations
{
    public class AdminService : IAdminService
    {
        private IRepository<Admin> _adminRepository;
        private IRepository<Draw> _drawRepository;
        private IRepository<Session> _sessionRepository;
        private IRepository<WinningNumber> _winningNumberRepository;
        private IRepository<User> _userRepository;
        private IRepository<Prize> _prizeRepository;
        public AdminService(IRepository<Admin> adminRepository, IRepository<Draw> drawRepository, IRepository<Session> sessionRepository, IRepository<WinningNumber> winningNumberRepository, IRepository<User> userRepository,
            IRepository<Prize> prizeRepository)
        {
            _adminRepository = adminRepository;
            _drawRepository = drawRepository;
            _sessionRepository = sessionRepository;
            _winningNumberRepository = winningNumberRepository;
            _userRepository = userRepository;
            _prizeRepository = prizeRepository;
        }
        public List<int> MakeDraw()
        {
            foreach (var item in _winningNumberRepository.GetAll())
            {
                _winningNumberRepository.Delete(item);
            };
            Admin admin = _adminRepository.GetById(1);
            List<int> drawNumbers = new List<int>();
            Session session = new Session();
            for (int i = 0; i <= 7 && i >= 1; i++)
            {
                Random random = new Random();
                int randomNumber = random.Next(1, 37);
                drawNumbers.Add(randomNumber);
            }
            session = new Session()
            {
                LastDraw = DateTime.Now,
                NextDraw = DateTime.Now.AddDays(45)
            };
            Draw draw = new Draw();
            draw.Session = session;
            for (int j = 0; j < drawNumbers.Count; j++)
            {
                WinningNumber winningNumber = new WinningNumber()
                {
                    WinningNum = drawNumbers[j]
                };
                draw.WinningNumbers.Add(winningNumber);
                _winningNumberRepository.Insert(winningNumber);
            }
            List<User> users = _userRepository.GetAll();
            foreach (User user in users)
            {
                user.LotoNumbers = new List<LotoNumber>();
                _userRepository.Update(user);
            }
            foreach (var user in users)
            {
                List<int> nums = new List<int>();
                foreach (var lotoNumbersChoice in user.LotoNumbers)
                {
                    for (int k = 0; k <= drawNumbers.Count; k++)
                    {
                        if (lotoNumbersChoice.LotoNumberChoice == drawNumbers[k])
                        {
                            nums.Add(drawNumbers[k]);
                        }
                    }
                    user.Prize += $" {_prizeRepository.GetById(nums.Count).PrizeType}";
                }
                _userRepository.Update(user);
            }
            draw.Admin = admin;
            draw.AdminId = admin.Id;
            admin.Draws.Add(draw);
            _adminRepository.Update(admin);
            _sessionRepository.Insert(session);
            _drawRepository.Insert(draw);
            return drawNumbers;
        }
    }
}
