using Linko.Domain;
using Linko.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Linko.Controllers
{
    [Route("API/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        public IActionResult Login(LoginDto data)
        {
            if (data.Username.IsEmpty())
                return this.Response(false, "InvalidUsername");

            if (!data.Password.IsPasswordStrength())
                return this.Response(false, "InvalidUsername");
        }
    }
}
