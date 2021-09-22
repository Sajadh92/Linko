using Microsoft.AspNetCore.Mvc;

namespace Linko.Helper
{
    public static class Extentions
    {
        public static IActionResult Response(this Controller controller, 
            bool success, string msg = "", object data = null)
        {
            return controller.Ok(new { success, msg, data });
        }
    }
}
