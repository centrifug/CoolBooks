using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models.Quiz
{
    public class QuizGenre
    {
        [Required]
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public string? CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual CoolBooksUser CoolBooksUser { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? LastUpdated { get; set; }

        public List<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}
