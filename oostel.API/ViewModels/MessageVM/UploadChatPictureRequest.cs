namespace Oostel.API.ViewModels.MessageVM
{
    public record UploadChatPictureRequest
    {
        public string Message { get; set; }
        public string ReceiverId { get; set; }
        public string SenderId { get; set; }
        public IFormFile MediaFile { get; set; }
    }
}
