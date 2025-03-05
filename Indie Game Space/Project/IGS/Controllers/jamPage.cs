using Microsoft.AspNetCore.Mvc;

namespace IGS.Controllers
{
    public class jamPage : Controller
    {
        public IActionResult JamPage()
        {
            return View();
        }
    }
}
