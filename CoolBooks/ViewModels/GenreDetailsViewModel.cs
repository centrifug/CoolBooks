using CoolBooks.Models;
namespace CoolBooks.ViewModels
{
    public class GenreDetailsViewModel
    {
        public Genre Genre { get; set; }

        public PaginatedList<Book> Books { get; set; }
    }
}
