using Books.API.Extensions;
using Books.BLL.Dtos.Auth;
using Books.BLL.Services;
using Microsoft.AspNetCore.Mvc;


namespace Books.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return this.GetAction(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            return this.GetAction(response);
        }
    }
}