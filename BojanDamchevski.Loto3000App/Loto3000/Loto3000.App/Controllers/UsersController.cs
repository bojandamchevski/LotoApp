using DTOs.LotoNumbersDTOs;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loto3000.App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register-user")]
        [AllowAnonymous]
        public IActionResult RegisterUser([FromBody] RegisterUserDTO registerUserDTO)
        {
            try
            {
                _userService.Register(registerUserDTO);
                return StatusCode(StatusCodes.Status201Created, "New user registered.");
            }
            catch (UserException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"Error. {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occured. {ex.Message}");
            }
        }

        [HttpPost("login-user")]
        [AllowAnonymous]
        public ActionResult<string> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            try
            {
                string token = _userService.Login(loginUserDTO);
                return StatusCode(StatusCodes.Status200OK, "You just logged in !");
            }
            catch (UserException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"User not found. {ex.Message}.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("enter-loto-numbers")]
        public IActionResult EnterLotoNumbers([FromBody] LotoNumbersDTO lotoNumbersDTO)
        {
            try
            {
                _userService.InsertNumbers(lotoNumbersDTO);
                return StatusCode(StatusCodes.Status200OK, "You have entered your choice of numbers.");
            }
            catch (UserException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, $"You are unauthorized for this action. {ex.Message}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }
    }
}
