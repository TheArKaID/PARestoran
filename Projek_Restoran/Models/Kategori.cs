using System;
using System.Collections.Generic;

namespace Projek_Restoran.Models
{
    public partial class Kategori
    {
        public Kategori()
        {
            Produk = new HashSet<Produk>();
        }

        public int IdKategori { get; set; }
        public string NamaKategori { get; set; }

        public ICollection<Produk> Produk { get; set; }
    }
}
