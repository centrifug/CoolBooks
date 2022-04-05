using System.ComponentModel.DataAnnotations;

namespace CoolBooks.Models
{
    public class BookGenre
    {
        public int BookId { get; set; }
        public int GenreId { get; set; }

        [Required]
        public DateTime Created { get; set; }

        // CreatedBy?
        // Updated?
        // UPdatedBy?
    }
}
