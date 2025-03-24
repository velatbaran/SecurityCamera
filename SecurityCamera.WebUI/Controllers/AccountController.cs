using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityCamera.Core.Entities;
using SecurityCamera.Service.IService;
using SecurityCamera.WebUI.Models;
using System.Security.Claims;

namespace SecurityCamera.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IService<AppUser> _service;

        public AccountController(IService<AppUser> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = await _service.GetAsync(x => x.Email == loginViewModel.Email & x.Password == loginViewModel.Password);
                    if (account == null)
                    {

                        return NotFound("Email veya password hatalı.");
                    }
                    else
                    {
                        var claims = new List<Claim>()
                        {
                            new(ClaimTypes.Name,account.Name),
                            new(ClaimTypes.Role,account.IsAdmin ? "Admin" : "User"),
                            new(ClaimTypes.Email,account.Email),
                            new("Phone1",account.Phone1.ToString()),
                            new("Phone2",account.Phone2.ToString()),
                            new("UserId",account.Id.ToString()),
                            new("UserGuid",account.UserGuid.ToString())
                        };
                        var userIdentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(userPrincipal);
                        return Redirect(string.IsNullOrEmpty(loginViewModel.ReturnUrl) ? "/" : loginViewModel.ReturnUrl);
                    }
                }
                catch (Exception)
                {
                    // loglama
                    ModelState.AddModelError("", "Hata Oluştu!");
                    return View();
                }
            }
            return View(loginViewModel);
        }

    }
}
