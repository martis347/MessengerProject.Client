using System;

namespace Messenger.Displays
{
    public class RoomDisplay: IDisplay
    {
        public void Write(string message)
        {
            Console.WriteLine(String.Format("Chat room informs: {0}",message));
        }     
    }
}