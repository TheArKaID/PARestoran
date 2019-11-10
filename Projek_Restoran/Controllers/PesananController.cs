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
        public async Task<IActionResult> Index(string Srch, string Jenis, string Meja, string Admin, string Tanggal)
        {
            var listJenis = new List<string>();
            var listMeja = new List<string>();
            var listAdmin = new List<string>();

            var dataJenis = from jenis in _context.JenisPesanan select jenis.NamaJenisPesanan;
            var dataMeja = from meja in _context.Meja select meja.NomorMeja;
            var dataAdmin = from admin in _context.User select admin.Nama;

            listJenis.AddRange(dataJenis.Distinct());
            listMeja.AddRange(dataMeja.Distinct());
            listAdmin.AddRange(dataAdmin.Distinct());

            ViewBag.Jenis = new SelectList(listJenis);
            ViewBag.Meja = new SelectList(listMeja);
            ViewBag.Admin = new SelectList(listAdmin);

            var data = from Data in _context.Pesanan.Include(p => p.IdJenisPesananNavigation).Include(p => p.IdProdukNavigation).Include(p => p.IdUserNavigation).Include(p => p.IdMejaNavigation) select Data;

            if (!string.IsNullOrEmpty(Jenis) || !string.IsNullOrWhiteSpace(Jenis))
            {
                data = data.Where(x => x.IdJenisPesananNavigation.NamaJenisPesanan == Jenis);
            }

            if (!string.IsNullOrEmpty(Meja) || !string.IsNullOrWhiteSpace(Meja))
            {
                data = data.Where(x => x.IdMejaNavigation.NomorMeja == Meja);
            }

            if (!string.IsNullOrEmpty(Admin) || !string.IsNullOrWhiteSpace(Admin))
            {
                data = data.Where(x => x.IdUserNavigation.Nama == Admin);
            }

            if (!string.IsNullOrEmpty(Tanggal))
            {
                DateTime dateTime = Convert.ToDateTime(Tanggal);
                data = data.Where(x => x.Tanggal == dateTime);
            }

            if (!string.IsNullOrEmpty(Srch) || !string.IsNullOrWhiteSpace(Srch))
            {
                data = data.Where(x => x.NamaCustomer.Contains(Srch) || x.IdProdukNavigation.Nama.Contains(Srch) || x.Jumlah.Contains(Srch) || x.IdJenisPesananNavigation.NamaJenisPesanan.Contains(Srch) || x.IdMejaNavigation.NomorMeja.Contains(Srch) || x.Keterangan.Contains(Srch) || x.IdUserNavigation.Nama.Contains(Srch));
            }
            
            return View(await data.ToListAsync());
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
                .Include(p => p.IdUserNavigation)
                .Include(p => p.IdMejaNavigation)
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
            ViewData["IdJenisPesanan"] = new SelectList(_context.JenisPesanan, "IdJenisPesanan", "NamaJenisPesanan");
            ViewData["IdProduk"] = new SelectList(_context.Produk, "IdProduk", "Nama");
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "Nama");
            ViewData["IdMeja"] = new SelectList(_context.Meja, "IdMeja", "NomorMeja");
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
            ViewData["IdMeja"] = new SelectList(_context.Meja, "IdMeja", "IdMeja", pesanan.IdMeja);
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
            ViewData["IdJenisPesanan"] = new SelectList(_context.JenisPesanan, "IdJenisPesanan", "NamaJenisPesanan", pesanan.IdJenisPesanan);
            ViewData["IdProduk"] = new SelectList(_context.Produk, "IdProduk", "Nama", pesanan.IdProduk);
            ViewData["IdUser"] = new SelectList(_context.User, "IdUser", "Nama", pesanan.IdUserNavigation);
            ViewData["IdMeja"] = new SelectList(_context.Meja, "IdMeja", "NomorMeja", pesanan.IdMeja);
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
            ViewData["IdMeja"] = new SelectList(_context.Meja, "IdMeja", "IdMeja", pesanan.IdMeja);
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
                .Include(p => p.IdUserNavigation)
                .Include(p => p.IdMejaNavigation)
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
