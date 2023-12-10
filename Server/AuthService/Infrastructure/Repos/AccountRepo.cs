using AuthService.Infrastructure.Data.Identity.Dtos.User;

namespace AuthService.Infrastructure.Repos{
    public class AccountRepo : IAccountRepo
    {
        public Task<UserReadDto> Login(UserLoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}