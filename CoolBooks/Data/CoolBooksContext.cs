#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoolBooks.Models;
using CoolBooks.Models.Quiz;
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
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CoolBooks.Models.ReviewLikes> ReviewLikes { get; set; }
        public DbSet<CoolBooks.Models.CommentLikes> CommentLikes { get; set; }
        public DbSet<ReportedReview> ReportedReviews { get; set; }
        public DbSet<ReportedComment> ReportedComments { get; set; }

        //quiz dbsets
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QOption> QOption { get; set; }
        public DbSet<QuizGenre> QuizGenre { get; set; }
        public DbSet<QuizTaken> QuizTaken { get; set; }


        public CoolBooksContext (DbContextOptions<CoolBooksContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews).OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Review>()
                .Property(r => r.Created)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Review>()
                       .Property(l => l.LikeCount).HasDefaultValue(0);

            modelBuilder.Entity<Review>()
                        .Property(d => d.DisLikeCount).HasDefaultValue(0);

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity<AuthorBook>
                 (ba => ba.HasOne<Author>().WithMany().OnDelete(DeleteBehavior.ClientSetNull),
                 ba => ba.HasOne<Book>().WithMany().OnDelete(DeleteBehavior.ClientSetNull))
                 .Property(ba => ba.Created);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books)
                .UsingEntity<BookGenre>
                 (bg => bg.HasOne<Genre>().WithMany().OnDelete(DeleteBehavior.ClientSetNull),
                 bg => bg.HasOne<Book>().WithMany().OnDelete(DeleteBehavior.ClientSetNull))
                 .Property(ba => ba.Created);

            modelBuilder.Entity<Comment>()
                       .Property(l => l.LikeCount).HasDefaultValue(0);

            modelBuilder.Entity<Comment>()
                        .Property(d => d.DisLikeCount).HasDefaultValue(0);

            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.QuizGenres)
                .WithMany(qg => qg.Quizzes)
                .UsingEntity<QuizQuizGenre>
                (qqg => qqg.HasOne<QuizGenre>().WithMany().OnDelete(DeleteBehavior.ClientSetNull),
                qqg => qqg.HasOne<Quiz>().WithMany().OnDelete(DeleteBehavior.ClientSetNull))
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
            modelBuilder.SeedComment();
            modelBuilder.SeedReviewLike();


            //Seed Quiztables
            modelBuilder.SeedQuizzes();
            modelBuilder.SeedQuestions();
            modelBuilder.SeedOption();
            modelBuilder.SeedQuizGenres();
            modelBuilder.SeedQuizQuizGenres();



        }


        
    }
}
