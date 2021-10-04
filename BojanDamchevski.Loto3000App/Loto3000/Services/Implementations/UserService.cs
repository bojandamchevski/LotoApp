using DataAccess.Interfaces;
using Domain.Models;
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
    public class UserService : IUserService
    {
        public IUserRepository _userRepository { get; set; }
        public IRepository<LotoNumber> _lotoNumberRepository { get; set; }
        IOptions<AppSettings> _options;
        public UserService(IUserRepository userRepository, IRepository<LotoNumber> lotoNumberRepository, IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _lotoNumberRepository = lotoNumberRepository;
            _options = options;
        }
        public void Register(RegisterUserDTO registerUserDTO)
        {
            ValidateUser(registerUserDTO);

            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDTO.Password);
            byte[] passwordHash = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            string hashedPassword = Encoding.ASCII.GetString(passwordHash);

            User newUser = registerUserDTO.ToUser(hashedPassword);
            _userRepository.Insert(newUser);
        }

        public string Login(LoginUserDTO loginUserDTO)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(loginUserDTO.Password);
            byte[] hashedBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            string hashedPassword = Encoding.ASCII.GetString(hashedBytes);

            User userDb = _userRepository.LoginUser(loginUserDTO.Username, hashedPassword);

            if (userDb == null)
            {
                throw new ResourceNotFoundException($"Could not login user {loginUserDTO.Username}");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}")
                    })
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(token);
        }

        private void ValidateUser(RegisterUserDTO registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password))
            {
                throw new UserException("Username and password are required fields!");
            }
            if (registerUserDto.Username.Length > 50)
            {
                throw new UserException("Username can contain maximum 50 characters!");
            }
            if (registerUserDto.FirstName.Length > 50 || registerUserDto.LastName.Length > 50)
            {
                throw new UserException("Firstname and Lastname can contain maximum 50 characters!");
            }
            if (!IsUserNameUnique(registerUserDto.Username))
            {
                throw new UserException("A user with this username already exists!");
            }
            if (registerUserDto.Password.Length > 50)
            {
                throw new UserException("Password can contain maximum 50 characters!");
            }
            if (registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new UserException("The passwords do not match!");
            }
            if (!IsPasswordValid(registerUserDto.Password))
            {
                throw new UserException("The password is not complex enough!");
            }
        }

        private bool IsUserNameUnique(string username)
        {
            return _userRepository.GetUserByUsername(username) == null;
        }

        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
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
    }
}
