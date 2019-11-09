using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Projek_Restoran.Models
{
    public partial class User
    {
        public User()
        {
            Pesanan = new HashSet<Pesanan>();
        }

        public int IdUser { get; set; }

        [Required(ErrorMessage = "Nama Tidak Boleh Kosong")]
        public string Nama { get; set; }

        [MaxLength(13, ErrorMessage = "Nomor HP Maksimal 13 Angka")]
        [MinLength(10, ErrorMessage = "Nomor HP Minimal 10 Angka")]
        [Required(ErrorMessage = "Nomor HP Tidak Boleh Kosong")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Hanya Boleh Diisi Angka")]
        public string NoHp { get; set; }

        [MaxLength(20, ErrorMessage = "Username Maksimal 20 Karakter")]
        [Required(ErrorMessage = "Username Tidak Boleh Kosong")]
        public string Username { get; set; }

        [MaxLength(10, ErrorMessage = "Password Maksimal 10 Karakter")]
        [MinLength(6, ErrorMessage = "Password Minimal 6 Karakter")]
        [Required(ErrorMessage = "Password Tidak Boleh Kosong")]
        public string Password { get; set; }

        public ICollection<Pesanan> Pesanan { get; set; }
    }
}
