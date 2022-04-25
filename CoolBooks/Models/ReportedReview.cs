using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.Models
{
    public class ReportedReview
    {
        public int id { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual CoolBooksUser CoolBooksUser { get; set; }

        public DateTime Created { get; set; }

        public int ReviewId { get; set; }

        public Review Review { get; set; }
    }
}
