using Oostel.Common.Enums;

namespace Oostel.API.ViewModels.UserAuthenticationsVM
{
    public class RegisterUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public RegisterRole RoleType { get; set; }
    }
}
