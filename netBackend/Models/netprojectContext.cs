using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace netBackend.Models
{
    public partial class netprojectContext : DbContext
    {
        public netprojectContext()
        {
        }

        public netprojectContext(DbContextOptions<netprojectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Commodit> Commodits { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=netproject");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commodit>(entity =>
            {
                entity.ToTable("commodit");

                entity.HasIndex(e => e.SellerId, "seller_id_commid_idx");

                entity.Property(e => e.CommoditId).HasColumnName("commodit_id");

                entity.Property(e => e.Description)
                    .HasColumnType("longtext")
                    .HasColumnName("description");

                entity.Property(e => e.HasPostage).HasColumnName("has_postage");

                entity.Property(e => e.PicUrl)
                    .HasMaxLength(2048)
                    .HasColumnName("pic_url");

                entity.Property(e => e.Postage).HasColumnName("postage");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SellerId).HasColumnName("seller_id");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Commodits)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("seller_id_commid");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(256)
                    .HasColumnName("email");

                entity.Property(e => e.FakeName)
                    .HasMaxLength(1024)
                    .HasColumnName("fake_name");

                entity.Property(e => e.Identified).HasColumnName("identified");

                entity.Property(e => e.UserPwd)
                    .HasMaxLength(1024)
                    .HasColumnName("user_pwd");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
