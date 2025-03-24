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
    public class PricesController : Controller
    {
        private readonly IService<Price> _service;

        public PricesController(IService<Price> service)
        {
            _service = service;
        }

        // GET: Admin/Prices
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Admin/Prices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var price = await _service.GetAsync(m => m.Id == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Admin/Prices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Prices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Price price, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                price.Image = await FileHelper.FileLoaderAsync(Image, "/img/price/");
                _service.Add(price);
                await _service.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: Admin/Prices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _service.FindAsync(id.Value);
            if (price == null)
            {
                return NotFound();
            }
            return View(price);
        }

        // POST: Admin/Prices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Price price, IFormFile? Image, bool cbResmiSil = false)
        {
            if (id != price.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (cbResmiSil)
                    {
                        FileHelper.FileRemover(price.Image, "/img/price/");
                        price.Image = string.Empty;
                    }
                    if (Image is not null)
                    {
                        price.Image = await FileHelper.FileLoaderAsync(Image, "/img/price/");
                    }
                    _service.Update(price);
                    await _service.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceExists(price.Id))
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
            return View(price);
        }

        // GET: Admin/Prices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _service.GetAsync(m => m.Id == id);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Admin/Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var price = await _service.FindAsync(id);
            if (price != null)
            {
                FileHelper.FileRemover(price.Image, "/img/price/");
                _service.Delete(price);
            }

            await _service.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceExists(int id)
        {
            var price = _service.GetAsync(e => e.Id == id);
            if (price == null)
            {
                return false;
            }
            return true;
        }
    }
}
