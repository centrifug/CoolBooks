using System.ComponentModel.DataAnnotations;

namespace CoolBooks.Models
{
    public class Review
    {   
        [Required]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        //[Required]
        public Book Book { get; set; }

        [Required][MaxLength(25)]
        public string Title { get; set; }

        [Required][MaxLength(1000)]
        public string Text { get; set; }

        [Required][Range(1,5)]
        public int Rating { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime Created { get; set; }

        // UserID?
        // CreatedBy?
        // Updated?
        // UPdatedBy?
    }
}
