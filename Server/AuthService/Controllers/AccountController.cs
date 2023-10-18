using AuthService.Features.Responses.Identity;
using AuthService.Infrastructure.Data.Dtos;
using AuthService.Infrastructure.Data.Identity.Entities;
using AuthService.Infrastructure.Services.Identity.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AuthService.Controllers
{
    [Route("api/a/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
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
                    if (item.Code == _userManager.ErrorDescriber.InvalidUserName(user.UserName).Code)
                    {
                        createUserResponse.ErrorList.Add(item.Description);
                    }
                }
                return Ok(createUserResponse);
            }
            createUserResponse.Message = "User successfully created.";
            createUserResponse.Email = user.Email;
            createUserResponse.DisplayName = user.DisplayName;
            createUserResponse.Token = _tokenService.CreateToken(user);

            return Ok(createUserResponse);
        }

        [HttpPost("validateToken")]
        public async Task<ActionResult<ValidateTokenResponse>> ValidateTokenAsync(ValidateTokenDto validateTokenDto)
        {
            string jwtToken = validateTokenDto.Token;

            if (jwtToken == null)
            {
                return BadRequest("JWT token is null.");
            }

            var isValid = await _tokenService.ValidateTokenAsync(jwtToken);
            if (!isValid)
            {
                return BadRequest("Invalid Token.");
            }

            return Ok(new ValidateTokenResponse { IsValid = isValid });
        }

        [HttpPost("testAuth")]
        [Authorize]
        public ActionResult<string> TestingAuth(UserLoginDto loginDto)
        {
            return "success!";
        }
    }
}