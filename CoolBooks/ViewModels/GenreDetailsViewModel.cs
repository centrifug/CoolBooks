using CoolBooks.Models;
namespace CoolBooks.ViewModels
{
    public class GenreDetailsViewModel
    {
        public Genre Genre { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
