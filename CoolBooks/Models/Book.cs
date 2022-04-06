using System.ComponentModel.DataAnnotations;

namespace CoolBooks.Models
{
    public class Book
    {
        [Required][Key]
        public int Id { get; set; }

        [Required][MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [MaxLength(17)][Required] // 13 siffrigt med 4 bindesstreck.
        public string ISBN { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime Created { get; set; }

        //[Required]
        public List<Review> Reviews { get; set; } = new List<Review>();

        //[Required]
        public List<Author> Authors { get; set; } = new List<Author>();

        //[Required]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        // UserID?
        // CreatedBy?
        // Updated?
        // UPdatedBy?
        // public something Image { get; set; }
    }
}
