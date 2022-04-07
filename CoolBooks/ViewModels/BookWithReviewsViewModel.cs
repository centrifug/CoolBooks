using CoolBooks.Models;

namespace CoolBooks.ViewModels
{
    public class BookWithReviewsViewModel
    {
        public Book book    { get; set; }
        public List<Review> reviews { get; set; } = new List<Review>();
    }
}
