#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoolBooks.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CoolBooks.Data
{
    public class CoolBooksContext : IdentityDbContext<CoolBooksUser>
    {
        public DbSet<CoolBooks.Models.Book> Book { get; set; }
        public DbSet<CoolBooks.Models.Author> Author { get; set; }
        public DbSet<CoolBooks.Models.Review> Review { get; set; }
        public DbSet<CoolBooks.Models.Genre> Genre { get; set; }
        public DbSet<AuthorBook> AuthorBook { get; set; }
        public DbSet<CoolBooks.Models.Likes> Likes { get; set; }

        public CoolBooksContext (DbContextOptions<CoolBooksContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>()
                .Property(r => r.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Review>()
                       .Property(l => l.LikeCount).HasDefaultValue(0);

            modelBuilder.Entity<Review>()
                        .Property(d => d.DisLikeCount).HasDefaultValue(0);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<AuthorBook>
                 (ba => ba.HasOne<Author>().WithMany(),
                 ba => ba.HasOne<Book>().WithMany())
                 .Property(ba => ba.Created);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity<BookGenre>
                 (bg => bg.HasOne<Genre>().WithMany(),
                 bg => bg.HasOne<Book>().WithMany())
                 .Property(ba => ba.Created);

            modelBuilder.SeedBook(); // Kör min Seed extension metod.
            modelBuilder.SeedAuthor(); // Kör min Seed author metod.
            modelBuilder.SeedBookAuthor();
            modelBuilder.SeedGenre(); //  Kör min Seed genre metod.
            modelBuilder.SeedBookGenre();
            modelBuilder.SeedReview(); // Kör min Seed review metod.
            modelBuilder.SeedUser();
            modelBuilder.SeedRole();
            modelBuilder.SeedUserRole();

        }


        
    }
}
