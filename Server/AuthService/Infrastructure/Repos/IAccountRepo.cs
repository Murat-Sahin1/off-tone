using AuthService.Infrastructure.Data.Identity.Dtos.User;

namespace AuthService.Infrastructure.Repos{
    public interface IAccountRepo{
        public Task<UserReadDto> Login(UserLoginDto loginDto);
        /*TO-DO*/
    }
}