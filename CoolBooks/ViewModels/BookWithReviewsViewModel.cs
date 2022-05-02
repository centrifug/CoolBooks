using CoolBooks.Models;

namespace CoolBooks.ViewModels
{
    public class BookWithReviewsViewModel
    {
        public Book book    { get; set; }
        public PaginatedList<Review> reviews { get; set; }
        public CreateReviewViewModel review { get; set; }

        public CreateCommentViewModel comment { get; set; }
    }
}
