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
            _winningNumberRepository.GetAll().Clear();
            Admin admin = _adminRepository.GetById(1);
            List<int> drawNumbers = new List<int>();
            Session session = new Session();
            for (int i = 0; i <= 37 && i >= 1; i++)
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
            foreach (var user in users)
            {
                foreach (var lotoNumbersChoice in user.LotoNumbers)
                {
                    for (int k = 0; k <= drawNumbers.Count; k++)
                    {
                        if (lotoNumbersChoice.LotoNumberChoice == drawNumbers[k])
                        {
                            user.Prize += $" {_prizeRepository.GetById(drawNumbers[k]).PrizeType}";
                        }
                    }
                }
            }
            admin.Draws.Add(draw);
            draw.Admin = admin;
            draw.AdminId = admin.Id;
            _sessionRepository.Insert(session);
            _drawRepository.Insert(draw);
            return drawNumbers;
        }
    }
}
