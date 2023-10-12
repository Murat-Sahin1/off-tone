using AuthService.Data.Dtos;
using AuthService.Data.Identity.Entities;
using AuthService.Features.Responses.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserReadDto>> Login(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            return new UserReadDto
            {
                Email = user.Email,
                Token = null,
                DisplayName = user.DisplayName,
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<CreateUserResponse>> Register(UserRegisterDto registerUserDto)
        {
            var user = new AppUser
            {
                Email = registerUserDto.Email,
                DisplayName = registerUserDto.DisplayName,
                UserName = registerUserDto.DisplayName,
            };

            var isEmailUnique = await _userManager.FindByEmailAsync(user.Email) == null;

            if (!isEmailUnique)
            {
                return new CreateUserResponse
                {
                    IsSucceeded = false,
                    Message = "User creation is failed.",
                    ErrorList = new List<string>()
                    {
                        "Please use another email.",
                    }
                };
            }

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);
            var createUserResponse = new CreateUserResponse
            {
                IsSucceeded = result.Succeeded
            };
            
            if (!result.Succeeded)
            {
                createUserResponse.Message = "User creation is failed.";
                createUserResponse.ErrorList = new List<string>();

                foreach (var item in result.Errors)
                {
                    if (item.Code == _userManager.ErrorDescriber.PasswordRequiresDigit().Code)
                    {
                        createUserResponse.ErrorList.Add(item.Description);
                    }
                    if (item.Code == _userManager.ErrorDescriber.PasswordRequiresLower().Code)
                    {
                        createUserResponse.ErrorList.Add(item.Description);
                    }
                    if (item.Code == _userManager.ErrorDescriber.PasswordRequiresNonAlphanumeric().Code)
                    {
                        createUserResponse.ErrorList.Add(item.Description);
                    }
                    if (item.Code == _userManager.ErrorDescriber.PasswordRequiresUniqueChars(1).Code)
                    {
                        createUserResponse.ErrorList.Add(item.Description);
                    }
                    if (item.Code == _userManager.ErrorDescriber.PasswordRequiresUpper().Code)
                    {
                        createUserResponse.ErrorList.Add(item.Description);
                    }
                    if (item.Code == _userManager.ErrorDescriber.PasswordTooShort(6).Code)
                    {
                        createUserResponse.ErrorList.Add(item.Description);
                    }
                    if (item.Code == _userManager.ErrorDescriber.DuplicateUserName(user.UserName).Code)
                    {
                        createUserResponse.ErrorList.Add(item.Description);
                    }
                }
                return Ok(createUserResponse);
            }
            createUserResponse.Message = "User successfully created.";
            createUserResponse.Email = user.Email;
            createUserResponse.DisplayName = user.DisplayName;
            createUserResponse.Token = null;

            return Ok(createUserResponse);
        }
    }
}