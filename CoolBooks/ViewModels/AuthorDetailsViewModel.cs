using CoolBooks.Models;
namespace CoolBooks.ViewModels
{
    public class AuthorDetailsViewModel
    {
        public Author Author { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
