using System.ComponentModel.DataAnnotations;

namespace CoolBooks.Models
{
    public class AuthorBook
    {
        public int? BookId { get; set; }
        public int? AuthorId { get; set; }

        [Required]
        public DateTime Created { get; set; }

        // CreatedBy?
        // Updated?
        // UPdatedBy?
    }
}
