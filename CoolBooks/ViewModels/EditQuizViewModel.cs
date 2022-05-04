using CoolBooks.Models.Quiz;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class EditQuizViewModel
    {
        public int QuizId { get; set; }

        [Display(Name = "Namn på quizet:")]
        public string QuizName { get; set; }

        [Display(Name = "Ta bort")]
        public bool IsDeleted { get; set; }

        public List<Question>? Questions { get; set; } 
    }
}
