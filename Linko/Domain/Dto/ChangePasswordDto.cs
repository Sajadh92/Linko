
namespace Linko.Domain
{
    public class ChangePasswordDto
    {
        public string UserIdentity { get; set; }
        public string OldPassword { get; set; }
        public string VerificationCode { get; set; }
        public string NewPassword { get; set; }
        public string Type { get; set; }
    }
}
