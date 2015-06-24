using System;
using Messenger.Displays;

namespace Messenger
{
    public class ChatUser
    {
        public string Username { get; private set; }

        public string CurrentChatRoomName { get; set; }

        public readonly IDisplay Display;

        public ChatUser(string username, IDisplay display)
        {
            if (username == null) 
                throw new Exception("Username is null");
            Username = username;

            Display = display;
        }
    }
}
