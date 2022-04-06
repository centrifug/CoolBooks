using CoolBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace CoolBooks.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedBook (this ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Book>()
                .HasData(
                new Book { Id = 1, Title = "Sword of Destiny", Description = "Geralt is a witcher, a man whose magic powers, enhanced by long training and a mysterious elixir, have made him a brilliant fighter and a merciless assassin. Yet he is no ordinary murderer: his targets are the multifarious monsters and vile fiends that ravage the land and attack the innocent.", ISBN = "1473231086", Created = DateTime.Now }
                // new Book { Id = , Title = "", Description = "", ISBN = "", Created = DateTime.Now}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!


                );
        }
        public static void SeedAuthor(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasData(
                new Author { Id = 1, FirstName = "Andrzej", LastName = "Sapkowski", BirthDate = new DateTime(1948, 6, 21), Created = DateTime.Now, }
                // new Author { Id = , FirstName = "", LastName = "", BirthDate = , Created = DateTime.Now, },
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
        public static void SeedGenre(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasData(
                new Genre { Id = 1, Name = "Fantasy", Description = "Fantasy is a genre that uses magic and other supernatural forms as a primary element of plot, theme, and/or setting. Fantasy is generally distinguished from science fiction and horror by the expectation that it steers clear of technological and macabre themes, respectively, though there is a great deal of overlap between the three (collectively known as speculative fiction or science fiction/fantasy)", Created = DateTime.Now }
                // new Book { Id = , Name = "", Description = "", Created = DateTime.Now}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
        public static void SeedReview(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasData(
                new Review { Id = 1, BookId = 1, Title = "Stefans review", Text = "I liked this short story collection less than the 1st one in this series. The first 3 stories were quite boring and useless and I preferred the on screen version of the last 2 where Ciri first appeared. As the previous book, there is a lot of dialogue and verbal confrontation. There was less fighting and more philosophy . I still enjoyed listening to this volume and I am planning to continue with the series as soon as Ionut Grama will record it (Hurry Up!)", Rating = 5, Created = DateTime.Now }
                // new Book { Id = , BookId = "", Title = "", Text = "", Created = DateTime.Now}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
    }
}
