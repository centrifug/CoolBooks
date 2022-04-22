using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CoolBooks.Models
{
    public class CommentLikes
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public virtual CoolBooksUser CoolBooksUser { get; set; }
        public bool IsLike { get; set; }
        public int CommentId { get; set; }
        public string UserId { get; set; }
    }
}
