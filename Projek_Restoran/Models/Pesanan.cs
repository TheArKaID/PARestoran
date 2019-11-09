using System;
using System.Collections.Generic;

namespace Projek_Restoran.Models
{
    public partial class Pesanan
    {
        public int IdPesanan { get; set; }
        public string NamaCustomer { get; set; }
        public string Jumlah { get; set; }
        public DateTime? Tanggal { get; set; }
        public string Keterangan { get; set; }
        public int? IdProduk { get; set; }
        public int? IdJenisPesanan { get; set; }
        public int? IdMeja { get; set; }
        public int? IdUser { get; set; }

        public JenisPesanan IdJenisPesananNavigation { get; set; }
        public Produk IdProdukNavigation { get; set; }
        public User IdUser1 { get; set; }
        public Meja IdUserNavigation { get; set; }
    }
}
