namespace CoolBooks.ViewModels
{
    public class CreateCommentViewModel
    {
        public string Text { get; set; }
        
        public int? ReviewId { get; set; }

        public int? CommentId { get; set; }

        public int reviewIdNested { get; set; }

    }
}
