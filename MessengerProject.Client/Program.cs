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
            Console.WriteLine("Choose username: ");
            Request.Username = Console.ReadLine();

            Console.WriteLine("Commands list:\n" +
                              "To register press R\n" +
                              "To create room press C and type room name \n" +
                              "To joinRoom press J and type room name \n" +
                              "To type message to chat room press M and type your message\n" +
                              "To EXIT press esc");
            while (true)
            {
                ConsoleKeyInfo action = Console.ReadKey();

                switch (action.KeyChar)
                {
                    case 'R':
                        Console.WriteLine(Request.Register().Result
                            ? "\bSuccesfully registered"
                            : "\bRegistration failed");
                        break;
                    case 'r':
                        Console.WriteLine(Request.Register().Result
                            ? "\bSuccesfully registered"
                            : "\bRegistration failed");
                        break;
                    case 'j':
                        Console.WriteLine("\bEnter room name\n");
                        Console.WriteLine(Request.JoinRoom(Console.ReadLine()).Result
                            ? "Successfully joined room"
                            : "Failed to join room");
                        break;
                    case 'J':
                        Console.WriteLine("\bEnter room name\n");
                        Console.WriteLine(Request.JoinRoom(Console.ReadLine()).Result
                            ? "Successfully joined room"
                            : "Failed to join room");
                        break;
                    case 'c':
                        Console.WriteLine("\bEnter room name\n");
                        Console.WriteLine(Request.CreateRoom(Console.ReadLine()).Result
                            ? "Successfully created room"
                            : "Failed to create room");
                        break;
                    case 'C':
                        Console.WriteLine("\bEnter room name\n");
                        Console.WriteLine(Request.CreateRoom(Console.ReadLine()).Result
                            ? "Successfully created room"
                            : "Failed to create room");
                        break;
                    case 'M':
                        Console.WriteLine("\bEnter your message\n");
                        Console.WriteLine(Request.Write(Console.ReadLine()).Result
                            ? ""
                            : "Failed to send message");
                        break;
                    case 'm':
                        Console.WriteLine(Request.Write(Console.ReadLine()).Result
                            ? "\b"
                            : "\bFailed to send message");
                        break;
                    case 'u':
                        var a = Request.GetNewestText().Result; 
                        Console.WriteLine(a);
                        break;
                    case (char)27:
                        return;
                }
            }
        }
    }
}
