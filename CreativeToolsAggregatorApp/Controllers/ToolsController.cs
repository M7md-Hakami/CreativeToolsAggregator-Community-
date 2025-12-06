using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CreativeToolsAggregatorApp.Data;
using CreativeToolsAggregatorApp.Models;

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
            return View(await _context.Tools.ToListAsync());
        }

        // GET: Tools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tools = await _context.Tools
                .FirstOrDefaultAsync(m => m.id == id);
            if (tools == null)
            {
                return NotFound();
            }

            return View(tools);
        }

        // GET: Tools/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,description,link,tag")] Tools tools)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tools);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tools);
        }

        // GET: Tools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tools = await _context.Tools.FindAsync(id);
            if (tools == null)
            {
                return NotFound();
            }
            return View(tools);
        }

        // POST: Tools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,description,link,tag")] Tools tools)
        {
            if (id != tools.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tools);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToolsExists(tools.id))
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
            return View(tools);
        }

        // GET: Tools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tools = await _context.Tools
                .FirstOrDefaultAsync(m => m.id == id);
            if (tools == null)
            {
                return NotFound();
            }

            return View(tools);
        }

        // POST: Tools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tools = await _context.Tools.FindAsync(id);
            if (tools != null)
            {
                _context.Tools.Remove(tools);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToolsExists(int id)
        {
            return _context.Tools.Any(e => e.id == id);
        }
        public async Task<IActionResult> SearchForm()
        {
                        return View();
        }
        public async Task<IActionResult> SearchResult(string SearchString)

        { 
           if (_context.Tools == null)
           {
               return Problem("Entity set 'ApplicationDbContext.Tools'  is null.");
           }
           var filteredTools = await _context.Tools.Where(t => t.name.Contains(SearchString) || t.tag.Contains(SearchString)).ToListAsync();
            return View("Index", filteredTools); 

        }
    }
}
