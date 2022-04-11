using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class CreateReviewViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
        [Required]
        public int Rating { get; set; }

        public int BookId { get; set; }

    }
}
