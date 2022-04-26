using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class EditReviewViewModel
    {
        public int Id { get; set; }

        public int? BookId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Text { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime Created { get; set; }


    }
}
