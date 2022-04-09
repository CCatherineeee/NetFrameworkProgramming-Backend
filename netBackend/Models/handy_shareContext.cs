using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace netBackend.Models
{
    public partial class handy_shareContext : DbContext
    {
        public handy_shareContext()
        {
        }

        public handy_shareContext(DbContextOptions<handy_shareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<FavoritePost> FavoritePosts { get; set; }
        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=handy_share");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.HasIndex(e => e.UserId, "address_user_idx");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.City)
                    .HasMaxLength(1023)
                    .HasColumnName("city");

                entity.Property(e => e.County)
                    .HasMaxLength(1023)
                    .HasColumnName("county");

                entity.Property(e => e.Detail)
                    .HasMaxLength(48)
                    .HasColumnName("detail");

                entity.Property(e => e.Name)
                    .HasMaxLength(124)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(127)
                    .HasColumnName("phone");

                entity.Property(e => e.Province)
                    .HasMaxLength(1023)
                    .HasColumnName("province");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("address_user");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.HasIndex(e => e.GoodId, "cart_good_idx");

                entity.HasIndex(e => e.UserId, "cart_user_idx");

                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.GoodId).HasColumnName("good_id");

                entity.Property(e => e.Num)
                    .HasColumnName("num")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Good)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.GoodId)
                    .HasConstraintName("cart_good");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("cart_user");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.HasIndex(e => e.PostId, "comment_post_idx");

                entity.HasIndex(e => e.UserId, "comment_user_idx");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.Content)
                    .HasColumnType("mediumtext")
                    .HasColumnName("content");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.CommentsNavigation)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("comment_post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("comment_user");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.ToTable("favorite");

                entity.HasIndex(e => e.UserId, "favorite_user_idx");

                entity.Property(e => e.FavoriteId).HasColumnName("favorite_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("favorite_user");
            });

            modelBuilder.Entity<FavoritePost>(entity =>
            {
                entity.HasKey(e => new { e.FavoriteId, e.PostId })
                    .HasName("PRIMARY");

                entity.ToTable("favorite_post");

                entity.HasIndex(e => e.PostId, "favorite_post_post_idx");

                entity.Property(e => e.FavoriteId).HasColumnName("favorite_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.HasOne(d => d.Favorite)
                    .WithMany(p => p.FavoritePosts)
                    .HasForeignKey(d => d.FavoriteId)
                    .HasConstraintName("favorite_post_favorite");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.FavoritePosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favorite_post_post");
            });

            modelBuilder.Entity<Good>(entity =>
            {
                entity.ToTable("goods");

                entity.HasIndex(e => e.UserId, "good_user_idx");

                entity.Property(e => e.GoodId).HasColumnName("good_id");

                entity.Property(e => e.DescriptionUrl)
                    .HasMaxLength(2048)
                    .HasColumnName("description_url");

                entity.Property(e => e.InSale)
                    .HasColumnType("tinyint")
                    .HasColumnName("in_sale")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Likes)
                    .HasColumnName("likes")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Num).HasColumnName("num");

                entity.Property(e => e.PicUrl)
                    .HasMaxLength(2048)
                    .HasColumnName("pic_url");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Sales)
                    .HasColumnName("sales")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Goods)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("good_user");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasIndex(e => e.AddressId, "order_address_idx");

                entity.HasIndex(e => e.GoodId, "order_goods_idx");

                entity.HasIndex(e => e.UserId, "order_user_idx");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.CreateTime).HasColumnName("create_time");

                entity.Property(e => e.GoodId).HasColumnName("good_id");

                entity.Property(e => e.Num)
                    .HasMaxLength(45)
                    .HasColumnName("num");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("order_address");

                entity.HasOne(d => d.Good)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.GoodId)
                    .HasConstraintName("order_goods");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("order_user");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.HasIndex(e => e.UserId, "post_user_idx");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.HtmlUrl)
                    .HasMaxLength(2048)
                    .HasColumnName("html_url");

                entity.Property(e => e.Likes)
                    .HasColumnName("likes")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Title)
                    .HasMaxLength(45)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("post_user");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(127)
                    .HasColumnName("email");

                entity.Property(e => e.FakeName)
                    .HasMaxLength(127)
                    .HasColumnName("fake_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(2047)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
