using CoolBooks.Models.Quiz;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class CreateQuizViewModel
    {
        [Display(Name = "Vad ska Quizet heta?")]
        public string Name { get; set; }

        [Display(Name = "Genre/Tagar")]
        public List<QuizGenreViewModel> Genres { get; set; } = new List<QuizGenreViewModel>();

    }
}
