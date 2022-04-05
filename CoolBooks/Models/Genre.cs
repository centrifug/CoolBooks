using System.ComponentModel.DataAnnotations;

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

        //[Required]
        public List<Book> Books { get; set; } = new List<Book>();

        // CreatedBy?
        // Updated?
        // UPdatedBy?
    }
}
