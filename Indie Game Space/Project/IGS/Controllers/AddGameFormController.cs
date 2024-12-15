using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IGS.Controllers
{
    public class AddGameFormController : Controller
    {
        // GET: AddGameFormControlller
        public ActionResult AddGameForm()
        {
            return View();
        }
    }
}
