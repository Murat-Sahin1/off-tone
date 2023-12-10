using AuthService.Extensions.Identity;
using AuthService.Features.Responses.Identity;
using AuthService.Infrastructure.Data.Identity.Dtos;
using AuthService.Infrastructure.Data.Identity.Dtos.User;
using AuthService.Infrastructure.Data.Identity.Dtos.User.Update;
using AuthService.Infrastructure.Data.Identity.Entities;
using AuthService.Infrastructure.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/a/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;
        public AccountController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetailsAsync()
        {
            return await _accountRepo.GetUserDetailsAsync(User);
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _accountRepo.CheckEmailExistsAsync(email);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserReadDto>> LoginAsync(UserLoginDto loginDto)
        {
            return await _accountRepo.LoginAsync(loginDto);
        }

        /* 
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
        public ActionResult<string> TestingToken(UserLoginDto loginDto)
        {
            return "success!";
        }

        [HttpPost("userName")]
        [Authorize]
        public async Task<ActionResult<UpdateUserNameResponse>> UpdateUserName(UpdateUserNameDto userNameDto)
        {
            var user = await _userManager.FindByNameAsync(userNameDto.UserName);

            if(user == null)
            {
                return Unauthorized();
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, userNameDto.Password, false);
            
            if(!signInResult.Succeeded)
            {
                return Unauthorized();
            }

            user.UserName = userNameDto.NewUserName;
            var result = await _userManager.UpdateAsync(user);

            if(!result.Succeeded)
            {
                return new UpdateUserNameResponse
                {
                    IsSuccess = false,
                    Message = "Username couldn't be updated.",
                };
            }

            return Ok(new UpdateUserNameResponse
            {
                IsSuccess = true,
                Message = "Username is successfully updated!",
                NewUserName = user.UserName,
            });
        }

        [HttpPost("displayName")]
        [Authorize]
        public async Task<ActionResult<UpdateDisplayNameResponse>> UpdateDisplayName(UpdateDisplayNameDto displayNameDto)

        {
            var user = await _userManager.FindByNameAsync(displayNameDto.UserName);

            if(user == null)
            {
                return Unauthorized();
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, displayNameDto.Password, false);

            if(!signInResult.Succeeded)
            {
                return Unauthorized();
            }

            user.DisplayName = displayNameDto.NewDisplayName;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new UpdateDisplayNameResponse
                {
                    IsSuccess = false,
                    Message = "Display name couldn't be updated.",
                };
            }

            return Ok(new UpdateDisplayNameResponse
            {
                IsSuccess = true,
                Message = "Display name is successfully updated!",
                NewDisplayName = user.DisplayName,
            });
        }
        */
    }
}