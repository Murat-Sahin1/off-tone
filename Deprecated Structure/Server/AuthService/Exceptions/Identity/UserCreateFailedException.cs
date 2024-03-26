using Microsoft.AspNetCore.Identity;

namespace AuthService.Exceptions.Identity
{
    public class UserCreateFailedException : Exception
    {
        public bool _isSuccess {  get; set; }
        public string _message {  get; set; }
        public IEnumerable<IdentityError> _errorList { get; set; }
    }
}
