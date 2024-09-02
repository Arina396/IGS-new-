using Microsoft.AspNetCore.Mvc;

namespace IGS.Components
{
    public class SafetyViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
