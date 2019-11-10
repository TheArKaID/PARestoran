using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projek_Restoran.Models
{
    public partial class JenisPesanan
    {
        public JenisPesanan()
        {
            Pesanan = new HashSet<Pesanan>();
        }

        public int IdJenisPesanan { get; set; }

        [Required(ErrorMessage = "Data Tidak Boleh Kosong")]
        public string NamaJenisPesanan { get; set; }

        public ICollection<Pesanan> Pesanan { get; set; }
    }
}
