using CreativeToolsAggregatorApp.Data;
using CreativeToolsAggregatorApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CreativeToolsAggregatorApp.Data;
using CreativeToolsAggregatorApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CreativeToolsAggregatorApp.Controllers
{
    public class ToolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ToolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tools
        public async Task<IActionResult> Index()
        {
            var tools = await _context.Tools.ToListAsync();

            var groups = tools
                .GroupBy(t => string.IsNullOrWhiteSpace(t.tag) ? "Uncategorized" : t.tag.Trim())
                .OrderBy(g => g.Key)
                .Select(g => new ToolsGroupViewModel
                {
                    Tag = g.Key,
                    Items = g.OrderBy(t => t.name).ToList()
                })
                .ToList();

            var model = new ToolsIndexViewModel { Groups = groups };
            return View(model);
        }

        // GET: Tools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tools = await _context.Tools.FirstOrDefaultAsync(m => m.id == id);
            if (tools == null) return NotFound();

            return View(tools);
        }

        // GET: Tools/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var tags = await _context.Tools
                .Where(t => !string.IsNullOrEmpty(t.tag))
                .Select(t => t.tag.Trim())
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();

            ViewData["TagList"] = new SelectList(tags);
            return View();
        }

        // POST: Tools/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,description,link,tag,image")] Tools tools)
        {
            if (ModelState.IsValid)
            {
                tools.tag = tools.tag?.Trim();
                tools.image = tools.image?.Trim();

                _context.Add(tools);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var tags = await _context.Tools
                .Where(t => !string.IsNullOrEmpty(t.tag))
                .Select(t => t.tag.Trim())
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();
            ViewData["TagList"] = new SelectList(tags);

            return View(tools);
        }

        // GET: Tools/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tools = await _context.Tools.FindAsync(id);
            if (tools == null) return NotFound();

            var tags = await _context.Tools
                .Where(t => !string.IsNullOrEmpty(t.tag))
                .Select(t => t.tag.Trim())
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();
            ViewData["TagList"] = new SelectList(tags);

            return View(tools);
        }

        // POST: Tools/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,description,link,tag,image")] Tools formTools)
        {
            if (id != formTools.id) return NotFound();

            if (!ModelState.IsValid)
            {
                var tags = await _context.Tools
                    .Where(t => !string.IsNullOrEmpty(t.tag))
                    .Select(t => t.tag.Trim())
                    .Distinct()
                    .OrderBy(s => s)
                    .ToListAsync();
                ViewData["TagList"] = new SelectList(tags);
                return View(formTools);
            }

            var existing = await _context.Tools.FindAsync(id);
            if (existing == null) return NotFound();

            existing.name = formTools.name;
            existing.description = formTools.description;
            existing.link = formTools.link;
            existing.tag = formTools.tag?.Trim();
            existing.image = formTools.image?.Trim();

            try
            {
                _context.Update(existing);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToolsExists(existing.id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Tools/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tools = await _context.Tools.FirstOrDefaultAsync(m => m.id == id);
            if (tools == null) return NotFound();

            return View(tools);
        }

        // POST: Tools/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tools = await _context.Tools.FindAsync(id);
            if (tools != null)
            {
                _context.Tools.Remove(tools);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ToolsExists(int id) => _context.Tools.Any(e => e.id == id);

        public IActionResult SearchForm() => View();

        public async Task<IActionResult> SearchResult(string SearchString)
        {
            if (_context.Tools == null) return Problem("Entity set 'ApplicationDbContext.Tools'  is null.");

            var filtered = await _context.Tools
                .Where(t => (t.name ?? "").Contains(SearchString) || (t.tag ?? "").Contains(SearchString))
                .ToListAsync();

            var groups = filtered
                .GroupBy(t => string.IsNullOrWhiteSpace(t.tag) ? "Uncategorized" : t.tag.Trim())
                .OrderBy(g => g.Key)
                .Select(g => new ToolsGroupViewModel
                {
                    Tag = g.Key,
                    Items = g.OrderBy(t => t.name).ToList()
                })
                .ToList();

            var model = new ToolsIndexViewModel { Groups = groups };
            return View("Index", model);
        }
    }
}