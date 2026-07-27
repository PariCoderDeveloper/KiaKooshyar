using Microsoft.AspNetCore.Mvc;

namespace KiaKooshar.Peresentation.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index ()
        {
            return View ();
        }
    }
}
