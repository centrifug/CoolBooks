using CoolBooks.Models;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class CreateBookViewModel
    {
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [MaxLength(17)]

        public string ISBN { get; set; }


        //[Required]
        public List<Author> Authors { get; set; } = new List<Author>();

        //[Required]
        [Display(Name = "Genre")]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        public string ImagePath { get; set; }

        // UserID?
        // CreatedBy?
        // Updated?
        // UPdatedBy?
    }
}
