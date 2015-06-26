using System;
using System.Threading;
using Messenger.WebApi;
using Messenger.WebApi.Enums;

namespace MessengerProject.Client
{
    internal class Chat
    {
        private bool _exitChat;
        private static bool _userIsWriting;
        readonly Thread _updater = new Thread(UpdateText);

        public void Start()
        {
            _exitChat = false;
            _userIsWriting = false;

            GiveStartupInfo();

            ChooseCommands();
        }
    
        private void ChooseCommands()
        {
            while (!_exitChat)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                RequestAction requestAction = (RequestAction) Char.ToLower(key.KeyChar);
                switch (requestAction)
                {
                    case RequestAction.Register:
                        Register();
                        break;
                    case RequestAction.JoinRoom:
                        JoinRoom();
                        break;
                    case RequestAction.CreateRoom:
                        CreateRoom();
                        break;
                    case RequestAction.Message:
                        Message();
                        break;
                    case RequestAction.Exit:
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
            RequestStatus status = RequestStatus.ConnectionError;;
            while (status != RequestStatus.Success)
            {
                Console.WriteLine("Choose username: ");
                Request.Username = Console.ReadLine();
                status = Request.Register().Result;

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
        }

        private void Message()
        {
            _userIsWriting = true;
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
            _userIsWriting = false;
        }

        private static void UpdateText()
        {
            while (true)
            {
                if (!_userIsWriting)
                {
                    ChatInfo chatInfo = Request.GetNewestText().Result;

                    switch (chatInfo.Status)
                    {
                        case RequestStatus.ConnectionError:
                            Console.WriteLine("");
                            Thread.Sleep(1500);
                            break;
                        case RequestStatus.Success:
                            string message = chatInfo.NewMessages;
                            if (message != "")
                            {
                                Console.WriteLine(message);
                            }
                            Thread.Sleep(500);
                            break;
                    }
                }
            }
        }

        private void StartTextUpdater()
        {
            _updater.Start();
        }

        private void GiveStartupInfo()
        {
            Register();

            Console.WriteLine("Commands list:\n" +
                              "To create room press C and type room name \n" +
                              "To joinRoom press J and type room name \n" +
                              "To type message to chat room press M and type your message\n" +
                              "To EXIT press esc");
        }
    }
}
