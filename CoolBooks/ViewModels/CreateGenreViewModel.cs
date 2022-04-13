using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class CreateGenreViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
