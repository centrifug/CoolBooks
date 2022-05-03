using CoolBooks.Models.Quiz;

namespace CoolBooks.ViewModels
{
    public class EditQuizQuestionViewModel
    {
        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        public int Answer { get; set; }
        public List<EditQuestionOptionViewModel> Options { get; set; } = new List<EditQuestionOptionViewModel>();
    }
}
