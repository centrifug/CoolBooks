using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models.Quiz
{
    public class QOption
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Text { get; set; }

        public bool Answer { get; set; }

        public int? QuestionId { get; set; }

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
    }
}
