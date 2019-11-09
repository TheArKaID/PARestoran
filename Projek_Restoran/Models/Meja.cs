using System;
using System.Collections.Generic;

namespace Projek_Restoran.Models
{
    public partial class Meja
    {
        public Meja()
        {
            Pesanan = new HashSet<Pesanan>();
        }

        public int IdMeja { get; set; }
        public string NomorMeja { get; set; }

        public ICollection<Pesanan> Pesanan { get; set; }
    }
}
