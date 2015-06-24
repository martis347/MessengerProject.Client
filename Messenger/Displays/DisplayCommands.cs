using System;

namespace Messenger.Displays
{
    public static class DisplayCommands
    {
        public static string NewUserAdded(string username)
        {
            return String.Format("User {0} added to chat", username);
        }

    }
}