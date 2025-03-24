using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using SecurityCamera.Core.Entities;
using SecurityCamera.Data;
using SecurityCamera.Service.IService;
using SecurityCamera.WebUI.Utils;

namespace SecurityCamera.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class GaleriesController : Controller
    {
        private readonly IService<Galery> _service;
        private readonly IWebHostEnvironment _environment;

        public GaleriesController(IService<Galery> service)
        {
            _service = service;
        }

        // GET: Admin/Galeries
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Admin/Galeries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galery = await _service.GetAsync(m => m.Id == id);
            if (galery == null)
            {
                return NotFound();
            }

            return View(galery);
        }

        // GET: Admin/Galeries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Galeries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Galery galery, IFormFile? Image1, IFormFile? Image2, IFormFile? Image3, IFormFile? Image4, IFormFile? Image5)
        {
            if (ModelState.IsValid)
            {
                galery.Image1 = await FileHelper.FileLoaderAsync(Image1, "/img/galery/");
                galery.Image2 = await FileHelper.FileLoaderAsync(Image2, "/img/galery/");
                galery.Image3 = await FileHelper.FileLoaderAsync(Image3, "/img/galery/");
                galery.Image4 = await FileHelper.FileLoaderAsync(Image4, "/img/galery/");
                galery.Image5 = await FileHelper.FileLoaderAsync(Image5, "/img/galery/");
                //int say = 1;
                //foreach (var item in Files)
                //{
                //    string fileName = Upload(item);
                //    if (say == 1)
                //        galery.Image1 = fileName;
                //    else if (say == 2)
                //        galery.Image2 = fileName;
                //    else if (say == 3)
                //        galery.Image3 = fileName;
                //    else if (say == 4)
                //        galery.Image4 = fileName;
                //    say++;
                //}

                await _service.AddAsync(galery);
                await _service.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(galery);
        }

        private string Upload(IFormFile file)
        {
            string filename = null;
            if (file != null)
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "/img/galery/");
                filename = Guid.NewGuid().ToString() + "" + file.FileName;
                string filePath = Path.Combine(uploadDir, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return filename;
        }

        // GET: Admin/Galeries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galery = await _service.FindAsync(id.Value);
            if (galery == null)
            {
                return NotFound();
            }
            return View(galery);
        }

        // POST: Admin/Galeries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Galery galery, IFormFile? Image1, IFormFile? Image2, IFormFile? Image3, IFormFile? Image4, IFormFile? Image5, bool cbResmiSil1 = false, bool cbResmiSil2 = false, bool cbResmiSil3 = false, bool cbResmiSil4 = false, bool cbResmiSil5 = false)
        {
            if (id != galery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (cbResmiSil1)
                    {
                        FileHelper.FileRemover(galery.Image1, "/img/galery/");
                        galery.Image1 = string.Empty;
                    }
                    if (Image1 is not null)
                    {
                        galery.Image1 = await FileHelper.FileLoaderAsync(Image1, "/img/galery/");
                    }
                    if (cbResmiSil2)
                    {
                        FileHelper.FileRemover(galery.Image2, "/img/galery/");
                        galery.Image2 = string.Empty;
                    }
                    if (Image2 is not null)
                    {
                        galery.Image2 = await FileHelper.FileLoaderAsync(Image2, "/img/galery/");
                    }
                    if (cbResmiSil3)
                    {
                        FileHelper.FileRemover(galery.Image3, "/img/galery/");
                        galery.Image3 = string.Empty;
                    }
                    if (Image3 is not null)
                    {
                        galery.Image3 = await FileHelper.FileLoaderAsync(Image3, "/img/galery/");
                    }
                    if (cbResmiSil4)
                    {
                        FileHelper.FileRemover(galery.Image4, "/img/galery/");
                        galery.Image4 = string.Empty;
                    }
                    if (Image4 is not null)
                    {
                        galery.Image4 = await FileHelper.FileLoaderAsync(Image4, "/img/galery/");
                    }
                    if (cbResmiSil5)
                    {
                        FileHelper.FileRemover(galery.Image5, "/img/galery/");
                        galery.Image5 = string.Empty;
                    }
                    if (Image5 is not null)
                    {
                        galery.Image5 = await FileHelper.FileLoaderAsync(Image5, "/img/galery/");
                    }
                    _service.Update(galery);
                    await _service.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GaleryExists(galery.Id))
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
            return View(galery);
        }

        // GET: Admin/Galeries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var galery = await _service.GetAsync(m => m.Id == id);
            if (galery == null)
            {
                return NotFound();
            }

            return View(galery);
        }

        // POST: Admin/Galeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var galery = await _service.FindAsync(id);
            if (galery != null)
            {
                if (!string.IsNullOrEmpty(galery.Image1) || !string.IsNullOrEmpty(galery.Image2) || !string.IsNullOrEmpty(galery.Image3) || !string.IsNullOrEmpty(galery.Image4) || !string.IsNullOrEmpty(galery.Image5))
                {
                    FileHelper.FileRemover(galery.Image1, "/img/galery/");
                    FileHelper.FileRemover(galery.Image2, "/img/galery/");
                    FileHelper.FileRemover(galery.Image3, "/img/galery/");
                    FileHelper.FileRemover(galery.Image4, "/img/galery/");
                    FileHelper.FileRemover(galery.Image5, "/img/galery/");
                }
                _service.Delete(galery);
            }

            await _service.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GaleryExists(int id)
        {
            var galery = _service.GetAsync(e => e.Id == id);
            if (galery == null)
            {
                return false;
            }
            return true;
        }
    }
}
