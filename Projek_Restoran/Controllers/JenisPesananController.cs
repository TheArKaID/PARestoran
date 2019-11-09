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
    public class JenisPesananController : Controller
    {
        private readonly WEB_ProjekAkhirContext _context;

        public JenisPesananController(WEB_ProjekAkhirContext context)
        {
            _context = context;
        }

        // GET: JenisPesanan
        public async Task<IActionResult> Index()
        {
            return View(await _context.JenisPesanan.ToListAsync());
        }

        // GET: JenisPesanan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisPesanan = await _context.JenisPesanan
                .FirstOrDefaultAsync(m => m.IdJenisPesanan == id);
            if (jenisPesanan == null)
            {
                return NotFound();
            }

            return View(jenisPesanan);
        }

        // GET: JenisPesanan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JenisPesanan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdJenisPesanan,NamaJenisPesanan")] JenisPesanan jenisPesanan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jenisPesanan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jenisPesanan);
        }

        // GET: JenisPesanan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisPesanan = await _context.JenisPesanan.FindAsync(id);
            if (jenisPesanan == null)
            {
                return NotFound();
            }
            return View(jenisPesanan);
        }

        // POST: JenisPesanan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdJenisPesanan,NamaJenisPesanan")] JenisPesanan jenisPesanan)
        {
            if (id != jenisPesanan.IdJenisPesanan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jenisPesanan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JenisPesananExists(jenisPesanan.IdJenisPesanan))
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
            return View(jenisPesanan);
        }

        // GET: JenisPesanan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jenisPesanan = await _context.JenisPesanan
                .FirstOrDefaultAsync(m => m.IdJenisPesanan == id);
            if (jenisPesanan == null)
            {
                return NotFound();
            }

            return View(jenisPesanan);
        }

        // POST: JenisPesanan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jenisPesanan = await _context.JenisPesanan.FindAsync(id);
            _context.JenisPesanan.Remove(jenisPesanan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JenisPesananExists(int id)
        {
            return _context.JenisPesanan.Any(e => e.IdJenisPesanan == id);
        }
    }
}
