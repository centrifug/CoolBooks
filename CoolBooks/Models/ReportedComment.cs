using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models
{
    public class ReportedComment
    {
        public int id { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual CoolBooksUser CoolBooksUser { get; set; }

        public DateTime Created { get; set; }

        public int CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}
