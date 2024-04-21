using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Models;

namespace PhotoSharingApi.DAL
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<PhotoAlbum> PhotoAlbums { get; set; }
        public DbSet<PhotoCategory> PhotoCategories { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Album>()
                .HasOne(a => a.User)
                .WithMany(u => u.Albums)
                .HasForeignKey(a => a.user_id)
                .HasPrincipalKey(u => u.user_id);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.user_id)
                .HasPrincipalKey(u => u.user_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Photo)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.photo_id)
                .HasPrincipalKey(p => p.photo_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Photo>()
                .HasOne(p => p.User)
                .WithMany(u => u.Photos)
                .HasForeignKey(p => p.user_id)
                .HasPrincipalKey(u => u.user_id);

            modelBuilder.Entity<PhotoAlbum>()
                .HasKey(pa => new { pa.photo_id, pa.album_id });

            modelBuilder.Entity<PhotoAlbum>()
                .HasOne(pa => pa.Photo)
                .WithMany(p => p.PhotoAlbums)
                .HasForeignKey(pa => pa.photo_id) 
                .HasPrincipalKey(p => p.photo_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PhotoAlbum>()
                .HasOne(pa => pa.Album)
                .WithMany(a => a.PhotoAlbums)
                .HasForeignKey(pa => pa.album_id)
                .HasPrincipalKey(a => a.album_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PhotoCategory>()
                .HasKey(pc => new { pc.photo_id, pc.category_id });

            modelBuilder.Entity<PhotoCategory>()
                .HasOne(pc => pc.Photo)
                .WithMany(p => p.PhotoCategories)
                .HasForeignKey(pc => pc.photo_id) 
                .HasPrincipalKey(p => p.photo_id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PhotoCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.PhotoCategories)
                .HasForeignKey(pc => pc.category_id) 
                .HasPrincipalKey(c => c.category_id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
