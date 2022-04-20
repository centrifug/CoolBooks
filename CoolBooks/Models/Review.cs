using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]        
        public string CreatedBy { get; set; }
        
        [ForeignKey("CreatedBy")]
        public virtual CoolBooksUser CoolBooksUser { get; set; }
        public string? UpdatedBy     { get; set; }

        public DateTime? LastUpdated { get; set; }

        public List<Comment>? Comments { get; set; } = new List<Comment>();


    }
}
