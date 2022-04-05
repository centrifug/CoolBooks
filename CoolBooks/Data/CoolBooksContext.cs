#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoolBooks.Models;

namespace CoolBooks.Data
{
    public class CoolBooksContext : DbContext
    {
        public DbSet<CoolBooks.Models.Book> Book { get; set; }
        public CoolBooksContext (DbContextOptions<CoolBooksContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .Property(r => r.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<AuthorBook>
                 (ba => ba.HasOne<Author>().WithMany(),
                 ba => ba.HasOne<Book>().WithMany()).Property(ba => ba.Created);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity<BookGenre>
                 (bg => bg.HasOne<Genre>().WithMany(),
                 bg => bg.HasOne<Book>().WithMany()).Property(ba => ba.Created);

        }

        
    }
}
