using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Domain.Models;

namespace WebAPI.Persistence.Contexts
{
    public class LibraryAdminContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryBook> CategoryBook { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            optionsBuilder.UseMySql(configuration.GetConnectionString("LibraryAdmin"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CategoryBook>()
                .HasKey(k => new { k.CategoryId, k.BookId });

            builder.Entity<Author>(entity =>
            {
                entity.Property(p => p.Name)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

                entity.Property(p => p.LastName)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();
            });

            builder.Entity<Book>(entity =>
            {
                entity.Property(p => p.Name)
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

                entity.Property(p => p.ISBN)
                .HasColumnType("BIGINT(13)");
            });

            builder.Entity<Category>(entity =>
            {
                entity.Property(p => p.Name)
                .HasColumnType("VARCHAR(20)")
                .IsRequired();

                entity.Property(p => p.Description)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();
            });
        }
    }
}
