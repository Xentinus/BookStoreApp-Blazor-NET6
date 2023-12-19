using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager)
        {
            _logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            _logger.LogInformation($"Registration Attempt for {userDto.Email}");

            try
            {
                var user = mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;
                var result = await userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await userManager.AddToRoleAsync(user, "User");

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login (LoginUserDto userDto)
        {
            _logger.LogInformation($"Login Attempt for {userDto.Email}");

            try
            {
                // Check user exits
                var user = await userManager.FindByEmailAsync(userDto.Email);

                if (user == null)return NotFound();

                // Check password
                var passwordValid = await userManager.CheckPasswordAsync(user, userDto.Password);

                if (!passwordValid) return NotFound();

                // Return accept is everything is good
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }
}
