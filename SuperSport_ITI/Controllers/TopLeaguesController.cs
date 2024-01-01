using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalProject_ITI.Data;
using FinalProject_ITI.Models;

namespace FinalProject_ITI.Controllers
{
    public class TopLeaguesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TopLeaguesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TopLeagues
        public async Task<IActionResult> Index()
        {
              return _context.topLeagues != null ? 
                          View(await _context.topLeagues.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.topLeagues'  is null.");
        }

        // GET: TopLeagues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.topLeagues == null)
            {
                return NotFound();
            }

            var topLeague = await _context.topLeagues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topLeague == null)
            {
                return NotFound();
            }

            return View(topLeague);
        }

        // GET: TopLeagues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopLeagues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImagePath")] TopLeague topLeague)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topLeague);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topLeague);
        }

        // GET: TopLeagues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.topLeagues == null)
            {
                return NotFound();
            }

            var topLeague = await _context.topLeagues.FindAsync(id);
            if (topLeague == null)
            {
                return NotFound();
            }
            return View(topLeague);
        }

        // POST: TopLeagues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImagePath")] TopLeague topLeague)
        {
            if (id != topLeague.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topLeague);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopLeagueExists(topLeague.Id))
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
            return View(topLeague);
        }

        // GET: TopLeagues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.topLeagues == null)
            {
                return NotFound();
            }

            var topLeague = await _context.topLeagues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topLeague == null)
            {
                return NotFound();
            }

            return View(topLeague);
        }

        // POST: TopLeagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.topLeagues == null)
            {
                return Problem("Entity set 'ApplicationDbContext.topLeagues'  is null.");
            }
            var topLeague = await _context.topLeagues.FindAsync(id);
            if (topLeague != null)
            {
                _context.topLeagues.Remove(topLeague);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopLeagueExists(int id)
        {
          return (_context.topLeagues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
