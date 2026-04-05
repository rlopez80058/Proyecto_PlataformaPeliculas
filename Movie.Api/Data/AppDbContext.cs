using Microsoft.EntityFrameworkCore;
using Movie.Api.Models;

namespace Movie.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Movies> Movies => Set<Movies>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
        public DbSet<LibraryItem> LibraryItems => Set<LibraryItem>();
        public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Movies>(entity =>
            {
                entity.ToTable("Movies");
                entity.HasKey(x => x.Id);

                entity.HasIndex(x => x.TmdbId).IsUnique();

                entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
                entity.Property(x => x.OriginalTitle).HasMaxLength(200).IsRequired();
                entity.Property(x => x.Overview).HasMaxLength(3000);
                entity.Property(x => x.OriginalLanguage).HasMaxLength(10).IsRequired();
                entity.Property(x => x.PosterPath).HasMaxLength(500);
                entity.Property(x => x.BackdropPath).HasMaxLength(500);

                entity.Property(x => x.Popularity).HasColumnType("decimal(10,2)");
                entity.Property(x => x.VoteAverage).HasColumnType("decimal(4,2)");
            });

            mb.Entity<Genre>(entity =>
            {
                entity.ToTable("Genres");
                entity.HasKey(x => x.Id);

                entity.HasIndex(x => x.TmdbGenreId).IsUnique();

                entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
            });

            mb.Entity<MovieGenre>(entity =>
            {
                entity.ToTable("MovieGenres");
                entity.HasKey(x => new { x.MovieId, x.GenreId });

                entity.HasOne(x => x.Movie)
                    .WithMany(x => x.MovieGenres)
                    .HasForeignKey(x => x.MovieId);

                entity.HasOne(x => x.Genre)
                    .WithMany(x => x.MovieGenres)
                    .HasForeignKey(x => x.GenreId);
            });

            mb.Entity<LibraryItem>(entity =>
            {
                entity.ToTable("LibraryItems");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(x => x.Comment).HasMaxLength(1000);

                entity.HasOne(x => x.Movie)
                    .WithMany(x => x.LibraryItems)
                    .HasForeignKey(x => x.MovieId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            mb.Entity<ActivityLog>(entity =>
            {
                entity.ToTable("ActivityLogs");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.ActionType)
                    .HasConversion<string>()
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(x => x.Description)
                    .HasMaxLength(500)
                    .IsRequired();
            });
        }
    }
}
