using System;
using System.Threading;
using System.Threading.Tasks;
using Messenger.WebApi;

namespace MessengerProject.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Chat chat = new Chat();
            chat.Start();
        }
    }
}
