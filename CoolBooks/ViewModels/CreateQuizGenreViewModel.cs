using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class CreateQuizGenreViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
