namespace CoolBooks.ViewModels
{
    public class ReportedCommentViewModel
    {
        public int ReportedCommentId { get; set; }
        public string CommentText { get; set; }
        public int Total { get; set; }

        public bool IsDeleted { get; set; }

        public int? reviewId { get; set; }

        public int? commentId { get; set; }
        public int reviewIdNested { get; set; }
    }
}
