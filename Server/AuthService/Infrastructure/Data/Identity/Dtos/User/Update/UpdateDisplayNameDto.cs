namespace AuthService.Infrastructure.Data.Identity.Dtos.User.Update
{
    public class UpdateDisplayNameDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewDisplayName { get; set; }
    }
}
