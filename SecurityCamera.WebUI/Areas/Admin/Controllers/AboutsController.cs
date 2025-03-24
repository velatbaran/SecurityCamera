using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityCamera.Core.Entities;
using SecurityCamera.Data;
using SecurityCamera.Service.IService;
using SecurityCamera.WebUI.Utils;

namespace SecurityCamera.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class AboutsController : Controller
    {
        private readonly IService<About> _service;

        public AboutsController(IService<About> service)
        {
            _service = service;
        }

        // GET: Admin/Abouts
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Admin/Abouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _service.GetAsync(m => m.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);
        }

        // GET: Admin/Abouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var about = await _service.FindAsync(id.Value);
            if (about == null)
            {
                return NotFound();
            }
            return View(about);
        }

        // POST: Admin/Abouts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, About about, IFormFile? Image, bool cbResmiSil = false)
        {
            if (id != about.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (cbResmiSil)
                    {
                        FileHelper.FileRemover(about.Image, "/img/about/");
                        about.Image = string.Empty;
                    }
                    if (Image is not null)
                    {
                        about.Image = await FileHelper.FileLoaderAsync(Image, "/img/about/");
                    }
                    _service.Update(about);
                    await _service.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        private bool AboutExists(int id)
        {
            var about = _service.GetAsync(e => e.Id == id);
            if (about == null)
            {
                return false;
            }
            return true;
        }
    }
}
