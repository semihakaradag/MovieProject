using Microsoft.EntityFrameworkCore;
using MovieProject.Models;

namespace MovieProject.Context
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; } // Köprü tablosu

        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tabloların özel adları
            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Genre>().ToTable("Genres");
            modelBuilder.Entity<MovieGenre>().ToTable("MovieGenres");

            // Movie ve Genre arasındaki çoktan çoğa ilişkiyi ayarlayın
            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);
        }
    }
}
