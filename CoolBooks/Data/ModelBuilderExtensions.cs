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
                new Book { Id = 2, Title = "The Thursday Murder Club", Description = "Four septuagenarians with a few tricks up their sleeves, A female cop with her first big case, A brutal murder, Welcome to…, The Thursday Murder Club", ISBN = "0241425441", Created = DateTime.Now},
                new Book { Id = 3, Title = "Walt Disney's Donald Duck: Lost in the Andes", Description = "Carl Barks Donald Duck and Uncle Scrooge comics are considered among the greatest artistic and storytelling achievements in the history of the medium. After serving a stint at the Walt Disney studios as an in-betweener and a gag-man, Barks began drawing the comic book adventures of Donald Duck in 1942. He quickly mastered every aspect of cartooning and over the next nearly 30 years created some of the most memorable comics ever drawn as well as some of the most memorable characters: Barks introduced Uncle Scrooge, the charmed and insufferable Gladstone Gander, the daffy inventor Gyro Gearloose, the bumbling and heedless Beagle Boys, the Junior Woodchucks, and many others. Barks alternated between longish, sprawling 20- or 30-page adventure yarns filled with the romance of danger, courage, and derring-do, whose exotic locales spanned the globe, and shorter stories that usually revolved around crazily ingenious domestic squabbles between Donald and various members of the Duckburg cast.", ISBN = "1606994743 ", Created = DateTime.Now},
                new Book { Id = 4, Title = "Pojken som inte fanns", Description = "Pojken som inte fanns är den efterlängtade fristående fortsättningen på Dave Pelzers skakande barndomsskildring om vad som hände efter att han vid tolv års ålder omhändertogs av myndigheterna och placerades i fosterhem.", ISBN = "9170010412", Created = DateTime.Now},
                new Book { Id = 5, Title = "Troublemaker", Description = "Troublemaker follows the events of the LA Riots through the eyes of 12-year-old Jordan as he navigates school and family. This book will highlight the unique Korean American perspective.", ISBN = "0759554471", Created = DateTime.Now}
                // new Book { Id = , Title = "", Description = "", ISBN = "", Created = DateTime.Now}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
        public static void SeedAuthor(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasData(
                new Author { Id = 1, FirstName = "Andrzej", LastName = "Sapkowski", BirthDate = new DateTime(1948, 6, 21), Created = DateTime.Now, },
                new Author { Id = 2, FirstName = "Richard", LastName = "Osman", BirthDate = new DateTime(1970, 11, 28), Created = DateTime.Now, },
                new Author { Id = 3, FirstName = "Carl", LastName = "Barks", BirthDate = new DateTime(1901, 3, 27), Created = DateTime.Now, },
                new Author { Id = 4, FirstName = "Dave", LastName = "Pelzer", BirthDate = new DateTime(1960, 12, 29), Created = DateTime.Now, },
                new Author { Id = 5, FirstName = "John", LastName = "Cho", BirthDate = new DateTime(1972, 6, 16), Created = DateTime.Now, }
                // new Author { Id = , FirstName = "", LastName = "", BirthDate = new DateTime(2000, 12, 24), Created = DateTime.Now, }
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
        public static void SeedGenre(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasData(
                new Genre { Id = 1, Name = "Fantasy", Description = "Fantasy is a genre that uses magic and other supernatural forms as a primary element of plot, theme, and/or setting. Fantasy is generally distinguished from science fiction and horror by the expectation that it steers clear of technological and macabre themes, respectively, though there is a great deal of overlap between the three (collectively known as speculative fiction or science fiction/fantasy)", Created = DateTime.Now },
                new Genre { Id = 2, Name = "Mystery", Description = "Mystery fiction is a loosely-defined term that is often used as a synonym of detective fiction — in other words a novel or short story in which a detective (either professional or amateur) solves a crime. The term \"mystery fiction\" may sometimes be limited to the subset of detective stories in which the emphasis is on the puzzle element and its logical solution (cf. whodunit), as a contrast to hardboiled detective stories which focus on action and gritty realism. However, in more general usage \"mystery\" may be used to describe any form of crime fiction, even if there is no mystery to be solved. For example, the Mystery Writers of America describes itself as \"the premier organization for mystery writers, professionals allied to the crime writing field, aspiring crime writers, and those who are devoted to the genre\".", Created = DateTime.Now},
                new Genre { Id = 3, Name = "Comics", Description = "A comic book or comicbook, also called comic magazine or simply comic, is a publication that consists of comic art in the form of sequential juxtaposed panels that represent individual scenes. Panels are often accompanied by brief descriptive prose and written narrative, usually dialog contained in word balloons emblematic of the comics art form.", Created = DateTime.Now},
                new Genre { Id = 4, Name = "Nonfiction", Description = "Nonfiction is an account or representation of a subject which is presented as fact. This presentation may be accurate or not; that is, it can give either a true or a false account of the subject in question. However, it is generally assumed that the authors of such accounts believe them to be truthful at the time of their composition. Note that reporting the beliefs of others in a nonfiction format is not necessarily an endorsement of the ultimate veracity of those beliefs, it is simply saying that it is true that people believe that (for such topics as mythology, religion). Nonfiction can also be written about fiction, giving information about these other works.", Created = DateTime.Now},
                new Genre { Id = 5, Name = "Childrens", Description = "Children's literature is for readers and listeners up to about age 12. It is often illustrated. The term is used in senses that sometimes exclude young-adult fiction, comic books, or other genres. Books specifically for children existed at least several hundred years ago.", Created = DateTime.Now}
                // new Genre { Id = , Name = "", Description = "", Created = DateTime.Now}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
        public static void SeedReview(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasData(
                new Review { Id = 1, BookId = 1, Title = "Stefans review", Text = "I liked this short story collection less than the 1st one in this series. The first 3 stories were quite boring and useless and I preferred the on screen version of the last 2 where Ciri first appeared. As the previous book, there is a lot of dialogue and verbal confrontation. There was less fighting and more philosophy . I still enjoyed listening to this volume and I am planning to continue with the series as soon as Ionut Grama will record it (Hurry Up!)", Rating = 5, Created = DateTime.Now },
                new Review { Id = 2, BookId = 2, Title = "Kalle Ankas review", Text = "This book was enjoyable, I’m not going to deny that. I liked reading about the quirky characters and their penchant for solving murders. It was an intriguing and funny premise which did in a way deliver. But the problem was it was very slow and even though there were some twists and turns they were quite anticlimactic. There were too many character perspectives during the book that just seemed unnecessary and a bit confusing. In theory the story seems quite captivating, but in reality it never made you sit on the edge of your seat or have to read the next chapter. Unfortunately, I wasn’t that invested in the crime and I wasn’t that curious about who the murderer was which is kind of the whole point of a mystery novel. It was an enjoyable book to cruise along with, but something you could pretty easily put down.", Rating = 2, Created = DateTime.Now},
                new Review { Id = 3, BookId = 3, Title = "Sages review", Text = "With the exception of a few horribly racist remnants of the age these comics come from, it's a delightful collection of some of the best comics ever.", Rating = 4, Created = DateTime.Now},
                new Review { Id = 4, BookId = 4, Title = "Delenns review", Text = "I had to read this one after The Child Called It. It was also compelling and opened my eyes to the foster care system.", Rating = 4, Created = DateTime.Now},
                new Review { Id = 5, BookId = 5, Title = "Shannons review", Text = "This had great pacing, characters, & “lessons”. **Important to note, however: the entire plot is about a kid who’s hiding a gun in his backpack on a mission to his father’s convenience store during the LA riots in order to provide him protection from looting. Not a light subject or for super young readers.**", Rating = 3, Created = DateTime.Now}
                // new Review { Id = , BookId = , Title = "", Text = "", Rating = , Created = DateTime.Now}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
    }
}
