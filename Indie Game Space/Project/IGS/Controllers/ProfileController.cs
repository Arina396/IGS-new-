using IGS.Domain.Response;
using IGS.Domain.ViewModels.Profile;
using IGS.Service.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace IGS.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SettingsService _settingsService;

        public ProfileController(SettingsService settingsService) => _settingsService = settingsService;

        public async Task<IActionResult> Main()
        {
			BaseResponse<ProfileViewModel> response = await _settingsService.GetSettingsModel(User.Identity.Name);
            ProfileViewModel profileViewModel = response.Data;
			ViewData["imagePath"] = profileViewModel.ImageName;
			return View(profileViewModel);
        }
    }
}
