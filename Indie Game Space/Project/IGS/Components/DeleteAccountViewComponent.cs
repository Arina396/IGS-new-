using Microsoft.AspNetCore.Mvc;

namespace IGS.Components
{
    public class DeleteAccountViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
