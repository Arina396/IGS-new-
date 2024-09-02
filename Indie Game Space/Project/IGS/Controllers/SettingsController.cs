using IGS.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using IGS.Service.Implementations;
using IGS.Domain.ViewModels.Profile;
using IGS.Domain.ViewModels.Settings;

namespace IGS.Controllers
{
    public class SettingsController : Controller
    {
        private readonly SettingsService _settingsService;

        public SettingsController(SettingsService settingsService) => _settingsService = settingsService;

        [HttpPost]
        public async Task<IActionResult> SaveProfile(ProfileViewModel model)
        {
            BaseResponse<ProfileViewModel> response = await _settingsService.SaveProfile(User.Identity.Name, model);
            return NoContent();
        }

        public async Task<IActionResult> Settings(int componentId)
        {
			try
			{
				BaseResponse<ProfileViewModel> profileViewModel = await _settingsService.GetSettingsModel(User.Identity.Name);
				ViewData["imagePath"] = profileViewModel.Data.ImageName;
			}
			catch (Exception ex) { }
			SettingsViewModel model = new SettingsViewModel(componentId);
            return View(model);
        }
    }
}