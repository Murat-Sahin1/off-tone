using AuthService.Extensions.Identity;
using AuthService.Features.Responses.Identity;
using AuthService.Infrastructure.Data.Identity.Dtos;
using AuthService.Infrastructure.Data.Identity.Dtos.User;
using AuthService.Infrastructure.Data.Identity.Dtos.User.Update;
using AuthService.Infrastructure.Data.Identity.Entities;
using AuthService.Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/a/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetailsAsync()
        {
            return await _accountService.GetUserDetailsAsync(User);
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _accountService.CheckEmailExistsAsync(email);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserReadDto>> LoginAsync(UserLoginDto loginDto)
        {
            return await _accountService.LoginAsync(loginDto);
        }


        [HttpPost("register")]
        public async Task<ActionResult<CreateUserResponse>> Register(UserRegisterDto registerUserDto)
        {
            return await _accountService.RegisterAsync(registerUserDto);
        }

        [HttpPost("displayName")]
        [Authorize]
        public async Task<ActionResult<UpdateDisplayNameResponse>> UpdateDisplayName(UpdateDisplayNameDto displayNameDto)
        {
            return await _accountService.UpdateDisplayNameAsync(displayNameDto);
        }

        [HttpPost("userName")]
        [Authorize]
        public async Task<ActionResult<UpdateUserNameResponse>> UpdateUserName(UpdateUserNameDto userNameDto)
        {
            return await _accountService.UpdateUserNameAsync(userNameDto);
        }

        
        [HttpPost("validateToken")]
        public async Task<ActionResult<ValidateTokenResponse>> ValidateTokenAsync(ValidateTokenDto validateTokenDto)
        {
            return await _accountService.ValidateTokenAsync(validateTokenDto);
        }
        
        /* 
        [HttpPost("testAuth")]
        [Authorize]
        public ActionResult<string> TestingToken(UserLoginDto loginDto)
        {
            return "success!";
        }
        */
    }
}