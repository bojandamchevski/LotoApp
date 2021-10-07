using DTOs.AdminDTOs;
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
    public class AdminsController : ControllerBase
    {
        private IAdminService _adminService;
        public AdminsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("register-admin")]
        [AllowAnonymous]
        //public IActionResult Register([FromBody] RegisterAdminDTO registerAdminDTO)
        //{
        //    try
        //    {
        //        _adminService.Register(registerAdminDTO);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        [HttpPost("make-draw")]
        public IActionResult GetDraw()
        {
            try
            {
                List<int> drawNumbers = _adminService.MakeDraw();
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
    }
}
