using Microsoft.AspNetCore.Mvc;

namespace IGS.Components
{
    public class ConfidentialityViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
