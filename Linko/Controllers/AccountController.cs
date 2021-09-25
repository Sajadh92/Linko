using Linko.Application;
using Linko.Domain;
using Linko.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Linko.Controllers
{
    [Route("API/[controller]/[action]")]
    [ApiController]
    public class AccountController : MasterController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login(LoginDto data)
        {
            if (data.Username.IsEmpty())
                return Response(false, Message.InvalidUsername);

            if (!data.Password.IsPasswordStrength())
                return Response(false, Message.PasswordNotStrength);

            var response = _accountService.Login(data);

            return Response(true);
        }
    }
}
