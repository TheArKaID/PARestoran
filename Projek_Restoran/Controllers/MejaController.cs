using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projek_Restoran.Models;

namespace Projek_Restoran.Controllers
{
    public class MejaController : Controller
    {
        private readonly WEB_ProjekAkhirContext _context;

        public MejaController(WEB_ProjekAkhirContext context)
        {
            _context = context;
        }

        // GET: Meja
        public async Task<IActionResult> Index(string srch, string sortOrder, string currentFilter, int? pageNumber)
        {
            var menu = from m in _context.Meja select m;

            if (!string.IsNullOrEmpty(srch))
            {
                menu = menu.Where(s => s.NomorMeja.Contains(srch));
            }

            //membuat Sorting

            ViewData["MejaSortParm"] = sortOrder == "nomeja_desc" ? "Nomeja" : "nomeja_desc";

            switch (sortOrder)
            {
                case "nomeja_desc":
                    menu = menu.OrderByDescending(s => s.NomorMeja);
                    break;
                default: //name ascending
                    menu = menu.OrderBy(s => s.NomorMeja);
                    break;
            }
            //membuat pagedlist
            ViewData["CurrentSort"] = sortOrder;

            if (srch != null)
            {
                pageNumber = 1;
            }
            else
            {
                srch = currentFilter;
            }
            ViewData["CurrentFilter"] = srch;

            //return View(await menu.ToListAsync());
            int pageSize = 5;
            return View(await PaginatedList<Meja>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: Meja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meja = await _context.Meja
                .FirstOrDefaultAsync(m => m.IdMeja == id);
            if (meja == null)
            {
                return NotFound();
            }

            return View(meja);
        }

        // GET: Meja/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meja/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMeja,NomorMeja")] Meja meja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meja);
        }

        // GET: Meja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meja = await _context.Meja.FindAsync(id);
            if (meja == null)
            {
                return NotFound();
            }
            return View(meja);
        }

        // POST: Meja/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMeja,NomorMeja")] Meja meja)
        {
            if (id != meja.IdMeja)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MejaExists(meja.IdMeja))
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
            return View(meja);
        }

        // GET: Meja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meja = await _context.Meja
                .FirstOrDefaultAsync(m => m.IdMeja == id);
            if (meja == null)
            {
                return NotFound();
            }

            return View(meja);
        }

        // POST: Meja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meja = await _context.Meja.FindAsync(id);
            _context.Meja.Remove(meja);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MejaExists(int id)
        {
            return _context.Meja.Any(e => e.IdMeja == id);
        }
    }
}
