using Linko.Domain;
using System.Threading.Tasks;

namespace Linko.Application
{
    public interface ISecurityService
    {
        public Task<ResObj> Login(LoginDto data);
        public Task<ResObj> Register(RegisterDto data);
        public Task<UserProfile> GetByIdentity(string CallType, string Identity, string PassOrCode = "");
    }
}
