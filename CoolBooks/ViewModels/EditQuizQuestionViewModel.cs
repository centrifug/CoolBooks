using CoolBooks.Models.Quiz;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class EditQuizQuestionViewModel
    {
        public int QuestionId { get; set; }

        [Display(Name = "Fråga:")]
        public string QuestionText { get; set; }

        public int Answer { get; set; }
        public List<EditQuestionOptionViewModel> Options { get; set; } = new List<EditQuestionOptionViewModel>();
    }
}
