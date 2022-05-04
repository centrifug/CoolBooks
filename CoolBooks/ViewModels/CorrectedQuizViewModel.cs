using CoolBooks.Models.Quiz;

namespace CoolBooks.ViewModels
{
    public class CorrectedQuizViewModel
    {
        public string QuizName { get; set; }
        public List<CorrectedQuestionViewModel> Quiz { get; set; } = new List<CorrectedQuestionViewModel>();

        public int Score { get; set; }

        public int NumberOfQuestions { get; set; }

        public bool QuizAlreadyTaken { get; set; } = false;
    }
}
