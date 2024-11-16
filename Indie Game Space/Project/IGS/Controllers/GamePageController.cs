using Microsoft.AspNetCore.Mvc;

namespace IGS.Controllers
{
    public class GamePageController : Controller
    {
        public IActionResult GamePage()
        {
            return View();
        }
    }
}
