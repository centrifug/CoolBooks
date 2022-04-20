using CoolBooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CoolBooks.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedBook (this ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Book>()
                .HasData(
                new Book { Id = 1, Title = "Sword of Destiny", Description = "Geralt is a witcher, a man whose magic powers, enhanced by long training and a mysterious elixir, have made him a brilliant fighter and a merciless assassin. Yet he is no ordinary murderer: his targets are the multifarious monsters and vile fiends that ravage the land and attack the innocent.", ISBN = "1473231086", Created = DateTime.Now, ImagePath = "/images/Books/1.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 2, Title = "The Thursday Murder Club", Description = "Four septuagenarians with a few tricks up their sleeves, A female cop with her first big case, A brutal murder, Welcome to…, The Thursday Murder Club", ISBN = "0241425441", Created = DateTime.Now, ImagePath = "/images/Books/2.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 3, Title = "Walt Disney's Donald Duck: Lost in the Andes", Description = "Carl Barks Donald Duck and Uncle Scrooge comics are considered among the greatest artistic and storytelling achievements in the history of the medium. After serving a stint at the Walt Disney studios as an in-betweener and a gag-man, Barks began drawing the comic book adventures of Donald Duck in 1942. He quickly mastered every aspect of cartooning and over the next nearly 30 years created some of the most memorable comics ever drawn as well as some of the most memorable characters: Barks introduced Uncle Scrooge, the charmed and insufferable Gladstone Gander, the daffy inventor Gyro Gearloose, the bumbling and heedless Beagle Boys, the Junior Woodchucks, and many others. Barks alternated between longish, sprawling 20- or 30-page adventure yarns filled with the romance of danger, courage, and derring-do, whose exotic locales spanned the globe, and shorter stories that usually revolved around crazily ingenious domestic squabbles between Donald and various members of the Duckburg cast.", ISBN = "1606994743 ", Created = DateTime.Now, ImagePath = "/images/Books/3.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 4, Title = "Pojken som inte fanns", Description = "Pojken som inte fanns är den efterlängtade fristående fortsättningen på Dave Pelzers skakande barndomsskildring om vad som hände efter att han vid tolv års ålder omhändertogs av myndigheterna och placerades i fosterhem.", ISBN = "9170010412", Created = DateTime.Now, ImagePath = "/images/Books/4.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 5, Title = "Troublemaker", Description = "Troublemaker follows the events of the LA Riots through the eyes of 12-year-old Jordan as he navigates school and family. This book will highlight the unique Korean American perspective.", ISBN = "0759554471", Created = DateTime.Now, ImagePath = "/images/Books/5.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 6, Title = "Beautiful World, Where Are You", Description = "Alice, a novelist, meets Felix, who works in a warehouse, and asks him if he’d like to travel to Rome with her. In Dublin, her best friend, Eileen, is getting over a break-up and slips back into flirting with Simon, a man she has known since childhood. Alice, Felix, Eileen, and Simon are still young—but life is catching up with them. They desire each other, they delude each other, they get together, they break apart. They have sex, they worry about sex, they worry about their friendships and the world they live in. Are they standing in the last lighted room before the darkness, bearing witness to something? Will they find a way to believe in a beautiful world?", ISBN = "0374602603", Created = DateTime.Now, ImagePath = "/images/Books/6.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 7, Title = "The Last Thing He Told Me", Description = "Before Owen Michaels disappears, he manages to smuggle a note to his beloved wife of one year: Protect her. Despite her confusion and fear, Hannah Hall knows exactly to whom the note refers: Owen’s sixteen-year-old daughter, Bailey. Bailey, who lost her mother tragically as a child. Bailey, who wants absolutely nothing to do with her new stepmother. As Hannah’s increasingly desperate calls to Owen go unanswered; as the FBI arrests Owen’s boss; as a US Marshal and FBI agents arrive at her Sausalito home unannounced, Hannah quickly realizes her husband isn’t who he said he was. And that Bailey just may hold the key to figuring out Owen’s true identity—and why he really disappeared. Hannah and Bailey set out to discover the truth, together. But as they start putting together the pieces of Owen’s past, they soon realize they are also building a new future.One neither Hannah nor Bailey could have anticipated.", ISBN = "1501171348", Created = DateTime.Now, ImagePath = "/images/Books/7.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 8, Title = "Malibu Rising", Description = "Malibu: August 1983. It’s the day of Nina Riva’s annual end-of-summer party, and anticipation is at a fever pitch. Everyone wants to be around the famous Rivas: Nina, the talented surfer and supermodel; brothers Jay and Hud, one a championship surfer, the other a renowned photographer; and their adored baby sister, Kit. Together, the siblings are a source of fascination in Malibu and the world over—especially as the offspring of the legendary singer Mick Riva. The only person not looking forward to the party of the year is Nina herself, who never wanted to be the center of attention, and who has also just been very publicly abandoned by her pro tennis player husband. Oh, and maybe Hud—because it is long past time to confess something to the brother from whom he’s been inseparable since birth. Jay, on the other hand, is counting the minutes until nightfall, when the girl he can’t stop thinking about promised she’ll be there. And Kit has a couple secrets of her own—including a guest she invited without consulting anyone. By midnight the party will be completely out of control. By morning, the Riva mansion will have gone up in flames. But before that first spark in the early hours before dawn, the alcohol will flow, the music will play, and the loves and secrets that shaped this family’s generations will all come rising to the surface. Malibu Rising is a story about one unforgettable night in the life of a family: the night they each have to choose what they will keep from the people who made them . . . and what they will leave behind.", ISBN = "1524798657", Created = DateTime.Now, ImagePath = "/images/Books/8.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 9, Title = "A ​Court of Silver Flames", Description = "The one person who ignites her temper more than any other is Cassian, the battle-scarred warrior whose position in Rhysand and Feyre's Night Court keeps him constantly in Nesta's orbit. But her temper isn't the only thing Cassian ignites. The fire between them is undeniable, and only burns hotter as they are forced into close quarters with each other. Meanwhile, the treacherous human queens who returned to the Continent during the last war have forged a dangerous new alliance, threatening the fragile peace that has settled over the realms. And the key to halting them might very well rely on Cassian and Nesta facing their haunting pasts. Against the sweeping backdrop of a world seared by war and plagued with uncertainty, Nesta and Cassian battle monsters from within and without as they search for acceptance-and healing-in each other's arms.", ISBN = "168119628X", Created = DateTime.Now, ImagePath = "/images/Books/9.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 10, Title = "Broken (In the Best Possible Way)", Description = "As Jenny Lawson’s hundreds of thousands of fans know, she suffers from depression. In Broken, Jenny brings readers along on her mental and physical health journey, offering heartbreaking and hilarious anecdotes along the way. With people experiencing anxiety and depression now more than ever, Jenny humanizes what we all face in an all-too-real way, reassuring us that we’re not alone and making us laugh while doing it. From the business ideas that she wants to pitch to Shark Tank to the reason why Jenny can never go back to the post office, Broken leaves nothing to the imagination in the most satisfying way. And of course, Jenny’s long-suffering husband Victor―the Ricky to Jenny’s Lucille Ball―is present throughout. A treat for Jenny Lawson’s already existing fans, and destined to convert new ones, Broken is a beacon of hope and a wellspring of laughter when we all need it most.", ISBN = "1250077036", Created = DateTime.Now, ImagePath = "/images/Books/10.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 11, Title = "The Final Girl Support Group", Description = "In horror movies, the final girl is the one who's left standing when the credits roll. The one who fought back, defeated the killer, and avenged her friends. The one who emerges bloodied but victorious. But after the sirens fade and the audience moves on, what happens to her? Lynnette Tarkington is a real-life final girl who survived a massacre twenty-two years ago, and it has defined every day of her life since. And she's not alone. For more than a decade she's been meeting with five other actual final girls and their therapist in a support group for those who survived the unthinkable, putting their lives back together, piece by piece. That is until one of the women misses a meeting and Lynnette's worst fears are realized--someone knows about the group and is determined to take their lives apart again, piece by piece. But the thing about these final girls is that they have each other now, and no matter how bad the odds, how dark the night, how sharp the knife, they will never, ever give up. ", ISBN = "059320123X", Created = DateTime.Now, ImagePath = "/images/Books/11.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 12, Title = "Project Hail Mary", Description = "Ryland Grace is the sole survivor on a desperate, last-chance mission—and if he fails, humanity and the Earth itself will perish. Except that right now, he doesn't know that. He can't even remember his own name, let alone the nature of his assignment or how to complete it. All he knows is that he's been asleep for a very, very long time. And he's just been awakened to find himself millions of miles from home, with nothing but two corpses for company. His crewmates dead, his memories fuzzily returning, he realizes that an impossible task now confronts him. Alone on this tiny ship that's been cobbled together by every government and space agency on the planet and hurled into the depths of space, it's up to him to conquer an extinction-level threat to our species. And thanks to an unexpected ally, he just might have a chance.", ISBN = "0593135202", Created = DateTime.Now, ImagePath = "/images/Books/12.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 13, Title = "People We Meet on Vacation", Description = "Poppy and Alex. Alex and Poppy. They have nothing in common. She’s a wild child; he wears khakis. She has insatiable wanderlust; he prefers to stay home with a book. And somehow, ever since a fateful car share home from college many years ago, they are the very best of friends. For most of the year they live far apart—she’s in New York City, and he’s in their small hometown—but every summer, for a decade, they have taken one glorious week of vacation together. Until two years ago, when they ruined everything. They haven’t spoken since. Poppy has everything she should want, but she’s stuck in a rut. When someone asks when she was last truly happy, she knows, without a doubt, it was on that ill-fated, final trip with Alex. And so, she decides to convince her best friend to take one more vacation together—lay everything on the table, make it all right. Miraculously, he agrees. Now she has a week to fix everything. If only she can get around the one big truth that has always stood quietly in the middle of their seemingly perfect relationship. What could possibly go wrong?", ISBN = "1984806750", Created = DateTime.Now, ImagePath = "/images/Books/13.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 14, Title = "The Anthropocene Reviewed", Description = "A deeply moving and mind-expanding collection of personal essays in the first ever work of non-fiction from #1 internationally bestselling author John Green. The Anthropocene is the current geological age, in which human activity has profoundly shaped the planet and its biodiversity. In this remarkable symphony of essays adapted and expanded from his ground-breaking, critically acclaimed podcast, John Green reviews different facets of the human-centered planet - from the QWERTY keyboard and Halley's Comet to Penguins of Madagascar - on a five-star scale. Complex and rich with detail, the Anthropocene's reviews have been praised as 'observations that double as exercises in memoiristic empathy', with over 10 million lifetime downloads. John Green's gift for storytelling shines throughout this artfully curated collection about the shared human experience; it includes beloved essays along with six all-new pieces exclusive to the book.", ISBN = "0525555218", Created = DateTime.Now, ImagePath = "/images/Books/14.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 15, Title = "Crying in H Mart", Description = "An unflinching, powerful memoir about growing up Korean American, losing her mother, and forging her own identity. In this exquisite story of family, food, grief, and endurance, Michelle Zauner proves herself far more than a dazzling singer, songwriter, and guitarist. With humor and heart, she tells of growing up one of the few Asian American kids at her school in Eugene, Oregon; of struggling with her mother's particular, high expectations of her; of a painful adolescence; of treasured months spent in her grandmother's tiny apartment in Seoul, where she and her mother would bond, late at night, over heaping plates of food. As she grew up, moving to the East Coast for college, finding work in the restaurant industry, and performing gigs with her fledgling band--and meeting the man who would become her husband--her Koreanness began to feel ever more distant, even as she found the life she wanted to live. It was her mother's diagnosis of terminal cancer, when Michelle was twenty-five, that forced a reckoning with her identity and brought her to reclaim the gifts of taste, language, and history her mother had given her.", ISBN = "0525657746", Created = DateTime.Now, ImagePath = "/images/Books/15.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 16, Title = "Empire of Pain: The Secret History of the Sackler Dynasty", Description = "The Sackler name adorns the walls of many storied institutions: Harvard, the Metropolitan Museum of Art, Oxford, the Louvre. They are one of the richest families in the world, known for their lavish donations to the arts and sciences. The source of the family fortune was vague, however, until it emerged that the Sacklers were responsible for making and marketing OxyContin, a blockbuster painkiller that was a catalyst for the opioid crisis.", ISBN = "0385545681", Created = DateTime.Now, ImagePath = "/images/Books/16.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 17, Title = "Lore Olympus: Volume One", Description = "Scandalous gossip, wild parties, and forbidden love—witness what the gods do after dark in this stylish and contemporary reimagining of one of mythology’s most well-known stories from creator Rachel Smythe. Featuring a brand-new, exclusive short story, Smythe’s original Eisner-nominated web-comic Lore Olympus brings the Greek Pantheon into the modern age with this sharply perceptive and romantic graphic novel.", ISBN = "0593160290", Created = DateTime.Now, ImagePath = "/images/Books/17.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 18, Title = "Rule of Wolves", Description = "The Demon King. As Fjerda's massive army prepares to invade, Nikolai Lantsov will summon every bit of his ingenuity and charm—and even the monster within—to win this fight. But a dark threat looms that cannot be defeated by a young king's gift for the impossible.", ISBN = "125014230X", Created = DateTime.Now, ImagePath = "/images/Books/18.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 19, Title = "Firekeeper's Daughter", Description = "Eighteen-year-old Daunis Fontaine has never quite fit in, both in her hometown and on the nearby Ojibwe reservation. She dreams of a fresh start at college, but when family tragedy strikes, Daunis puts her future on hold to look after her fragile mother. The only bright spot is meeting Jamie, the charming new recruit on her brother Levi’s hockey team. Yet even as Daunis falls for Jamie, she senses the dashing hockey star is hiding something. Everything comes to light when Daunis witnesses a shocking murder, thrusting her into an FBI investigation of a lethal new drug. Reluctantly, Daunis agrees to go undercover, drawing on her knowledge of chemistry and Ojibwe traditional medicine to track down the source. But the search for truth is more complicated than Daunis imagined, exposing secrets and old scars. At the same time, she grows concerned with an investigation that seems more focused on punishing the offenders than protecting the victims. Now, as the deceptions—and deaths—keep growing, Daunis must learn what it means to be a strong Anishinaabe kwe (Ojibwe woman) and how far she’ll go for her community, even if it tears apart the only world she’s ever known.", ISBN = "1250766567", Created = DateTime.Now, ImagePath = "/images/Books/19.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Book { Id = 20, Title = "The Spanish Love Deception", Description = "Catalina Martín desperately needs a date to her sister’s wedding. Especially since her little white lie about her American boyfriend has spiralled out of control. Now everyone she knows—including her ex and his fiancée—will be there and eager to meet him. She only has four weeks to find someone willing to cross the Atlantic and aid in her deception. New York to Spain is no short flight and her raucous family won’t be easy to fool. Enter Aaron Blackford—her tall, handsome, condescending colleague—who surprisingly offers to step in. She’d rather refuse; never has there been a more aggravating, blood-boiling, and insufferable man. But Catalina is desperate, and as the wedding draws nearer, Aaron looks like her best option. And she begins to realize he might not be as terrible in the real world as he is at the office.", ISBN = "9798705893843", Created = DateTime.Now, ImagePath = "/images/Books/20.jpg", Rating = 0, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" }
                // new Book { Id = 6, Title = "", Description = "", ISBN = "", Created = DateTime.Now, ImagePath = "/images/Books/6.jpg", Rating = 0 }
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
        public static void SeedAuthor(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasData(
                new Author { Id = 1, FirstName = "Andrzej", LastName = "Sapkowski", BirthDate = new DateTime(1948, 6, 21), Created = DateTime.Now, ImagePath = "/images/Authors/1.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 2, FirstName = "Richard", LastName = "Osman", BirthDate = new DateTime(1970, 11, 28), Created = DateTime.Now, ImagePath = "/images/Authors/2.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 3, FirstName = "Carl", LastName = "Barks", BirthDate = new DateTime(1901, 3, 27), Created = DateTime.Now, ImagePath = "/images/Authors/3.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 4, FirstName = "Dave", LastName = "Pelzer", BirthDate = new DateTime(1960, 12, 29), Created = DateTime.Now, ImagePath = "/images/Authors/4.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 5, FirstName = "John", LastName = "Cho", BirthDate = new DateTime(1972, 6, 16), Created = DateTime.Now, ImagePath = "/images/Authors/5.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 6, FirstName = "Sally", LastName = "Rooney", BirthDate = new DateTime(1991, 2, 20), Created = DateTime.Now, ImagePath = "/images/Authors/6.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 7, FirstName = "Laura", LastName = "Dave", BirthDate = new DateTime(1977, 7, 18), Created = DateTime.Now, ImagePath = "/images/Authors/7.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 8, FirstName = "Taylor", LastName = "Reid", BirthDate = new DateTime(1983, 12, 20), Created = DateTime.Now, ImagePath = "/images/Authors/8.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 9, FirstName = "Sarah", LastName = "Maas", BirthDate = new DateTime(1986, 3, 5), Created = DateTime.Now, ImagePath = "/images/Authors/9.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 10, FirstName = "Jenny", LastName = "Lawson", BirthDate = new DateTime(1973, 12, 29), Created = DateTime.Now, ImagePath = "/images/Authors/10.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 11, FirstName = "Grady", LastName = "Hendrix", BirthDate = new DateTime(1948, 5, 13), Created = DateTime.Now, ImagePath = "/images/Authors/11.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 12, FirstName = "Andy", LastName = "Weir", BirthDate = new DateTime(1972, 6, 16), Created = DateTime.Now, ImagePath = "/images/Authors/12.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 13, FirstName = "Emily", LastName = "Henry", BirthDate = new DateTime(1995, 11, 17), Created = DateTime.Now, ImagePath = "/images/Authors/13.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 14, FirstName = "John", LastName = "Green", BirthDate = new DateTime(1977, 8, 24), Created = DateTime.Now, ImagePath = "/images/Authors/14.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 15, FirstName = "Michelle", LastName = "Zauner", BirthDate = new DateTime(1989, 3, 29), Created = DateTime.Now, ImagePath = "/images/Authors/15.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 16, FirstName = "Patrick", LastName = "Keefe", BirthDate = new DateTime(1976, 1, 15), Created = DateTime.Now, ImagePath = "/images/Authors/16.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 17, FirstName = "Rachel", LastName = "Smythe", BirthDate = new DateTime(1986, 9, 19), Created = DateTime.Now, ImagePath = "/images/Authors/17.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 18, FirstName = "Leigh", LastName = "Bardugo", BirthDate = new DateTime(1975, 5, 6), Created = DateTime.Now, ImagePath = "/images/Authors/18.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 19, FirstName = "Angeline", LastName = "Boulley", BirthDate = new DateTime(1966, 4, 27), Created = DateTime.Now, ImagePath = "/images/Authors/19.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Author { Id = 20, FirstName = "Elena", LastName = "Armas", BirthDate = new DateTime(1989, 1, 30), Created = DateTime.Now, ImagePath = "/images/Authors/20.jpg", CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" }
                // new Author { Id = , FirstName = "", LastName = "", BirthDate = new DateTime(2000, 12, 24), Created = DateTime.Now, ImagePath = "images/Authors/.jpg"}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }

        public static void SeedBookAuthor(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>()
                .HasData(
                new AuthorBook { AuthorId = 1, BookId = 1, Created = DateTime.Now },
                new AuthorBook { AuthorId = 2, BookId = 2, Created = DateTime.Now },
                new AuthorBook { AuthorId = 3, BookId = 3, Created = DateTime.Now },
                new AuthorBook { AuthorId = 4, BookId = 4, Created = DateTime.Now },
                new AuthorBook { AuthorId = 5, BookId = 5, Created = DateTime.Now },
                new AuthorBook { AuthorId = 6, BookId = 6, Created = DateTime.Now },
                new AuthorBook { AuthorId = 7, BookId = 7, Created = DateTime.Now },
                new AuthorBook { AuthorId = 8, BookId = 8, Created = DateTime.Now },
                new AuthorBook { AuthorId = 9, BookId = 9, Created = DateTime.Now },
                new AuthorBook { AuthorId = 10, BookId = 10, Created = DateTime.Now },
                new AuthorBook { AuthorId = 11, BookId = 11, Created = DateTime.Now },
                new AuthorBook { AuthorId = 12, BookId = 12, Created = DateTime.Now },
                new AuthorBook { AuthorId = 13, BookId = 13, Created = DateTime.Now },
                new AuthorBook { AuthorId = 14, BookId = 14, Created = DateTime.Now },
                new AuthorBook { AuthorId = 15, BookId = 15, Created = DateTime.Now },
                new AuthorBook { AuthorId = 16, BookId = 16, Created = DateTime.Now },
                new AuthorBook { AuthorId = 17, BookId = 17, Created = DateTime.Now },
                new AuthorBook { AuthorId = 18, BookId = 18, Created = DateTime.Now },
                new AuthorBook { AuthorId = 19, BookId = 19, Created = DateTime.Now },
                new AuthorBook { AuthorId = 20, BookId = 20, Created = DateTime.Now }
                // new AuthorBook { AuthorId = , BookId = , Created = DateTime.Now },
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }
        public static void SeedGenre(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasData(
                new Genre { Id = 1, Name = "Fantasy", Description = "Fantasy is a genre that uses magic and other supernatural forms as a primary element of plot, theme, and/or setting. Fantasy is generally distinguished from science fiction and horror by the expectation that it steers clear of technological and macabre themes, respectively, though there is a great deal of overlap between the three (collectively known as speculative fiction or science fiction/fantasy)", Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Genre { Id = 2, Name = "Mystery", Description = "Mystery fiction is a loosely-defined term that is often used as a synonym of detective fiction — in other words a novel or short story in which a detective (either professional or amateur) solves a crime. The term \"mystery fiction\" may sometimes be limited to the subset of detective stories in which the emphasis is on the puzzle element and its logical solution (cf. whodunit), as a contrast to hardboiled detective stories which focus on action and gritty realism. However, in more general usage \"mystery\" may be used to describe any form of crime fiction, even if there is no mystery to be solved. For example, the Mystery Writers of America describes itself as \"the premier organization for mystery writers, professionals allied to the crime writing field, aspiring crime writers, and those who are devoted to the genre\".", Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Genre { Id = 3, Name = "Comics", Description = "A comic book or comicbook, also called comic magazine or simply comic, is a publication that consists of comic art in the form of sequential juxtaposed panels that represent individual scenes. Panels are often accompanied by brief descriptive prose and written narrative, usually dialog contained in word balloons emblematic of the comics art form.", Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Genre { Id = 4, Name = "Nonfiction", Description = "Nonfiction is an account or representation of a subject which is presented as fact. This presentation may be accurate or not; that is, it can give either a true or a false account of the subject in question. However, it is generally assumed that the authors of such accounts believe them to be truthful at the time of their composition. Note that reporting the beliefs of others in a nonfiction format is not necessarily an endorsement of the ultimate veracity of those beliefs, it is simply saying that it is true that people believe that (for such topics as mythology, religion). Nonfiction can also be written about fiction, giving information about these other works.", Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Genre { Id = 5, Name = "Childrens", Description = "Children's literature is for readers and listeners up to about age 12. It is often illustrated. The term is used in senses that sometimes exclude young-adult fiction, comic books, or other genres. Books specifically for children existed at least several hundred years ago.", Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Genre { Id = 6, Name = "Fiction", Description = "Fiction is the telling of stories which are not real. More specifically, fiction is an imaginative form of narrative, one of the four basic rhetorical modes. Although the word fiction is derived from the Latin fingo, fingere, finxi, fictum, \"to form, create\", works of fiction need not be entirely imaginary and may include real people, places, and events. Fiction may be either written or oral. Although not all fiction is necessarily artistic, fiction is largely perceived as a form of art or entertainment. The ability to create fiction and other artistic works is considered to be a fundamental aspect of human culture, one of the defining characteristics of humanity.", Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Genre { Id = 7, Name = "Horror", Description = "Horror fiction is fiction in any medium intended to scare, unsettle, or horrify the audience. Historically, the cause of the \"horror\" experience has often been the intrusion of a supernatural element into everyday human experience. Since the 1960s, any work of fiction with a morbid, gruesome, surreal, or exceptionally suspenseful or frightening theme has come to be called \"horror\". Horror fiction often overlaps science fiction or fantasy, all three of which categories are sometimes placed under the umbrella classification speculative fiction. ", Created= DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Genre { Id = 8, Name = "Science fiction", Description = "Science fiction (abbreviated SF or sci-fi with varying punctuation and capitalization) is a broad genre of fiction that often involves speculations based on current or future science or technology. Science fiction is found in books, art, television, films, games, theatre, and other media. In organizational or marketing contexts, science fiction can be synonymous with the broader definition of speculative fiction, encompassing creative works incorporating imaginative elements not found in contemporary reality; this includes fantasy, horror and related genres. Although the two genres are often conflated as science fiction/fantasy, science fiction differs from fantasy in that, within the context of the story, its imaginary elements are largely possible within scientifically established or scientifically postulated laws of nature (though some elements in a story might still be pure imaginative speculation). Exploring the consequences of such differences is the traditional purpose of science fiction, making it a \"literature of ideas\". Science fantasy is largely based on writing entertainingly and rationally about alternate possibilities in settings that are contrary to known reality.", Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Genre { Id = 9, Name = "Romance", Description = "According to the Romance Writers of America, \"Two basic elements comprise every romance novel: a central love story and an emotionally - satisfying and optimistic ending.\" Both the conflict and the climax of the novel should be directly related to that core theme of developing a romantic relationship, although the novel can also contain subplots that do not specifically relate to the main characters' romantic love. Other definitions of a romance novel may be broader, including other plots and endings or more than two people, or narrower, restricting the types of romances or conflicts.", Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Genre { Id = 10, Name ="Mystery", Description = "Mystery fiction is a loosely-defined term that is often used as a synonym of detective fiction — in other words a novel or short story in which a detective (either professional or amateur) solves a crime. The term \"mystery fiction\" may sometimes be limited to the subset of detective stories in which the emphasis is on the puzzle element and its logical solution (cf. whodunit), as a contrast to hardboiled detective stories which focus on action and gritty realism. However, in more general usage \"mystery\" may be used to describe any form of crime fiction, even if there is no mystery to be solved. For example,  Mystery Writers of America describes itself as \"the premier organization for mystery writers, professionals allied to the crime writing field, aspiring crime writers, and those who are devoted to the genre\".", Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" }
                // new Genre { Id = , Name = "", Description = "", Created = DateTime.Now}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }

        public static void SeedBookGenre(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookGenre>()
                .HasData(
                new BookGenre { BookId = 1, GenreId = 1, Created = DateTime.Now },
                new BookGenre { BookId = 2, GenreId = 2, Created = DateTime.Now },
                new BookGenre { BookId = 3, GenreId = 3, Created = DateTime.Now },
                new BookGenre { BookId = 4, GenreId = 4, Created = DateTime.Now },
                new BookGenre { BookId = 5, GenreId = 5, Created = DateTime.Now },
                new BookGenre { BookId = 6, GenreId = 6, Created = DateTime.Now },
                new BookGenre { BookId = 7, GenreId = 2, Created = DateTime.Now },
                new BookGenre { BookId = 8, GenreId = 6, Created = DateTime.Now },
                new BookGenre { BookId = 9, GenreId = 1, Created = DateTime.Now },
                new BookGenre { BookId = 10, GenreId = 4, Created = DateTime.Now },
                new BookGenre { BookId = 11, GenreId = 7, Created = DateTime.Now },
                new BookGenre { BookId = 12, GenreId = 8, Created = DateTime.Now },
                new BookGenre { BookId = 13, GenreId = 9, Created = DateTime.Now },
                new BookGenre { BookId = 14, GenreId = 4, Created = DateTime.Now },
                new BookGenre { BookId = 15, GenreId = 4, Created = DateTime.Now },
                new BookGenre { BookId = 16, GenreId = 4, Created = DateTime.Now },
                new BookGenre { BookId = 17, GenreId = 1, Created = DateTime.Now },
                new BookGenre { BookId = 18, GenreId = 1, Created = DateTime.Now },
                new BookGenre { BookId = 19, GenreId = 10, Created = DateTime.Now },
                new BookGenre { BookId = 20, GenreId = 9, Created = DateTime.Now }
                // new BookGenre { BookId = , GenreId = , Created = DateTime.Now }
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );

        }
        public static void SeedReview(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasData(
                new Review { Id = 1, BookId = 1, Title = "Stefans review", Text = "I liked this short story collection less than the 1st one in this series. The first 3 stories were quite boring and useless and I preferred the on screen version of the last 2 where Ciri first appeared. As the previous book, there is a lot of dialogue and verbal confrontation. There was less fighting and more philosophy . I still enjoyed listening to this volume and I am planning to continue with the series as soon as Ionut Grama will record it (Hurry Up!)", Rating = 5, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 2, BookId = 2, Title = "Kalle Ankas review", Text = "This book was enjoyable, I’m not going to deny that. I liked reading about the quirky characters and their penchant for solving murders. It was an intriguing and funny premise which did in a way deliver. But the problem was it was very slow and even though there were some twists and turns they were quite anticlimactic. There were too many character perspectives during the book that just seemed unnecessary and a bit confusing. In theory the story seems quite captivating, but in reality it never made you sit on the edge of your seat or have to read the next chapter. Unfortunately, I wasn’t that invested in the crime and I wasn’t that curious about who the murderer was which is kind of the whole point of a mystery novel. It was an enjoyable book to cruise along with, but something you could pretty easily put down.", Rating = 2, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 3, BookId = 3, Title = "Sages review", Text = "With the exception of a few horribly racist remnants of the age these comics come from, it's a delightful collection of some of the best comics ever.", Rating = 4, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 4, BookId = 4, Title = "Delenns review", Text = "I had to read this one after The Child Called It. It was also compelling and opened my eyes to the foster care system.", Rating = 4, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 5, BookId = 5, Title = "Shannons review", Text = "This had great pacing, characters, & “lessons”. **Important to note, however: the entire plot is about a kid who’s hiding a gun in his backpack on a mission to his father’s convenience store during the LA riots in order to provide him protection from looting. Not a light subject or for super young readers.**", Rating = 3, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 6, BookId = 6, Title = "Eoins review", Text = "when i was diagnosed with covid i thought that being isolated to my bedroom for two weeks was the most boring thing in the world - Sally Rooney has now proven me wrong.", Rating = 1, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 7, BookId = 7, Title = "Emilys review", Text = "I'm wondering how these books become the winner's of the CoolBooks Choice Awards. Are we reading the same books ? This was....meh ", Rating = 2, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 8, BookId = 8, Title = "Chans review", Text = "i find myself once again reading a TJR book and reconsidering my life decisions leading to me this point", Rating = 3, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 9, BookId = 9, Title = "Cindys review", Text = "I took an edible and now I'm too high to write this review", Rating = 2, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 10, BookId = 10, Title = "The Story Girls review", Text = "Loved this! Some parts were hilarious, some were just very real about her depression, and some I related to.", Rating = 5, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 11, BookId = 11, Title = "Barbaras review", Text = "Grady Hendrix does it again. I thoroughly enjoyed this book, it took me back to the slasher movies I loved watching. I can't wait for his next one ", Rating = 4, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 12, BookId = 12, Title = "James review", Text = "Terrific fun from start to finish. Also the rare book I feel I could happily recommend to just about anyone. My favourite read of the year thus far.", Rating = 5, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 13, BookId = 13, Title = "Ches review", Text = "ladies, if a man pulls a \"sad puppy face\" at you, run as fast as you can.", Rating = 1, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 14, BookId = 14, Title = "Alexandras review", Text = "short, sweet, and just what i needed. i can’t wait to revisit some of these chapters for years to come.", Rating = 4, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 15, BookId = 15, Title = "Jessicas review", Text = "i feel like this book wasnt written for readers, but for MZ herself. its a cathartic exercise through grief and heartbreak and life. its extremely personal, which makes me not want to review this. but i genuinely hope it helped MZ work through her pain and sorrow. im gonna go call my mom now.", Rating = 4, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 16, BookId = 16, Title = "Tracis review", Text = "This is A+ reporting and storytelling. The story of the Sackler family is expertly laid out for the reader. Riveting and sickening. Investigative journalism at its best. Read this book.", Rating = 5, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 17, BookId = 17, Title = "Vinas review", Text = "*PRAYS THE NETFLIX GODS REALLY MAKES THIS A SHOW*", Rating = 5, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 18, BookId = 18, Title = "Aprils review", Text = "Not gonna lie, I do not buy that very convenient ending", Rating = 4, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 19, BookId = 19, Title = "Jessicas review", Text = "If I hear \"Secret Squirrel\" one more time.....", Rating = 3, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Review { Id = 20, BookId = 20, Title = "Hannahs review", Text = "I really suffered through this entire thing to get to the smut only to have him say “I want to feel you milking me, baby“ while using a (most likely) expired condom", Rating = 1, Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" }
                // new Review { Id = , BookId = , Title = "", Text = "", Rating = , Created = DateTime.Now}
                // Den utkommenterade raden är en mall så man kan lägga till mer seed-data med snabbare copy paste arbete. Glöm inte , på raden innan!
                );
        }

        public static void SeedUser(this ModelBuilder modelBuilder)
        {
            CoolBooksUser user = new CoolBooksUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "admin@coolbooks.com",
                NormalizedUserName = "ADMIN@COOLBOOKS.COM",
                Email = "admin@coolbooks.com",
                NormalizedEmail = "ADMIN@COOLBOOKS.COM",
                Name = "Admin Adminson",
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };

            CoolBooksUser user1 = new CoolBooksUser()
            {
                Id = "Stefan",
                UserName = "Stefan@coolbooks.com",
                NormalizedUserName = "STEFAN@COOLBOOKS.COM",
                Email = "stefan@coolbooks.com",
                NormalizedEmail = "STEFAN@COOLBOOKS.COM",
                Name = "Stefan Jonsson",
                LockoutEnabled = false,
                PhoneNumber = "2234567890"
            };

            CoolBooksUser moderator = new CoolBooksUser()
            {
                Id = "moderator",
                UserName = "moderator@coolbooks.com",
                NormalizedUserName = "MODERATOR@COOLBOOKS.COM",
                Email = "moderator@coolbooks.com",
                NormalizedEmail = "MODERATOR@COOLBOOKS.COM",
                Name = "Moderator Modingsson",
                LockoutEnabled = false,
                PhoneNumber = "11999190000"
            };

            PasswordHasher<CoolBooksUser> passwordHasher = new PasswordHasher<CoolBooksUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, ".");
            user1.PasswordHash = passwordHasher.HashPassword(user1, ".");
            moderator.PasswordHash = passwordHasher.HashPassword(moderator, ".");

            modelBuilder.Entity<CoolBooksUser>().HasData(user);
            modelBuilder.Entity<CoolBooksUser>().HasData(user1);
            modelBuilder.Entity<CoolBooksUser>().HasData(moderator);
        }

        public static void SeedRole(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "Moderator", ConcurrencyStamp = "2", NormalizedName = "Moderator" }
                );
        }

        public static void SeedUserRole(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "Stefan" },
                new IdentityUserRole<string>() { RoleId = "c7b013f0-5201-4317-abd8-c211f91b7330", UserId = "moderator" }
                );
        }

        public static void SeedComment(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, reviewId = 1, Text = "INTE GIVANDE!", Created = DateTime.Now, CreatedBy = "Stefan" },
                new Comment { Id = 2, reviewId = 1, Text = "Du har sååååå fel!", Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Comment { Id = 3, reviewId = 2, Text = "Snyggt skrivet!", Created = DateTime.Now, CreatedBy = "b74ddd14-6340-4840-95c2-db12554843e5" },
                new Comment { Id = 4, reviewId = 2, Text = "så jävla bra va!!", Created = DateTime.Now, CreatedBy = "Stefan" },
                
                //nested comments
                new Comment { Id = 5, commentId = 1, Text = "Comment on comment wuwu!", Created = DateTime.Now, CreatedBy = "Stefan" },
                new Comment { Id = 6, commentId = 5, Text = "next level! Comment on comment wuwu!", Created = DateTime.Now, CreatedBy = "Stefan" },
                new Comment { Id = 7, commentId = 6, Text = "Thired level! Comment on comment wuwu!", Created = DateTime.Now, CreatedBy = "Stefan" },
                new Comment { Id = 8, commentId = 1, Text = "en till kommentar på en kommentar", Created = DateTime.Now, CreatedBy = "Stefan" },
                new Comment { Id = 9, commentId = 1, Text = "och igen", Created = DateTime.Now, CreatedBy = "Stefan" },
                new Comment { Id = 10, commentId = 8, Text = "SPÄNNANDE!", Created = DateTime.Now, CreatedBy = "Stefan" }
                );
        }
    }
}
