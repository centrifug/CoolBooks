using CoolBooks.Models;
namespace CoolBooks.ViewModels

{
    public class HomeIndexViewModel
    {
        public Book RandomBook { get; set; }

        public List<Book> Books { get; set; }
    }
}
