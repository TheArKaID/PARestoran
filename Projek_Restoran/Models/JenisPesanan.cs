using System;
using System.Collections.Generic;

namespace Projek_Restoran.Models
{
    public partial class JenisPesanan
    {
        public JenisPesanan()
        {
            Pesanan = new HashSet<Pesanan>();
        }

        public int IdJenisPesanan { get; set; }
        public string NamaJenisPesanan { get; set; }

        public ICollection<Pesanan> Pesanan { get; set; }
    }
}
