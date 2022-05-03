using CoolBooks.Models.Quiz;

namespace CoolBooks.ViewModels
{
    public class EditQuizViewModel
    {
        public int QuizId { get; set; }

        public string QuizName { get; set; }

        public bool IsDeleted { get; set; }

        public List<Question>? Questions { get; set; } 
    }
}
