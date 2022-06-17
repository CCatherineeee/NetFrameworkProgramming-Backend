using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Model
{
    public partial class netContext : DbContext
    {
        public netContext()
        {
        }

        public netContext(DbContextOptions<netContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<FavoritePost> FavoritePosts { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostLabel> PostLabels { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=124.221.218.138;port=3306;user=root;password=Root_lxy618;database=net");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {

                entity.ToTable("comment");

                entity.HasIndex(e => e.UserId, "comment_user_idx");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");


                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Content)
                    .HasColumnType("longtext")
                    .HasColumnName("content");

                entity.Property(e => e.CreateTime)
                    .HasMaxLength(45)
                    .HasColumnName("create_time");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comment_user");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.ToTable("favorite");

                entity.HasIndex(e => e.UserId, "favorite_user_idx");

                entity.Property(e => e.FavoriteId).HasColumnName("favorite_id");

                entity.Property(e => e.FavoriteName)
                    .HasMaxLength(2048)
                    .HasColumnName("favorite_name");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.UserId)
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favorite_post_favorite");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.FavoritePosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favorite_post_post");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.ToTable("follow");

                entity.HasIndex(e => e.UserId, "follow_user_idx");

                entity.Property(e => e.FollowId).HasColumnName("follow_id");

                entity.Property(e => e.FollowUserId).HasColumnName("follow_user_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("follow_user");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.HasIndex(e => e.UserId, "post_user_idx");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.CommrntCount)
                    .HasColumnName("commrnt_count")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.HotPoint)
                    .HasColumnName("hot_point")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ViewCount)
                    .HasColumnName("view_count")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Content)
                    .HasColumnType("longtext")
                    .HasColumnName("content");

                entity.Property(e => e.CreateTime).HasColumnName("create_time");

                entity.Property(e => e.FavoriteCount)
                    .HasColumnName("favorite_count")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PicUrl)
                    .HasMaxLength(2048)
                    .HasColumnName("pic_url");

                entity.Property(e => e.Title)
                    .HasMaxLength(2048)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("post_user");
            });

            modelBuilder.Entity<PostLabel>(entity =>
            {
                entity.ToTable("post_label");

                entity.HasIndex(e => e.PostId, "post_label_post_idx");

                entity.Property(e => e.PostLabelId).HasColumnName("post_label_id");

                entity.Property(e => e.Label)
                    .HasMaxLength(45)
                    .HasColumnName("label");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostLabels)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("post_label_post");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AvatarUrl)
                    .HasMaxLength(2048)
                    .HasColumnName("avatar_url");

                entity.Property(e => e.Description)
                    .HasColumnType("longtext")
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(2048)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(2048)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(2048)
                    .HasColumnName("password");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
