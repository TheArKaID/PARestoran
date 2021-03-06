﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projek_Restoran.Models;

namespace Projek_Restoran.Controllers
{
    public class KategoriController : Controller
    {
        private readonly WEB_ProjekAkhirContext _context;

        public KategoriController(WEB_ProjekAkhirContext context)
        {
            _context = context;
        }

        // GET: Kategori
        public async Task<IActionResult> Index(string srch, string sortOrder, string currentFilter, int? pageNumber)
        {
            var menu = from m in _context.Kategori select m;

            //Paged List
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
            int pageSize = 5;

            if (!string.IsNullOrEmpty(srch))
            {
                menu = menu.Where(s => s.NamaKategori.Contains(srch));
            }

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            //Sorting Order
            switch (sortOrder)
            {
                case "name_desc":
                    menu = menu.OrderByDescending(s => s.NamaKategori);
                    break;
                default: //name ascending
                    menu = menu.OrderBy(s => s.NamaKategori);
                    break;
            }

            return View(await PaginatedList<Kategori>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await menu.ToListAsync());
        }

        // GET: Kategori/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori
                .FirstOrDefaultAsync(m => m.IdKategori == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: Kategori/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategori/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKategori,NamaKategori")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        // GET: Kategori/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        // POST: Kategori/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKategori,NamaKategori")] Kategori kategori)
        {
            if (id != kategori.IdKategori)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriExists(kategori.IdKategori))
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
            return View(kategori);
        }

        // GET: Kategori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori
                .FirstOrDefaultAsync(m => m.IdKategori == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategori = await _context.Kategori.FindAsync(id);
            _context.Kategori.Remove(kategori);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriExists(int id)
        {
            return _context.Kategori.Any(e => e.IdKategori == id);
        }
    }
}
