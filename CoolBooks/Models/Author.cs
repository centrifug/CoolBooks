using System.ComponentModel.DataAnnotations;

namespace CoolBooks.Models
{
    public class Author
    {
        [Required]
        public int Id { get; set; }

        [Required][MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime Created { get; set; }

        //[Required]
        public List<Book> Books { get; set; } = new List<Book>();


        // UserID?
        // CreatedBy?
        // Updated?
        // UPdatedBy?
        // public something Image { get; set; }

    }
}
