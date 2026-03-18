using Books.BLL.Dtos.Auth;
using Books.BLL.Services;
using Books.DAL.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Books.BLL.Services
{
    public class AuthService
    {
        private readonly UserManager<AppUserEntity> _userManager;
        private readonly JwtService _jwtService;
        private readonly EmailService _emailService;

        public AuthService(UserManager<AppUserEntity> userManager, JwtService jwtService, EmailService emailService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _emailService = emailService;
        }

        public async Task<ServiceResponse> RegisterAsync(RegisterDto dto, string callbackUrl)
        {
            if (await EmailExistAsync(dto.Email))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Пошта '{dto.Email}' вже використовується"
                };
            }

            if (await UserNameExistAsync(dto.UserName))
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Ім'я користувача '{dto.UserName}' зайняте"
                };
            }

            var entity = new AppUserEntity
            {
                UserName = dto.UserName,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var createResult = await _userManager.CreateAsync(entity, dto.Password);

            if (!createResult.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = createResult.Errors.First().Description
                };
            }

            await _userManager.AddToRoleAsync(entity, "user");

            
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(entity);
            string encodedToken = Uri.EscapeDataString(token);
            string confirmUrl = $"{callbackUrl}?userId={entity.Id}&token={encodedToken}";

            string subject = "Підтвердження електронної пошти";
            string body = $"""
                <h2>Вітаємо, {entity.FirstName}!</h2>
                <p>Дякуємо за реєстрацію. Для підтвердження електронної пошти натисніть кнопку нижче:</p>
                <a href="{confirmUrl}" style="padding:10px 20px;background:#4CAF50;color:white;text-decoration:none;border-radius:5px;">
                    Підтвердити email
                </a>
                <p>Якщо ви не реєструвалися — просто ігноруйте цей лист.</p>
                """;

            var emailResponse = await _emailService.SendAsync(entity.Email!, subject, body);

            if (!emailResponse.Success)
            {
                return emailResponse;
            }

            return new ServiceResponse
            {
                Message = "Ви успішно зареєструвалися. Перевірте пошту для підтвердження акаунта"
            };
        }

        public async Task<ServiceResponse> ConfirmEmailAsync(string userId, string token)
        {
            var entity = await _userManager.FindByIdAsync(userId);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Користувача не знайдено"
                };
            }

            string decodedToken = Uri.UnescapeDataString(token);
            var result = await _userManager.ConfirmEmailAsync(entity, decodedToken);

            if (!result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = result.Errors.First().Description
                };
            }

            return new ServiceResponse
            {
                Message = "Email успішно підтверджено! Тепер ви можете увійти"
            };
        }

        public async Task<ServiceResponse> LoginAsync(LoginDto dto)
        {
            var entity = await _userManager.FindByEmailAsync(dto.Email);

            if (entity == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = $"Користувач з поштою '{dto.Email}' не існує"
                };
            }

            bool res = await _userManager.CheckPasswordAsync(entity, dto.Password);

            if (!res)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Пароль вказано невірно"
                };
            }

            string jwtToken = await _jwtService.GenerateAccessTokenAsync(entity);

            return new ServiceResponse
            {
                Message = "Успішний вхід",
                Payload = jwtToken
            };
        }

        private async Task<bool> EmailExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        private async Task<bool> UserNameExistAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName) != null;
        }
    }
}