using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DadJokes.Data;
using DadJokes.Models;
using Microsoft.AspNetCore.Authorization;

namespace DadJokes.Controllers
{
    public class DadjokesListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DadjokesListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DadjokesLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.DadjokesList.ToListAsync());
        }
        // GET: DadjokesLists/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // Post: DadjokesLists/ShowSearchResult
        public async Task<IActionResult> ShowSearchResult(String SearchPhrase)
        {
            return View("Index", await _context.DadjokesList.Where(j=>j.Answer.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: DadjokesLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dadjokesList = await _context.DadjokesList
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dadjokesList == null)
            {
                return NotFound();
            }

            return View(dadjokesList);
        }

        // GET: DadjokesLists/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: DadjokesLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Question,Answer")] DadjokesList dadjokesList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dadjokesList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dadjokesList);
        }

        // GET: DadjokesLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dadjokesList = await _context.DadjokesList.FindAsync(id);
            if (dadjokesList == null)
            {
                return NotFound();
            }
            return View(dadjokesList);
        }

        // POST: DadjokesLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Question,Answer")] DadjokesList dadjokesList)
        {
            if (id != dadjokesList.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dadjokesList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DadjokesListExists(dadjokesList.ID))
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
            return View(dadjokesList);
        }
        [Authorize]
        // GET: DadjokesLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dadjokesList = await _context.DadjokesList
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dadjokesList == null)
            {
                return NotFound();
            }

            return View(dadjokesList);
        }

        // POST: DadjokesLists/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dadjokesList = await _context.DadjokesList.FindAsync(id);
            _context.DadjokesList.Remove(dadjokesList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DadjokesListExists(int id)
        {
            return _context.DadjokesList.Any(e => e.ID == id);
        }
    }
}
