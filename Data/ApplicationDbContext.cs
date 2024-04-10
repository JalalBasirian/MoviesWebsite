using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesWeb.Models;

namespace MoviesWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<GenreMovie> GenreMovies { get; set; }
        public DbSet<MovieCast> MovieCast { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GenreMovie>()
                .HasKey(gm => new { gm.GenreId, gm.MovieId });
            modelBuilder.Entity<MovieCast>()
                .HasKey(gm => new { gm.MovieId, gm.CastId });
        }
    }
}
