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

namespace SecurityCamera.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class ServicesController : Controller
    {
        private readonly IService<Services> _service;

        public ServicesController(IService<Services> service)
        {
            _service = service;
        }

        // GET: Admin/Services
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Admin/Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var services = await _service.GetAsync(m => m.Id == id);
            if (services == null)
            {
                return NotFound();
            }

            return View(services);
        }

        // GET: Admin/Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Services services)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(services);
                await _service.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(services);
        }

        // GET: Admin/Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var services = await _service.FindAsync(id.Value);
            if (services == null)
            {
                return NotFound();
            }
            return View(services);
        }

        // POST: Admin/Services/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Services services)
        {
            if (id != services.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(services);
                    await _service.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicesExists(services.Id))
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
            return View(services);
        }

        // GET: Admin/Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var services = await _service.GetAsync(m => m.Id == id);
            if (services == null)
            {
                return NotFound();
            }

            return View(services);
        }

        // POST: Admin/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var services = await _service.FindAsync(id);
            if (services != null)
            {
                _service.Delete(services);
            }

            await _service.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicesExists(int id)
        {
            var service = _service.GetAsync(e => e.Id == id);
            if (service == null)
            {
                return false;
            }
            return true;
        }
    }
}
