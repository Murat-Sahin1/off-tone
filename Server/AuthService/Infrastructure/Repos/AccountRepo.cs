using System.Security.Claims;
using AuthService.Extensions.Identity;
using AuthService.Features.Responses.Identity;
using AuthService.Infrastructure.Data.Identity.Dtos;
using AuthService.Infrastructure.Data.Identity.Dtos.User;
using AuthService.Infrastructure.Data.Identity.Dtos.User.Update;
using AuthService.Infrastructure.Data.Identity.Entities;
using AuthService.Infrastructure.Services.Identity.Token;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Repos{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountRepo(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService){
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;    
        }

        public async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<UserDetailsDto> GetUserDetailsAsync(ClaimsPrincipal user)
        {
            var returnedUser = await _userManager.FindUserFromClaimsPrincipalByEmail(user);
            
            if(returnedUser == null){
                return null;
            }

            return new UserDetailsDto{
                DisplayName = returnedUser.DisplayName,
                UserName = returnedUser.UserName,
                Email = returnedUser.Email
            };
        }

        public async Task<UserReadDto> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null){
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded){
                return null;
            }

            return new UserReadDto{
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
            };
        }

        public Task<CreateUserResponse> RegisterAsync(UserRegisterDto registerUserDto)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateDisplayNameDto> UpdateDisplayNameAsync(UpdateDisplayNameDto updateDisplayNameDto)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserNameResponse> UpdateUserNameAsync(UpdateUserNameDto updateUserNameDto)
        {
            throw new NotImplementedException();
        }

        public Task<ValidateTokenResponse> ValidateTokenAsync(ValidateTokenDto validateTokenDto)
        {
            throw new NotImplementedException();
        }
    }
}