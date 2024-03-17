namespace Oostel.API.ViewModels.MessageVM
{
    public record SendMessageRequest
    {
        public string Message { get; set; }
        public string ReceiverId { get; set; }
        public string SenderId { get; set; }
    }
}
