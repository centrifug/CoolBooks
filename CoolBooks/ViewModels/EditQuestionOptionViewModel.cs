using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class EditQuestionOptionViewModel
    {
        [MaxLength(200)]
        [Display(Name = "Alternativ:")]
        public string Text { get; set; }

        [Display(Name = "Svar:")]
        public bool Answer { get; set; }
    }
}