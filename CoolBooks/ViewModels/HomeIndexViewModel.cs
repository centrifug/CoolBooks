using CoolBooks.Models;
namespace CoolBooks.ViewModels

{
    public class HomeIndexViewModel
    {
        public Book RandomBook { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();

        public List<Book> AllBooks { get; set; } = new List<Book>();
    }
}
