using System.Collections.Generic;
using Messenger.Displays;

namespace Messenger
{
    public class Server
    {
        private readonly Dictionary<string, ChatRoom> _chatRooms = new Dictionary<string, ChatRoom>();
        private readonly Dictionary<string, ChatUser> _users = new Dictionary<string, ChatUser>();

        public Server()
        {
        }

        public void CreateRoom(string roomName, ChatUser user)
        {
            _chatRooms.Add(roomName, new ChatRoom(roomName, user));
        }

        public void RemoveRoom(string roomName)
        {
            _chatRooms.Remove(roomName);
        }

        public void CreateUser(string username)
        {
            var user = new ChatUser(username,new UserDisplay("NONE",username));
            _users.Add(username,new ChatUser(username, new UserDisplay("NONE",username)));
        }

        public ChatRoom Room(string name)
        {
            return _chatRooms[name];
        }
        public ChatUser User(string name)
        {
            return _users[name];
        }
    }
}