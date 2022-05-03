using CoolBooks.Models.Quiz;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class CreateQuizViewModel
    {
        public string Name { get; set; }

        [Display(Name = "Genre/Tags")]
        public List<QuizGenreViewModel> Genres { get; set; } = new List<QuizGenreViewModel>();

    }
}
