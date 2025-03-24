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
    public class CommentsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IService<Comment> _service;
        public CommentsController(DatabaseContext context, IService<Comment> service)
        {
            _context = context;
            _service = service;
        }

        // GET: Admin/Comments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comments.Include(c => c.Galery).ToListAsync());
        }

        // GET: Admin/Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.Include(c => c.Galery).FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Admin/Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _service.FindAsync(id.Value);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["GaleryId"] = new SelectList(_context.Galeries, "Id", "WorkingName", comment.GaleryId);
            return View(comment);
        }

        // POST: Admin/Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(comment);
                    await _service.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["GaleryId"] = new SelectList(_context.Galeries, "Id", "WorkingName", comment.GaleryId);
            return View(comment);
        }

        // GET: Admin/Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.Include(c => c.Galery).FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _service.FindAsync(id);
            if (comment != null)
            {
                _service.Delete(comment);
            }

            await _service.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
