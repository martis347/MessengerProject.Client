using System;

namespace Messenger.Displays
{
    public class UserDisplay: IDisplay
    {
        private readonly string _chatRoom;
        private readonly string _username;

        public UserDisplay(string chatRoom, string username)
        {
            if (chatRoom == null) 
                throw new ArgumentNullException("chatRoom");
            _chatRoom = chatRoom;

            if (username == null) 
                throw new ArgumentNullException("username");
            _username = username;
        }

        public void Write(string message)
        {
            Console.WriteLine(String.Format("{0}: {1}", _username, message));
        }
    }
}