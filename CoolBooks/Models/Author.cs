using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public string? CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual CoolBooksUser CoolBooksUser { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? LastUpdated { get; set; }

        //[Required]
        public List<Book>? Books { get; set; } = new List<Book>();

        public string ImagePath { get; set; }

        [Required]
        [Range(0.00, 5.00)]
        public double Rating { get; set; }

        // UserID?
        // CreatedBy?
        // Updated?
        // UPdatedBy?

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

    }
}
