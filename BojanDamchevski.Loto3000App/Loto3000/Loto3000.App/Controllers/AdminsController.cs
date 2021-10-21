using DTOs.AdminDTOs;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Loto3000.App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private IAdminService _adminService;
        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("register-admin")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisterAdminDTO registerAdminDTO)
        {
            try
            {
                _adminService.Register(registerAdminDTO);
                return StatusCode(StatusCodes.Status201Created, "Admin registered");
            }
            catch (AdminException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login-admin")]
        [AllowAnonymous]
        public ActionResult<string> Login([FromBody] LoginAdminDTO loginAdminDTO)
        {
            try
            {
                string token = _adminService.Login(loginAdminDTO);
                return StatusCode(StatusCodes.Status200OK, token);
            }
            catch (AdminException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("make-draw")]
        public IActionResult GetDraw()
        {
            try
            {
                var claims = User.Claims;
                string adminId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                List<int> drawNumbers = _adminService.MakeDraw(adminId);
                return StatusCode(StatusCodes.Status200OK, drawNumbers);
            }
            catch (AdminException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, $"You are unauthorized for this action {ex.Message}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpGet("get-winners")]
        public IActionResult GetWinners()
        {
            try
            {
                var claims = User.Claims;
                string adminId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                List<WinnerUserDTO> winners = _adminService.GetWinners(adminId);
                return StatusCode(StatusCodes.Status200OK, winners);
            }
            catch (AdminException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, $"You are unauthorized for this action {ex.Message}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }
    }
}
