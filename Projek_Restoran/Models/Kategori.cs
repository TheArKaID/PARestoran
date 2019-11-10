using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projek_Restoran.Models
{
    public partial class Kategori
    {
        public Kategori()
        {
            Produk = new HashSet<Produk>();
        }

        public int IdKategori { get; set; }

        [Required(ErrorMessage = "Data Tidak Boleh Kosong")]
        public string NamaKategori { get; set; }

        public ICollection<Produk> Produk { get; set; }
    }
}
