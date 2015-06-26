using Messenger.WebApi.Enums;

namespace Messenger.WebApi
{
    public class ChatInfo
    {
        public string NewMessages { get; set; }
        public int UsersInRoom { get; set; }
        public RequestStatus Status { get; set; }
    }
}