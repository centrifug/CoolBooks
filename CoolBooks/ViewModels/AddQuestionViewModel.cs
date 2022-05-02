using CoolBooks.Models.Quiz;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class AddQuestionViewModel
    {
        [Required]
        [MaxLength(200)]
        public string Text { get; set; }

        public int QuizId { get; set; }

        public List<AddOptionsViewModel> Options { get; set; }

        public AddQuestionViewModel()
        {
            Options = new List<AddOptionsViewModel>();
            Options.Add(new AddOptionsViewModel());
            Options.Add(new AddOptionsViewModel());
            Options.Add(new AddOptionsViewModel());
            Options.Add(new AddOptionsViewModel());
        }
    }
}
