using System;
using System.Collections.Generic;

namespace Projek_Restoran.Models
{
    public partial class Produk
    {
        public Produk()
        {
            Pesanan = new HashSet<Pesanan>();
        }

        public int IdProduk { get; set; }
        public string Nama { get; set; }
        public int? Harga { get; set; }
        public int? IdKategori { get; set; }

        public Kategori IdKategoriNavigation { get; set; }
        public ICollection<Pesanan> Pesanan { get; set; }
    }
}
