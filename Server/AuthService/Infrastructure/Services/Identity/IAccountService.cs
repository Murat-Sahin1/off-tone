using System.Security.Claims;
using AuthService.Features.Responses.Identity;
using AuthService.Infrastructure.Data.Identity.Dtos;
using AuthService.Infrastructure.Data.Identity.Dtos.User;
using AuthService.Infrastructure.Data.Identity.Dtos.User.Update;

namespace AuthService.Infrastructure.Services.Identity{
    public interface IAccountService{
        public Task<LoginUserResponse> LoginAsync(UserLoginDto loginDto);
        public Task<UserDetailsDto> GetUserDetailsAsync(ClaimsPrincipal user);
        public Task<bool> CheckEmailExistsAsync(string email);
        public Task<CreateUserResponse> RegisterAsync(UserRegisterDto registerUserDto);
        public Task<ValidateTokenResponse> ValidateTokenAsync(ValidateTokenDto validateTokenDto);
        public Task<UpdateUserNameResponse> UpdateUserNameAsync(UpdateUserNameDto updateUserNameDto);
        public Task<UpdateDisplayNameResponse> UpdateDisplayNameAsync(UpdateDisplayNameDto updateDisplayNameDto);
    }
}