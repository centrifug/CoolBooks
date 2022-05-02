using System.ComponentModel.DataAnnotations;

namespace CoolBooks.Models.Quiz
{
    public class QuizQuizGenre
    {
        public int QuizId { get; set; }
        public int QuizGenreId { get; set; }

        [Required]
        public DateTime Created { get; set; }

    }
}
