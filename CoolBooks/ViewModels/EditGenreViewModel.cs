using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class EditGenreViewModel
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }


    }
}
