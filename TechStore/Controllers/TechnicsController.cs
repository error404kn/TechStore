using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;
using TechStore.Models;

namespace TechStore.Controllers
{
    public class TechnicsController : Controller
    {
        private readonly TechStoreContext _context;

        public TechnicsController(TechStoreContext context)
        {
            _context = context;
        }

        // GET: Technics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Technics.ToListAsync());
        }

        // GET: Technics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technics = await _context.Technics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (technics == null)
            {
                return NotFound();
            }

            return View(technics);
        }

        // GET: Technics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Technics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModelName,Data,Description,Price,Manufacturer,ImageUrl")] Technics technics)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technics);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(technics);
        }

        // GET: Technics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technics = await _context.Technics.FindAsync(id);
            if (technics == null)
            {
                return NotFound();
            }
            return View(technics);
        }

        // POST: Technics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModelName,Data,Description,Price,Manufacturer,ImageUrl")] Technics technics)
        {
            if (id != technics.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technics);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechnicsExists(technics.Id))
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
            return View(technics);
        }

        // GET: Technics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technics = await _context.Technics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (technics == null)
            {
                return NotFound();
            }

            return View(technics);
        }

        // POST: Technics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technics = await _context.Technics.FindAsync(id);
            if (technics != null)
            {
                _context.Technics.Remove(technics);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechnicsExists(int id)
        {
            return _context.Technics.Any(e => e.Id == id);
        }
    }
}
