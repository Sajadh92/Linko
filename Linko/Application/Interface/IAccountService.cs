using Linko.Domain;
using System.Threading.Tasks;

namespace Linko.Application
{
    public interface IAccountService
    {
        public Task<UserProfile> Login(LoginDto data);
        public Task<UserProfile> Register(RegisterDto data);
        public Task<UserProfile> VerificationEmail(VerificationEmailDto data);
    }
}
