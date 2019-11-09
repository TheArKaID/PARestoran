using System;
using System.Collections.Generic;

namespace Projek_Restoran.Models
{
    public partial class User
    {
        public User()
        {
            Pesanan = new HashSet<Pesanan>();
        }

        public int IdUser { get; set; }
        public string Nama { get; set; }
        public string NoHp { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Pesanan> Pesanan { get; set; }
    }
}
