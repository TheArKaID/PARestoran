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
    public class ProdukController : Controller
    {
        private readonly WEB_ProjekAkhirContext _context;

        public ProdukController(WEB_ProjekAkhirContext context)
        {
            _context = context;
        }

        // GET: Produk
        public async Task<IActionResult> Index(string ktgr, string sortOrder, string currentFilter, int? pageNumber, string srch)
        {
            
            var ketList = new List<string>();
            var ketQuery = from d in _context.Kategori orderby d.NamaKategori select d.NamaKategori;
            ketList.AddRange(ketQuery.Distinct());
            ViewBag.ktgr = new SelectList(ketList);
            var menu = from m in _context.Produk.Include(k => k.IdKategoriNavigation) select m;

            if (!string.IsNullOrEmpty(ktgr))
            {
                menu = menu.Where(x => x.IdKategoriNavigation.NamaKategori == ktgr);
            }

            if (!string.IsNullOrEmpty(srch))
            {
                menu = menu.Where(s => s.Nama.Contains(srch) || s.Harga.ToString().Contains(srch) || s.IdKategoriNavigation.NamaKategori.Contains(srch));
            }


            //sort Order
            ViewData["PesananSortParm"] = String.IsNullOrEmpty(sortOrder) ? "pesanan_desc" : "";
            ViewData["HargaSortParm"] = sortOrder == "Harga" ? "harga_desc" : "Harga";

            switch (sortOrder)
            {
                case "pesanan_desc":
                    menu = menu.OrderByDescending(s => s.Nama);
                    break;
                case "Harga":
                    menu = menu.OrderBy(s => s.Harga);
                    break;
                case "harga_desc":
                    menu = menu.OrderByDescending(s => s.Harga);
                    break;
                default: //name ascending
                    menu = menu.OrderBy(s => s.Nama);
                    break;
            }

            //PagedList
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


            //var wEB_ProjekAkhirContext = _context.Produk.Include(p => p.IdKategoriNavigation);
            //return View(await menu.ToListAsync());
            //PagedSize Return
            int pageSize = 5;
            return View(await PaginatedList<Produk>.CreateAsync(menu.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Produk/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produk = await _context.Produk
                .Include(p => p.IdKategoriNavigation)
                .FirstOrDefaultAsync(m => m.IdProduk == id);
            if (produk == null)
            {
                return NotFound();
            }

            return View(produk);
        }

        // GET: Produk/Create
        public IActionResult Create()
        {
            ViewData["IdKategori"] = new SelectList(_context.Kategori, "IdKategori", "NamaKategori");
            return View();
        }

        // POST: Produk/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduk,Nama,Harga,IdKategori")] Produk produk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKategori"] = new SelectList(_context.Kategori, "IdKategori", "NamaKategori", produk.IdKategori);
            return View(produk);
        }

        // GET: Produk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produk = await _context.Produk.FindAsync(id);
            if (produk == null)
            {
                return NotFound();
            }
            ViewData["IdKategori"] = new SelectList(_context.Kategori, "IdKategori", "NamaKategori", produk.IdKategori);
            return View(produk);
        }

        // POST: Produk/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduk,Nama,Harga,IdKategori")] Produk produk)
        {
            if (id != produk.IdProduk)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdukExists(produk.IdProduk))
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
            ViewData["IdKategori"] = new SelectList(_context.Kategori, "IdKategori", "NamaKategori", produk.IdKategori);
            return View(produk);
        }

        // GET: Produk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produk = await _context.Produk
                .Include(p => p.IdKategoriNavigation)
                .FirstOrDefaultAsync(m => m.IdProduk == id);
            if (produk == null)
            {
                return NotFound();
            }

            return View(produk);
        }

        // POST: Produk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produk = await _context.Produk.FindAsync(id);
            _context.Produk.Remove(produk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdukExists(int id)
        {
            return _context.Produk.Any(e => e.IdProduk == id);
        }
    }
}
