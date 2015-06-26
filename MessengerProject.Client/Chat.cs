using System;
using System.IO;
using System.Threading;
using Messenger.WebApi;
using Action = MessengerProject.Client.Enums.Action;

namespace MessengerProject.Client
{
    internal class Chat
    {
        private bool _exitChat;

        readonly Thread _updater = new Thread(UpdateText);

        public void Start()
        {
            _exitChat = false;

            GiveStartupInfo();

            ChooseCommands();
        }
    
        private void ChooseCommands()
        {
            while (!_exitChat)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Action action = (Action) Char.ToLower(key.KeyChar);
                switch (action)
                {
                    case Action.Register:
                        Register();
                        break;
                    case Action.JoinRoom:
                        JoinRoom();
                        break;
                    case Action.CreateRoom:
                        CreateRoom();
                        break;
                    case Action.Message:
                        Message();
                        break;
                    case Action.Exit:
                        _exitChat = true;
                        _updater.Abort();
                        return;
                    default:
                        Console.WriteLine("\bIncorrect command");
                       break;
                }
            }
        }

        private void CreateRoom()
        {
            Console.WriteLine("\bEnter room name\n");

            RequestStatus status = Request.CreateRoom(Console.ReadLine()).Result;

            switch (status)
            {
                case RequestStatus.ConnectionError:
                    Console.WriteLine("No response from server");
                    break;
                case RequestStatus.RoomAlreadyExists:
                    Console.WriteLine("Room with given name already exists");
                    break;
                case RequestStatus.Success:
                    Console.WriteLine("Successfully created room");
                    StartTextUpdater();
                    break;
            }
        }

        private void JoinRoom()
        {
            Console.WriteLine("\bEnter room name\n");

            RequestStatus status = Request.JoinRoom(Console.ReadLine()).Result;

            switch (status)
            {
                case RequestStatus.ConnectionError:
                    Console.WriteLine("No response from server");
                    break;
                case RequestStatus.RoomNotFound:
                    Console.WriteLine("Room with given name was not found");
                    break;
                case RequestStatus.Success:
                    Console.WriteLine("Successfully joined room");
                    StartTextUpdater();
                    break;
            }
        }

        private void Register()
        {
            RequestStatus status = Request.Register().Result;

            switch (status)
            {
                case RequestStatus.ConnectionError:
                    Console.WriteLine("No response from server");
                    break;
                case RequestStatus.UserAlreadyExists:
                    Console.WriteLine("User with given username already exists");
                    break;
                case RequestStatus.Success:
                    Console.WriteLine("Successfully registered");
                    break;
            }
        }

        private void Message()
        {
            Console.WriteLine("\bEnter your message\n");

            RequestStatus status = Request.Write(Console.ReadLine()).Result;

            switch (status)
            {
                case RequestStatus.ConnectionError:
                    Console.WriteLine("No response from server");
                    break;
                case RequestStatus.Success:
                    break;
            }
        }

        private static void UpdateText()
        {
            while (true)
            {
                ChatInfo chatInfo = Request.GetNewestText().Result;

                switch (chatInfo.Status)
                {
                    case RequestStatus.ConnectionError:
                        Console.WriteLine("No response from server");
                        Thread.Sleep(1500);
                        break;
                    case RequestStatus.Success:
                        Console.WriteLine(chatInfo.NewMessages);
                        Thread.Sleep(500);
                        break;
                }
            }
        }

        private void StartTextUpdater()
        {
            _updater.Start();
        }

        private static void GiveStartupInfo()
        {
            GetUsername();

            Console.WriteLine("Commands list:\n" +
                              "To register press R\n" +
                              "To create room press C and type room name \n" +
                              "To joinRoom press J and type room name \n" +
                              "To type message to chat room press M and type your message\n" +
                              "To EXIT press esc");
        }

        private static void GetUsername()
        {
            Console.WriteLine("Choose username: ");
            Request.Username = Console.ReadLine();
        }
    }
}
