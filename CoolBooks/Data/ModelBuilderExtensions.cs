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
                new Book { Id = 1, Title = "Sword of Destiny", Description = "Geralt is a witcher, a man whose magic powers, enhanced by long training and a mysterious elixir, have made him a brilliant fighter and a merciless assassin. Yet he is no ordinary murderer: his targets are the multifarious monsters and vile fiends that ravage the land and attack the innocent.", ISBN = "1473231086", Created = DateTime.Now },
                new Book { Id = 2, Title = "The Thursday Murder Club", Description = "Four septuagenarians with a few tricks up their sleeves, A female cop with her first big case, A brutal murder, Welcome to…, The Thursday Murder Club", ISBN = "0241425441", Created = DateTime.Now}
                // new Book { Id = , Title = "", Description = "", ISBN = "", Created = DateTime.Now}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
        public static void SeedAuthor(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasData(
                new Author { Id = 1, FirstName = "Andrzej", LastName = "Sapkowski", BirthDate = new DateTime(1948, 6, 21), Created = DateTime.Now, },
                new Author { Id = 2, FirstName = "Richard", LastName = "Osman", BirthDate = new DateTime(1970, 11, 28), Created = DateTime.Now, }
                // new Author { Id = , FirstName = "", LastName = "", BirthDate = , Created = DateTime.Now, },
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
        public static void SeedGenre(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasData(
                new Genre { Id = 1, Name = "Fantasy", Description = "Fantasy is a genre that uses magic and other supernatural forms as a primary element of plot, theme, and/or setting. Fantasy is generally distinguished from science fiction and horror by the expectation that it steers clear of technological and macabre themes, respectively, though there is a great deal of overlap between the three (collectively known as speculative fiction or science fiction/fantasy)", Created = DateTime.Now },
                new Genre { Id = 2, Name = "Mystery", Description = "Mystery fiction is a loosely-defined term that is often used as a synonym of detective fiction — in other words a novel or short story in which a detective (either professional or amateur) solves a crime. The term \"mystery fiction\" may sometimes be limited to the subset of detective stories in which the emphasis is on the puzzle element and its logical solution (cf. whodunit), as a contrast to hardboiled detective stories which focus on action and gritty realism. However, in more general usage \"mystery\" may be used to describe any form of crime fiction, even if there is no mystery to be solved. For example, the Mystery Writers of America describes itself as \"the premier organization for mystery writers, professionals allied to the crime writing field, aspiring crime writers, and those who are devoted to the genre\".", Created = DateTime.Now}
                // new Genre { Id = , Name = "", Description = "", Created = DateTime.Now}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
        public static void SeedReview(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasData(
                new Review { Id = 1, BookId = 1, Title = "Stefans review", Text = "I liked this short story collection less than the 1st one in this series. The first 3 stories were quite boring and useless and I preferred the on screen version of the last 2 where Ciri first appeared. As the previous book, there is a lot of dialogue and verbal confrontation. There was less fighting and more philosophy . I still enjoyed listening to this volume and I am planning to continue with the series as soon as Ionut Grama will record it (Hurry Up!)", Rating = 5, Created = DateTime.Now },
                new Review { Id = 2, BookId = 2, Title = "Kalle Ankas review", Text = "This book was enjoyable, I’m not going to deny that. I liked reading about the quirky characters and their penchant for solving murders. It was an intriguing and funny premise which did in a way deliver. But the problem was it was very slow and even though there were some twists and turns they were quite anticlimactic. There were too many character perspectives during the book that just seemed unnecessary and a bit confusing. In theory the story seems quite captivating, but in reality it never made you sit on the edge of your seat or have to read the next chapter. Unfortunately, I wasn’t that invested in the crime and I wasn’t that curious about who the murderer was which is kind of the whole point of a mystery novel. It was an enjoyable book to cruise along with, but something you could pretty easily put down.", Rating = 2, Created = DateTime.Now}
                // new Review { Id = , BookId = "", Title = "", Text = "", Created = DateTime.Now}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
    }
}
