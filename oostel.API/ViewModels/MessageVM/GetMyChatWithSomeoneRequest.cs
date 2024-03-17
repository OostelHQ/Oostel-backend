using Oostel.Common.Types.RequestFeatures;

namespace Oostel.API.ViewModels.MessageVM
{
    public class GetMyChatWithSomeoneRequest
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public ChatParam? ChatParam { get; set; }
    }

}
