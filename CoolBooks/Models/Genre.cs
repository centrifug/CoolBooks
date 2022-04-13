using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models
{
    public class Genre
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual CoolBooksUser CoolBooksUser { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? LastUpdated { get; set; }

        //[Required]
        public List<Book> Books { get; set; } = new List<Book>();

        // CreatedBy?
        // Updated?
        // UPdatedBy?
    }
}
