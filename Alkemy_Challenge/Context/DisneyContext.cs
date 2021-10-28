using Alkemy_Challenge.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Context
{
    public class DisneyContext : DbContext 
    {
        private const string Schema = "Disney";

        public DisneyContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(Schema);

            //modelBuilder.Entity<Movie>().HasData(
            //    new Movie()
            //    {
            //        Id = 1,
            //        Title = "Titanic",
            //        CreationDate = new DateTime(2015, 12, 31),
            //        Rating = 4,
            //    });
        }

        public DbSet<Character> Characters { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
    }
}
