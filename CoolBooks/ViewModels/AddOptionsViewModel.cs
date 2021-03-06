using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class AddOptionsViewModel
    {
        
        [MaxLength(200)]
        [Display(Name = "Alternativ:")]
        public string Text { get; set; }

        public bool Answer { get; set; }
    }
}
