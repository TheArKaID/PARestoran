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
    public class PesananController : Controller
    {
        private readonly WEB_ProjekAkhirContext _context;

        public PesananController(WEB_ProjekAkhirContext context)
        {
            _context = context;
        }

        // GET: Pesanan
        public async Task<IActionResult> Index()
        {
            var wEB_ProjekAkhirContext = _context.Pesanan.Include(p => p.IdJenisPesananNavigation).Include(p => p.IdProdukNavigation).Include(p => p.IdUser1).Include(p => p.IdUserNavigation);
            return View(await wEB_ProjekAkhirContext.ToListAsync());
        }

        // GET: Pesanan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pesanan = await _context.Pesanan
                .Include(p => p.IdJenisPesananNavigation)
                .Include(p => p.IdProdukNavigation)
                .Include(p => p.IdUser1)
                .Include(p => p.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdPesanan == id);
            if (pesanan == null)
            {
                return NotFound();
            }

            return View(pesanan);
        }

        // GET: Pesanan/Create
        public IActionResult Create()
        {
            ViewData["IdJenisPesanan"] = new SelectList(_context.JenisPesanan, "IdJenisPesanan", "IdJenisPesanan");
            ViewData["IdProduk"] = new SelectList(_context.Produk, "IdProduk", "IdProduk");
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "IdUser");
            ViewData["IdUser"] = new SelectList(_context.Meja, "IdMeja", "IdMeja");
            return View();
        }

        // POST: Pesanan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPesanan,NamaCustomer,Jumlah,Tanggal,Keterangan,IdProduk,IdJenisPesanan,IdMeja,IdUser")] Pesanan pesanan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pesanan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdJenisPesanan"] = new SelectList(_context.JenisPesanan, "IdJenisPesanan", "IdJenisPesanan", pesanan.IdJenisPesanan);
            ViewData["IdProduk"] = new SelectList(_context.Produk, "IdProduk", "IdProduk", pesanan.IdProduk);
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "IdUser", pesanan.IdUser);
            ViewData["IdUser"] = new SelectList(_context.Meja, "IdMeja", "IdMeja", pesanan.IdUser);
            return View(pesanan);
        }

        // GET: Pesanan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pesanan = await _context.Pesanan.FindAsync(id);
            if (pesanan == null)
            {
                return NotFound();
            }
            ViewData["IdJenisPesanan"] = new SelectList(_context.JenisPesanan, "IdJenisPesanan", "IdJenisPesanan", pesanan.IdJenisPesanan);
            ViewData["IdProduk"] = new SelectList(_context.Produk, "IdProduk", "IdProduk", pesanan.IdProduk);
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "IdUser", pesanan.IdUser);
            ViewData["IdUser"] = new SelectList(_context.Meja, "IdMeja", "IdMeja", pesanan.IdUser);
            return View(pesanan);
        }

        // POST: Pesanan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPesanan,NamaCustomer,Jumlah,Tanggal,Keterangan,IdProduk,IdJenisPesanan,IdMeja,IdUser")] Pesanan pesanan)
        {
            if (id != pesanan.IdPesanan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pesanan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PesananExists(pesanan.IdPesanan))
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
            ViewData["IdJenisPesanan"] = new SelectList(_context.JenisPesanan, "IdJenisPesanan", "IdJenisPesanan", pesanan.IdJenisPesanan);
            ViewData["IdProduk"] = new SelectList(_context.Produk, "IdProduk", "IdProduk", pesanan.IdProduk);
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "IdUser", pesanan.IdUser);
            ViewData["IdUser"] = new SelectList(_context.Meja, "IdMeja", "IdMeja", pesanan.IdUser);
            return View(pesanan);
        }

        // GET: Pesanan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pesanan = await _context.Pesanan
                .Include(p => p.IdJenisPesananNavigation)
                .Include(p => p.IdProdukNavigation)
                .Include(p => p.IdUser1)
                .Include(p => p.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.IdPesanan == id);
            if (pesanan == null)
            {
                return NotFound();
            }

            return View(pesanan);
        }

        // POST: Pesanan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pesanan = await _context.Pesanan.FindAsync(id);
            _context.Pesanan.Remove(pesanan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PesananExists(int id)
        {
            return _context.Pesanan.Any(e => e.IdPesanan == id);
        }
    }
}
