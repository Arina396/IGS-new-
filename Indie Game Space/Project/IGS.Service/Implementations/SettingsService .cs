using IGS.DAL.Interfaces;
using IGS.Domain.Entity;
using IGS.Domain.Response;
using IGS.Domain.ViewModels.Profile;
using Microsoft.AspNetCore.Hosting;

namespace IGS.Service.Implementations
{
	public class SettingsService
	{
		private readonly IUserRepository _userRepository;
		private readonly IProfileRepository _profileRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public SettingsService(IWebHostEnvironment webHostEnvironment, IUserRepository userRepository, IProfileRepository profileRepository)
		{
			_userRepository = userRepository;
			_profileRepository = profileRepository;
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<BaseResponse<ProfileViewModel>> GetSettingsModel(string login)
		{
			try
			{
				User user = await _userRepository.GetByLogin(login);
				Profile profile = await _profileRepository.GetById(user.Id);

				ProfileViewModel profileViewModel = new ProfileViewModel()
				{
					Name = profile.Name,
					ImageName = profile.ImageName,
					ImageFile = null,
					ICreator = user.Role.Equals("Creator"),
					IUser = user.Role.Equals("User"),
					Description = profile.Description,
					Country = "RUSSIA",
					URL = profile.URL,
					GitHubLink = profile.GitHubLink,
				};

				return new BaseResponse<ProfileViewModel>()
				{
					Description = "Профиль получен успешно",
					StatusCode = Domain.Enums.StatusCode.OperationSuccess,
					Data = profileViewModel
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<ProfileViewModel>()
				{
					Description = ex.Message,
					StatusCode = Domain.Enums.StatusCode.NotFoundException,
				};
			}
		}

		public async Task<BaseResponse<ProfileViewModel>> SaveProfile(string login, ProfileViewModel profileModel)
		{
			User user = await _userRepository.GetByLogin(login);
			Profile profile = await _profileRepository.GetById(user.Id);
			string? imageName = null;
			try
			{
				string wwRootPath = _webHostEnvironment.WebRootPath;
				string fileName = Path.GetFileNameWithoutExtension(profileModel.ImageFile.FileName);
				string extension = Path.GetExtension(profileModel.ImageFile.FileName);
				imageName = fileName + extension;
				string path = Path.Combine(wwRootPath + "/Image", imageName);
				using (var fileStream = new FileStream(path, FileMode.Create))
				{
					await profileModel.ImageFile.CopyToAsync(fileStream);
				}
			}
			catch (Exception ex) 
			{ 
				Console.WriteLine($"{ex.Message}");
                imageName = profile.ImageName != null ? profile.ImageName : null;
            }

			profile.Name = profileModel.Name;
			profile.ImageName = imageName;
			profile.Description = profileModel.Description;
			profile.Country = profileModel.Country;
			profile.URL = profileModel.URL;
			profile.GitHubLink = profileModel.GitHubLink;

			bool result = await _profileRepository.SaveProfile(profile);
			Domain.Enums.StatusCode code = result ? Domain.Enums.StatusCode.OperationSuccess : Domain.Enums.StatusCode.AddElementError;
			return new BaseResponse<ProfileViewModel>()
			{
				StatusCode = code,
			};
		}
	}
}
