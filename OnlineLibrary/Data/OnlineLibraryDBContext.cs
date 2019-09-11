using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Data
{
    public class OnlineLibraryDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Writer> Writers { get; set; }


        public OnlineLibraryDBContext(DbContextOptions<OnlineLibraryDBContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=OnlineLibrary;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Books)
                .WithOne(b => b.Genre)
                .IsRequired()
                .HasForeignKey(b => b.IdGenre);


            modelBuilder.Entity<Writer>()
                .HasMany(w => w.Books)
                .WithOne(b => b.Writer)
                .IsRequired()
                .HasForeignKey(b => b.IdWriter);

        }
    }
}
