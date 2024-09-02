using IGS.Models;
using IGS.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using IGS.Service.Implementations;
using IGS.Domain.ViewModels.Profile;
using IGS.DAL.Interfaces;
using IGS.Domain.Entity;

namespace IGS.Controllers
{
    public class HomeController : Controller
    {
		private readonly SettingsService _settingsService;
        private readonly IGameRepository _gameRepository;

        public HomeController(SettingsService settingsService, IGameRepository gameRepository)
        {
            _settingsService = settingsService;
            _gameRepository = gameRepository;
        }

		public async Task<IActionResult> Index()
        {
            try
            {
                BaseResponse<ProfileViewModel> profileViewModel = await _settingsService.GetSettingsModel(User.Identity.Name);        
                ViewData["imagePath"] = profileViewModel.Data.ImageName;
            }
            catch (Exception ex) {  }
            var list = await _gameRepository.Select();
            return View(list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }
    }
}