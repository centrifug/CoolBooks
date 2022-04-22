using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CoolBooks.Models
{
    public class ReviewLikes
    {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public virtual CoolBooksUser CoolBooksUser { get; set; }
        public bool IsLike { get; set; }
        public int ReviewId { get; set; }
        public string UserId { get; set; } 
    }
}
