using MimeKit;
using IGS.Domain.Entity;
using IGS.DAL.Interfaces;
using IGS.Domain.Response;
using IGS.Domain.Extensions;
using System.Security.Claims;
using IGS.Domain.ViewModels.Account;
using StatusCode = IGS.Domain.Enums.StatusCode;

namespace IGS.Service.Implementations
{
	public class AccountService
	{
		private readonly IUserRepository _userRepository;
		private readonly IProfileRepository _profileRepository;

		public AccountService(IUserRepository userRepository, IProfileRepository profileRepository)
		{
			_userRepository = userRepository;
			_profileRepository = profileRepository;
		}

		public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
		{
			try
			{
				User user = await _userRepository.GetByLogin(model.Login);
				if (user != null)
				{
					return new BaseResponse<ClaimsIdentity>()
					{
						Description = "Пользователь с таким логином уже найден",
						StatusCode = StatusCode.AddElementError,
					};
				}

				user = await _userRepository.GetByEmail(model.EmailAdress);

				if (user != null)
				{
					return new BaseResponse<ClaimsIdentity>()
					{
						Description = "Пользователь с такой почтой уже найден",
						StatusCode = StatusCode.AddElementError,
					};
				}

				int code = RandomGenerator.GenerateCodeAuthenticator();

				try
				{
					MimeMessage message = new MimeMessage();
					message.From.Add(new MailboxAddress("IndieGameSpace", "email"));
					message.To.Add(new MailboxAddress(" ", model.EmailAdress));
					message.Subject = "Подтверждение регистрации";
					message.Body = new BodyBuilder()
					{
						HtmlBody = "<div style=\"color: black; font-size : 20px; \">Ссылка для подтверждения регистрации</div>"
						+ "<div style=\"color: blue; font-size : 20px; \"><u>Подтвердить почту</u></div>",

					}.ToMessageBody();

					using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
					{
						client.Connect("smtp.yandex.ru", 465, true);
						client.Authenticate("email", "password"); 
						client.Send(message);
						client.Disconnect(true);
					}
				}
				catch (Exception ex) { }

				Role role = default;
				if (model.IUser && model.ICreator)
					role = Role.All;
				else
					role = model.IUser ? Role.Gamer : Role.Creator;

				user = new User()
				{
					Login = model.Login,
					Password = HashHelper.HashPassword(model.Password),
					Email = model.EmailAdress,
					AuthenticationPassed = 0,
					Role = role.ToString(),
				};
				await _userRepository.Create(user);
				user = await _userRepository.GetByLogin(model.Login);

				Profile profile = new Profile()
				{
					Id = user.Id,
					Name = model.Login,
				};

				await _profileRepository.Create(profile);

				ClaimsIdentity result = Authenticate(user);

				return new BaseResponse<ClaimsIdentity>()
				{
					Data = result,
					Description = "Пользователь добавлен",
					StatusCode = StatusCode.OperationSuccess,
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<ClaimsIdentity>()
				{
					Description = ex.Message,
					StatusCode = StatusCode.InternalServerError,
				};
			}
		}

		public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
		{
			try
			{
				User user = await _userRepository.GetByLogin(model.Login);

				if (user == null)
				{
					user = await _userRepository.GetByEmail(model.Login);
					if (user == null)
					{
						return new BaseResponse<ClaimsIdentity>()
						{
							Description = "Пользователь не найден",
							StatusCode = StatusCode.FoundElementError,
						};
					}
				}

				if (user.Password != HashHelper.HashPassword(model.Password))
				{
					return new BaseResponse<ClaimsIdentity>
					{
						Description = "Неверный пароль",
					};
				}

				ClaimsIdentity result = Authenticate(user);

				return new BaseResponse<ClaimsIdentity>()
				{
					Data = result,
					StatusCode = StatusCode.OperationSuccess,
				};
			}
			catch (Exception ex)
			{
				return new BaseResponse<ClaimsIdentity>()
				{
					Description = ex.Message,
					StatusCode = StatusCode.InternalServerError,
				};
			}
		}

		private ClaimsIdentity Authenticate(User user)
		{
			var claims = new List<Claim> {
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
			};
			return new ClaimsIdentity((System.Security.Principal.IIdentity?)Thread.CurrentPrincipal, claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
		}

		public async Task<BaseResponse<User>> GetUser(int id)
		{
			BaseResponse<User> response = new BaseResponse<User>();
			try
			{
				User user = await _userRepository.GetById(id);

				if (user == null)
				{
					response.Description = "Данный пользователь не найден";
					response.StatusCode = StatusCode.UserNotFound;
				}

				response.Data = user;
				return response;
			}
			catch (Exception ex)
			{
				return new BaseResponse<User>
				{
					Description = $"[GetUser] : {ex.Message}"
				};
			}
		}

		public async Task<BaseResponse<List<User>>> GetUsers()
		{
			BaseResponse<List<User>> response = new BaseResponse<List<User>>();
			try
			{
				List<User> users = await _userRepository.Select();

				if (users.Count == 0)
				{
					response.Description = "Отсутсвуют элементы";
					response.StatusCode = StatusCode.EmptyCollection;
				}

				response.Data = users;
				return response;
			}
			catch (Exception ex)
			{
				return new BaseResponse<List<User>>
				{
					Description = $"[GetUser] : {ex.Message}"
				};
			}
		}
	}
}
