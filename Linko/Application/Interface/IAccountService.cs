using Linko.Domain;

namespace Linko.Application
{
    public interface IAccountService
    {
        object Login(LoginDto data);
    }
}
