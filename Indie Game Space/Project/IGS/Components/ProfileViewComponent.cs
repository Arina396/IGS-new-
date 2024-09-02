using IGS.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using IGS.Service.Implementations;
using IGS.Domain.ViewModels.Profile;
using System.Security.Claims;

namespace IGS.Components
{
	public class ProfileViewComponent : ViewComponent
	{
		private readonly SettingsService _settingsService;

		public ProfileViewComponent(SettingsService settingsService) => _settingsService = settingsService;

		public async Task<IViewComponentResult> InvokeAsync()
		{
			BaseResponse<ProfileViewModel> response = await _settingsService.GetSettingsModel(User.Identity.Name);
			ProfileViewModel profileViewModel = response.Data;
			return View(profileViewModel);
		}
	}
}
