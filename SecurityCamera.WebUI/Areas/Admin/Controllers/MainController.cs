using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityCamera.Core.Entities;
using SecurityCamera.Service.IService;
using SecurityCamera.WebUI.Areas.Admin.Models;

namespace SecurityCamera.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainController : Controller
    {
        private readonly IService<AppUser> _service;

        public MainController(IService<AppUser> service)
        {
            _service = service;
        }

        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

        [HttpGet,Authorize]
        public async Task<IActionResult> MyProfile()
        {
            AppUser user = await _service.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
            if (user == null)
            {
                return NotFound();
            }
            var model = new MyProfileViewModel()
            {
                Email = user.Email,
                Phone1 = user.Phone1,
                Phone2 = user.Phone2,
                Name = user.Name,
                Address = user.Address,
                IsAdmin = user.IsAdmin,
            };
            return View(model);
        }

        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyProfile(MyProfileViewModel myProfileViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser user = await _service.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
                    if (user is not null)
                    {
                        user.Email = myProfileViewModel.Email;
                        user.Name = myProfileViewModel.Name;
                        user.Phone1 = myProfileViewModel.Phone1;
                        user.Phone2 = myProfileViewModel.Phone2;
                        user.Address = myProfileViewModel.Address;
                        user.IsAdmin = myProfileViewModel.IsAdmin;
                        _service.Update(user);
                        var sonuc = await _service.SaveChangesAsync();
                        if (sonuc > 0)
                        {
                            TempData["Message"] = "Profil bilgileriniz güncellenmiştir.";
                            return RedirectToAction("MyProfile");
                        }
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata  Oluştu!");
                }
            }
            return View(myProfileViewModel);
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> MyPasswordChange()
        {
            return View();
        }

        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MyPasswordChange(MyPasswordChangeViewModel myPasswordChangeViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser user = await _service.GetAsync(x => x.UserGuid.ToString() == HttpContext.User.FindFirst("UserGuid").Value);
                    if (user is not null)
                    {
                        user.Password = myPasswordChangeViewModel.Password;
                        user.Password = myPasswordChangeViewModel.RePassword;
                        _service.Update(user);
                        var sonuc = await _service.SaveChangesAsync();
                        if (sonuc > 0)
                        {
                            TempData["Message"] = "Profil şifreniz güncellenmiştir.";
                            return RedirectToAction("MyPasswordChange");
                        }
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Hata  Oluştu!");
                }
            }
            return View(myPasswordChangeViewModel);
        }

        [Authorize]
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }

}
