namespace Oostel.API.ViewModels.MessageVM
{
    public record DeleteMyChatWithSomeoneRequest
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
