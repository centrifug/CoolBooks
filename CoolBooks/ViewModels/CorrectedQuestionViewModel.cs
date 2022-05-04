namespace CoolBooks.ViewModels
{
    public class CorrectedQuestionViewModel
    {
        public string Name { get; set; }
        public string CorrectAnswer { get; set; }
        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; } = false;
    }
}
