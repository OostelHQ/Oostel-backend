namespace Oostel.API.ViewModels.HostelsVM
{
    public class HostelCommentRequest
    {
        public string PostId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public string? ParentCommentId { get; set; }
    }


    public class UpdateCommentRequest
    {
        public string CommentId { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public string? ParentCommentId { get; set; }
    }
}
