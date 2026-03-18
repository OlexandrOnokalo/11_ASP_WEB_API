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
            string callbackUrl = $"{Request.Scheme}://{Request.Host}/api/auth/confirm-email";
            var response = await _authService.RegisterAsync(dto, callbackUrl);
            return this.GetAction(response);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string token)
        {
            var response = await _authService.ConfirmEmailAsync(userId, token);
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