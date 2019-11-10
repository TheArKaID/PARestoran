using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Projek_Restoran.Models
{
    public partial class WEB_ProjekAkhirContext : DbContext
    {
        public WEB_ProjekAkhirContext()
        {
        }

        public WEB_ProjekAkhirContext(DbContextOptions<WEB_ProjekAkhirContext> options)
            : base(options)
        {
        }

        public virtual DbSet<JenisPesanan> JenisPesanan { get; set; }
        public virtual DbSet<Kategori> Kategori { get; set; }
        public virtual DbSet<Meja> Meja { get; set; }
        public virtual DbSet<Pesanan> Pesanan { get; set; }
        public virtual DbSet<Produk> Produk { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=FESAART-DEKSTOP;Database=WEB_Projek-Akhir;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JenisPesanan>(entity =>
            {
                entity.HasKey(e => e.IdJenisPesanan);

                entity.ToTable("Jenis_Pesanan");

                entity.Property(e => e.IdJenisPesanan).HasColumnName("Id_Jenis_Pesanan");

                entity.Property(e => e.NamaJenisPesanan)
                    .HasColumnName("Nama_Jenis_Pesanan")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Kategori>(entity =>
            {
                entity.HasKey(e => e.IdKategori);

                entity.Property(e => e.IdKategori).HasColumnName("Id_Kategori");

                entity.Property(e => e.NamaKategori)
                    .HasColumnName("Nama_Kategori")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Meja>(entity =>
            {
                entity.HasKey(e => e.IdMeja);

                entity.Property(e => e.IdMeja).HasColumnName("Id_Meja");

                entity.Property(e => e.NomorMeja)
                    .HasColumnName("Nomor_Meja")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pesanan>(entity =>
            {
                entity.HasKey(e => e.IdPesanan);

                entity.Property(e => e.IdPesanan).HasColumnName("Id_Pesanan");

                entity.Property(e => e.IdJenisPesanan).HasColumnName("Id_Jenis_Pesanan");

                entity.Property(e => e.IdMeja).HasColumnName("Id_Meja");

                entity.Property(e => e.IdProduk).HasColumnName("Id_Produk");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Jumlah)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Keterangan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NamaCustomer)
                    .HasColumnName("Nama_Customer")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Tanggal).HasColumnType("datetime");

                entity.HasOne(d => d.IdJenisPesananNavigation)
                    .WithMany(p => p.Pesanan)
                    .HasForeignKey(d => d.IdJenisPesanan)
                    .HasConstraintName("FK_Pesanan_Jenis_Pesanan");

                entity.HasOne(d => d.IdProdukNavigation)
                    .WithMany(p => p.Pesanan)
                    .HasForeignKey(d => d.IdProduk)
                    .HasConstraintName("FK_Pesanan_Produk");

                entity.HasOne(d => d.IdMejaNavigation)
                    .WithMany(p => p.Pesanan)
                    .HasForeignKey(d => d.IdMeja)
                    .HasConstraintName("FK_Pesanan_Meja");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Pesanan)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Pesanan_User");
            });

            modelBuilder.Entity<Produk>(entity =>
            {
                entity.HasKey(e => e.IdProduk);

                entity.Property(e => e.IdProduk).HasColumnName("Id_Produk");

                entity.Property(e => e.IdKategori).HasColumnName("Id_Kategori");

                entity.Property(e => e.Nama)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdKategoriNavigation)
                    .WithMany(p => p.Produk)
                    .HasForeignKey(d => d.IdKategori)
                    .HasConstraintName("FK_Produk_Kategori");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Nama)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.NoHp)
                    .HasColumnName("No_HP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
        }
    }
}
