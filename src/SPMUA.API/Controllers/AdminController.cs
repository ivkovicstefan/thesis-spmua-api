using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPMUA.Model.DTOs.Admin;
using SPMUA.Service.Contracts;

namespace SPMUA.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] AdminRegisterDTO adminRegisterDTO)
        {
            int result = await _adminService.RegisterAsync(adminRegisterDTO);

            return new CreatedAtActionResult(nameof(GetAdminByIdAsync),
                                             nameof(AdminController).Replace("Controller", ""),
                                             new { adminId = result },
                                             null);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginAsync([FromBody] AdminLoginDTO adminLoginDTO)
        {
            return new OkObjectResult(await _adminService.LoginAsync(adminLoginDTO));
        }

        [HttpGet("admin/{adminId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AdminDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAdminByIdAsync([FromRoute] int adminId)
        {
            return new OkObjectResult(await _adminService.GetAdminByIdAsync(adminId));
        }
    }
}
