using System.Security.Claims;
using AuthService.Extensions.Identity;
using AuthService.Features.Responses.Identity;
using AuthService.Infrastructure.Data.Identity.Dtos;
using AuthService.Infrastructure.Data.Identity.Dtos.User;
using AuthService.Infrastructure.Data.Identity.Dtos.User.Update;
using AuthService.Infrastructure.Data.Identity.Entities;
using AuthService.Infrastructure.Services.Identity.Token;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastructure.Services.Identity
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
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

            if (returnedUser == null)
            {
                return null;
            }

            return new UserDetailsDto
            {
                DisplayName = returnedUser.DisplayName,
                UserName = returnedUser.UserName,
                Email = returnedUser.Email
            };
        }

        public async Task<UserReadDto> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return null;
            }

            return new UserReadDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
            };
        }

        public async Task<CreateUserResponse> RegisterAsync(UserRegisterDto registerUserDto)
        {
            var user = new AppUser
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.DisplayName,
                DisplayName = registerUserDto.DisplayName,
            };

            var isEmailUnique = await _userManager.FindByEmailAsync(user.Email) == null;

            if (!isEmailUnique)
            {
                return new CreateUserResponse
                {
                    IsSucceeded = false,
                    Message = "User creation is failed.",
                    ErrorList = new List<string>(){
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
                foreach (var item in result.Errors)
                {
                    createUserResponse.ErrorList.Add(item.Description);
                }
                return createUserResponse;
            }

            createUserResponse.Message = "User sucessfully created.";
            createUserResponse.DisplayName = user.DisplayName;
            createUserResponse.Email = user.Email;
            createUserResponse.Token = _tokenService.CreateToken(user);

            return createUserResponse;
        }

        public async Task<UpdateDisplayNameResponse> UpdateDisplayNameAsync(UpdateDisplayNameDto updateDisplayNameDto)
        {
            var user = await _userManager.FindByNameAsync(updateDisplayNameDto.UserName);

            if (user == null)
            {
                return new UpdateDisplayNameResponse
                {
                    IsSuccess = false,
                    Message = "Display name couldn't be updated."
                };
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, updateDisplayNameDto.Password, false);

            if (!signInResult.Succeeded)
            {
                return new UpdateDisplayNameResponse
                {
                    IsSuccess = false,
                    Message = "Display name couldn't be updated."
                };
            }

            user.DisplayName = updateDisplayNameDto.NewDisplayName;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new UpdateDisplayNameResponse
                {
                    IsSuccess = false,
                    Message = "Display name couldn't be updated."
                };
            }

            return new UpdateDisplayNameResponse
            {
                IsSuccess = true,
                Message = "Display name is successfully updated!",
                NewDisplayName = user.DisplayName,
            };
        }

        public async Task<UpdateUserNameResponse> UpdateUserNameAsync(UpdateUserNameDto updateUserNameDto)
        {
            var user = await _userManager.FindByNameAsync(updateUserNameDto.UserName);

            if (user == null)
            {
                return new UpdateUserNameResponse
                {
                    IsSuccess = false,
                    Message = "Username could not be updated."
                };
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, updateUserNameDto.Password, false);

            if (!signInResult.Succeeded)
            {
                return new UpdateUserNameResponse
                {
                    IsSuccess = false,
                    Message = "Username could not be updated."
                };
            }

            user.UserName = updateUserNameDto.NewUserName;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return new UpdateUserNameResponse
                {
                    IsSuccess = false,
                    Message = "Username could not be updated."
                };
            }

            return new UpdateUserNameResponse
            {
                IsSuccess = true,
                Message = "Username is successfully updated!",
                NewUserName = user.UserName,
            };
        }

        public async Task<ValidateTokenResponse> ValidateTokenAsync(ValidateTokenDto validateTokenDto)
        {
            string jwtToken = validateTokenDto.Token;

            if (jwtToken == null)
            {
                return new ValidateTokenResponse { IsValid = false, Message = "Your token is NOT valid." };
            }

            var isValid = await _tokenService.ValidateTokenAsync(jwtToken);

            if (!isValid)
            {
                return new ValidateTokenResponse { IsValid = isValid, Message = "Your token is NOT valid." };
            }
            return new ValidateTokenResponse { IsValid = isValid, Message = "Your token is valid." };
        }
    }
}