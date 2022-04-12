using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.ViewModels
{
    public class EditBookViewModel
    {
        public int BookId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [MaxLength(17)]

        public string ISBN { get; set; }

        //[Required]
        public List<BookAuthorViewModel> Authors { get; set; } = new List<BookAuthorViewModel>();

        //[Required]
        [Display(Name = "Genre")]
        public List<BookGenreViewModel> Genres { get; set; } = new List<BookGenreViewModel>();

        public bool IsDeleted { get; set; }

        [NotMapped]
        [Display(Name = "Upload File")]
        public IFormFile ImageFile { get; set; }


        // UserID?
        // CreatedBy?
        // Updated?
        // UPdatedBy?
    }
}
