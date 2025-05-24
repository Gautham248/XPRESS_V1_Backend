using Microsoft.AspNetCore.Mvc;
using XPRESS_V1_Backend.Interfaces;

namespace XPRESS_V1_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Get Employees By Id
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetUserById(int employeeId)
        {
            var employee = await _userService.GetUserByIdAsync(employeeId);
            return Ok(employee);
        }
    }
}