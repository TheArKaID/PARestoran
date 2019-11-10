using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projek_Restoran.Models
{
    public partial class Pesanan
    {
        public int IdPesanan { get; set; }

        [Required(ErrorMessage ="Nama Customer Harus diisi"), 
        MaxLength(30, ErrorMessage = "Maksimal 30 Karakter")]
        public string NamaCustomer { get; set; }

        [Required(ErrorMessage = "Jumlah Harus diisi"),
        MaxLength(6, ErrorMessage = "Maksimal 6 Karakter"),
        RegularExpression("^[0-9]*$", ErrorMessage = "Hanya bisa diisi angka")]
        public string Jumlah { get; set; }

        [Required(ErrorMessage = "Tanggal harus diisi"),
        DataType(DataType.Date, ErrorMessage = "Format Tanggal salah")]
        public DateTime? Tanggal { get; set; }

        [Required(ErrorMessage = "Keterangan harus diisi")]
        public string Keterangan { get; set; }

        [Required(ErrorMessage = "ID Produk harus diisi")]
        public int? IdProduk { get; set; }

        [Required(ErrorMessage = "Jenis Pesanan harus diisi")]
        public int? IdJenisPesanan { get; set; }

        [Required(ErrorMessage = "Meja harus diisi")]
        public int? IdMeja { get; set; }

        [Required(ErrorMessage = "User harus diisi")]
        public int? IdUser { get; set; }

        public JenisPesanan IdJenisPesananNavigation { get; set; }
        public Produk IdProdukNavigation { get; set; }
        public User IdUserNavigation { get; set; }
        public Meja IdMejaNavigation { get; set; }
    }
}
