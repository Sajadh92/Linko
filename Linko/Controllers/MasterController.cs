using Linko.Domain;
using Linko.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace Linko.Controllers
{
    [ApiController]
    public class MasterController : ControllerBase
    {
        protected UserJWTClaimDto UserManager
        {
            get
            {
                // reading claim "UserProfile" from JWT Token
                string user = HttpContext.User.Claims
                    .Where(x => x.Type == Key.Lookup[Key.UserProfile])
                    .FirstOrDefault().Value;

                // if claim is exists then deserialize it 
                if (!string.IsNullOrWhiteSpace(user))
                {
                    // TODO: get json of user profile from hash string 

                    return JsonConvert.DeserializeObject<UserJWTClaimDto>(user);
                }

                // if no claim is found return null means the user is not logged in
                return null;
            }
        }

        public new IActionResult Response(bool success)
        {
            return Ok(new { success });
        }
        public new IActionResult Response(bool success, object data)
        {
            return Ok(new { success, data });
        }

        public new IActionResult Response(bool success, string msgCode)
        {
            return Ok(new { success, msg = Message.MsgDictionary[UserManager.Lang][msgCode] });
        }

        public new IActionResult Response(bool success, string msgCode, object data)
        {
            return Ok(new { success, msg = Message.MsgDictionary[UserManager.Lang][msgCode], data });
        }
    }
}
