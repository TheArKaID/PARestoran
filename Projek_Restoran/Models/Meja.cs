using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projek_Restoran.Models
{
    public partial class Meja
    {
        public Meja()
        {
            Pesanan = new HashSet<Pesanan>();
        }

        [Required(ErrorMessage = "Id Meja Tidak Boleh Kosong")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya Boleh Diisi Dengan Angka")]
        public int IdMeja { get; set; }

        [Required(ErrorMessage = "No Meja Tidak Boleh Kosong")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya Boleh Diisi Dengan Angka")]
        public string NomorMeja { get; set; }

        public ICollection<Pesanan> Pesanan { get; set; }
    }
}
