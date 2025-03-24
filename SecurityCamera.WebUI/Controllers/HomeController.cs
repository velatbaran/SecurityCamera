using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SecurityCamera.Core.Entities;
using SecurityCamera.Service.IService;
using SecurityCamera.Service.Service;
using SecurityCamera.WebUI.Models;

namespace SecurityCamera.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService<Galery> _serviceGalery;
        private readonly IService<About> _serviceAbout;
        private readonly IService<Contact> _serviceContact;
        private readonly IService<AppUser> _serviceAppUser;
        private readonly IService<Comment> _serviceComment;
        private readonly IService<Services> _serviceServices;
        private readonly IService<Price> _servicePrice;
        private readonly IToastNotification _toastNotification;


        public HomeController(IService<Galery> serviceGalery, IService<AppUser> serviceAppUser, IService<Comment> serviceComment, IToastNotification toastNotification, IService<About> serviceAbout, IService<Services> serviceServices, IService<Price> servicePrice, IService<Contact> serviceContact)
        {
            _serviceGalery = serviceGalery;
            _serviceAppUser = serviceAppUser;
            _serviceComment = serviceComment;
            _toastNotification = toastNotification;
            _serviceAbout = serviceAbout;
            _serviceServices = serviceServices;
            _servicePrice = servicePrice;
            _serviceContact = serviceContact;
        }

        public async Task<IActionResult> Index()
        {
            AppUser _appUser = await _serviceAppUser.GetAsync(x => x.Id == 1);
            ViewBag.Phone1 = _appUser.Phone1;

            var list = (from t in _serviceGalery.GetAll()
                        orderby t.CreatedDate
                        select t).Take(6);

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Contact()
        {

            AppUser _appUser = await _serviceAppUser.GetAsync(x => x.Id == 1);
            ViewBag.Phone1 = _appUser.Phone1;
            //ViewBag.Phone2 = _appUser.Phone2;
            //ViewBag.Email = _appUser.Email;
            //ViewBag.Address = _appUser.Address;

            return View(_appUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(string _name, string _email, string _subject,string _phone, string _message)
        {
            try
            {
                var contact = new Contact()
                {
                    Name = _name,
                    Email = _email,
                    Subject = _subject,
                    Phone = _phone,
                    Message = _message
                };
                _serviceContact.Add(contact);
                await _serviceContact.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Mesajýnýz gönderildi. Teþekkür ederiz!", new ToastrOptions { Title = "Baþarýlý" });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage($"Mesajýnýz gönderilirken hata oluþtu!{ex.Message}", new ToastrOptions { Title = "Hata" });
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> About()
        {
            AppUser _appUser = await _serviceAppUser.GetAsync(x => x.Id == 1);
            ViewBag.Phone1 = _appUser.Phone1;

            About about = await _serviceAbout.GetAsync(x => x.Id == 2);
            var model = new AboutCommentviewModel()
            {
                About = about,
                Comments = await _serviceComment.GetAllAsync(x => x.IsApproved)
            };

            return View(model);
        }

        public async Task<IActionResult> Services()
        {
            AppUser _appUser = await _serviceAppUser.GetAsync(x => x.Id == 1);
            ViewBag.Phone1 = _appUser.Phone1;

            var model = new ServicePriceViewModel()
            {
                Services = await _serviceServices.GetAllAsync(),
                Prices = await _servicePrice.GetAllAsync()
            };

            return View(model);
        }

        public async Task<IActionResult> Galery()
        {
            AppUser _appUser = await _serviceAppUser.GetAsync(x => x.Id == 1);
            ViewBag.Phone1 = _appUser.Phone1;

            return View(await _serviceGalery.GetAllAsync());
        }

        public async Task<IActionResult> GaleryDetail(int? id)
        {
            AppUser _appUser = await _serviceAppUser.GetAsync(x => x.Id == 1);
            ViewBag.Phone1 = _appUser.Phone1;

            if (id == null)
            {
                return NotFound();
            }

            var galery = await _serviceGalery.GetAsync(m => m.Id == id);
            if (galery == null)
            {
                return NotFound();
            }

            Comment comment = new Comment()
            {
                GaleryId = galery.Id
            };

            CommentGaleryViewModel commentGaleryViewModel = new CommentGaleryViewModel()
            {
                Galery = galery,
                Comment = comment
            };

            return View(commentGaleryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Comment(int _galeryId, string _name, string _description)
        {
            try
            {
                Comment comment = new Comment
                {
                    GaleryId = _galeryId,
                    Name = _name,
                    Description = _description,
                    IsApproved = false
                };
                _serviceComment.Add(comment);
                await _serviceComment.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Yorumunuz gönderildi. Teþekkür ederiz!", new ToastrOptions { Title = "Baþarýlý" });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage($"Yorumunuz gönderilirken hata oluþtu!{ex.Message}", new ToastrOptions { Title = "Hata" });
            }
            return RedirectToAction("Index");
        }
    }
}
