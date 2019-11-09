using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projek_Restoran.Models
{
    public partial class Produk
    {
        public Produk()
        {
            Pesanan = new HashSet<Pesanan>();
        }

        [Required(ErrorMessage = "Id Produk Tidak Boleh Kosong")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya Boleh Diisi Dengan Angka")]
        public int IdProduk { get; set; }

        [Required(ErrorMessage = "Nama Tidak Boleh Kosong")]
        public string Nama { get; set; }

        [Required(ErrorMessage = "Harga Tidak Boleh Kosong")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya Boleh Diisi Dengan Angka")]
        public int? Harga { get; set; }

        [Required(ErrorMessage = "Id Kategori Tidak Boleh Kosong")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya Boleh Diisi Dengan Angka")]
        public int? IdKategori { get; set; }

        public Kategori IdKategoriNavigation { get; set; }
        public ICollection<Pesanan> Pesanan { get; set; }
    }
}
