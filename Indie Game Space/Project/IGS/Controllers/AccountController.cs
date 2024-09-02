using DNTCaptcha.Core;
using IGS.Domain.Response;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using IGS.Service.Implementations;
using IGS.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Status = IGS.Domain.Enums.StatusCode;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace IGS.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService) => _accountService = accountService;

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateDNTCaptcha(ErrorMessage = "Please enter security code!")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                BaseResponse<ClaimsIdentity> response = await _accountService.Register(model);
                if (response.StatusCode == Status.OperationSuccess)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                    return RedirectToAction("Index", "Home");
                }
                if (response.StatusCode == Status.CodeConfirmation)
                {
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BaseResponse<ClaimsIdentity> response = await _accountService.Login(model);
                if (response.StatusCode == Status.OperationSuccess)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
