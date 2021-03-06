
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models.Quiz
{
    public class QuizTaken
    {
        [Required]
        public int Id { get; set; }

        public int? QuizID { get; set; }
        public Quiz? Quiz { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public int MaxScore { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public string? CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual CoolBooksUser CoolBooksUser { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? LastUpdated { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;




    }
}
