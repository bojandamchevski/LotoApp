using DataAccess.Interfaces;
using Domain.Models;
using DTOs.AdminDTOs;
using DTOs.UserDTOs;
using Mappers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Shared;
using Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Services.Implementations
{
    public class AdminService : IAdminService
    {
        private IAdminRepository _adminRepository;
        private IRepository<Draw> _drawRepository;
        private IRepository<Session> _sessionRepository;
        private IRepository<WinningNumber> _winningNumberRepository;
        private IUserRepository _userRepository;
        private IRepository<Prize> _prizeRepository;
        IOptions<AppSettings> _options;
        public AdminService(IAdminRepository adminRepository, IRepository<Draw> drawRepository, IRepository<Session> sessionRepository, IRepository<WinningNumber> winningNumberRepository, IUserRepository userRepository,
            IRepository<Prize> prizeRepository, IOptions<AppSettings> options)
        {
            _adminRepository = adminRepository;
            _drawRepository = drawRepository;
            _sessionRepository = sessionRepository;
            _winningNumberRepository = winningNumberRepository;
            _userRepository = userRepository;
            _prizeRepository = prizeRepository;
            _options = options;
        }
        public void Register(RegisterAdminDTO registerAdminDTO)
        {
            ValidateUser(registerAdminDTO);
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerAdminDTO.Password);
            byte[] passwordHash = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            string hashedPassword = Encoding.ASCII.GetString(passwordHash);

            Admin newAdmin = registerAdminDTO.ToAdmin();
            newAdmin.Password = hashedPassword;

            _adminRepository.Insert(newAdmin);
        }
        public string Login(LoginAdminDTO loginAdminDTO)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(loginAdminDTO.Password));
            string hashedPassword = Encoding.ASCII.GetString(hashedBytes);

            Admin adminDb = _adminRepository.LoginAdmin(loginAdminDTO.Username, loginAdminDTO.Password);

            if (adminDb == null)
            {
                throw new ResourceNotFoundException($"Could not login admin {loginAdminDTO.Username}");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, adminDb.Id.ToString()),
                        new Claim(ClaimTypes.Name, adminDb.Username),
                        new Claim(ClaimTypes.Role, adminDb.Role)
                    })};

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
        private void ValidateUser(RegisterAdminDTO registerAdminDTO)
        {
            if (string.IsNullOrEmpty(registerAdminDTO.Username) || string.IsNullOrEmpty(registerAdminDTO.Password))
            {
                throw new AdminException("Username and password are required fields!");
            }
            if (string.IsNullOrEmpty(registerAdminDTO.Role))
            {
                throw new AdminException("Role is a required field!");
            }
            if (registerAdminDTO.Username.Length > 30)
            {
                throw new AdminException("Username can contain maximum 30 characters!");
            }
            if (registerAdminDTO.AdminName.Length > 50)
            {
                throw new AdminException("Admin name can contain maximum 50 characters!");
            }
            if (!IsUserNameUnique(registerAdminDTO.Username))
            {
                throw new AdminException("A admin with this username already exists!");
            }
            if (registerAdminDTO.Password != registerAdminDTO.ConfirmedPassword)
            {
                throw new AdminException("The passwords do not match!");
            }
            if (!IsPasswordValid(registerAdminDTO.Password))
            {
                throw new AdminException("The password is not complex enough!");
            }
        }
        private bool IsUserNameUnique(string username)
        {
            return _adminRepository.GetAdminByUsername(username) == null;
        }

        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }
        public List<int> MakeDraw(string adminId)
        {
            bool isIdValid = int.TryParse(adminId, out int id);
            _adminRepository.GetById(id);
            foreach (var item in _winningNumberRepository.GetAll())
            {
                _winningNumberRepository.Delete(item);
            };
            Admin admin = _adminRepository.GetById(id);
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

        public List<WinnerUserDTO> GetWinners(string adminId)
        {
            bool isIdValid = int.TryParse(adminId, out int id);
            Admin admin = _adminRepository.GetById(id);
            List<WinnerUserDTO> winnersList = new List<WinnerUserDTO>();
            foreach(User user in admin.Users)
            {
                if (!String.IsNullOrEmpty(user.Prize))
                {
                    WinnerUserDTO winnerUser = new WinnerUserDTO()
                    {
                        Fullname = $"{user.FirstName} {user.LastName}",
                        Prize = user.Prize
                    };
                    winnersList.Add(winnerUser);
                }
            }

            return winnersList;
        }
    }
}
