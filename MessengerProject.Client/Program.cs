﻿using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace MessengerProject.Client
{
    class Program
    {
        static bool _exitSystem;

        #region Leave room on exit
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add = true);
        private delegate bool EventHandler();

        private static bool ActionsOnExit()
        {
            Console.WriteLine("TYVM");
            Thread.Sleep(1000);
            _exitSystem = true;
            return true;
        }

        private static void KeepThreadOn()
        {
            while (!_exitSystem)
            {
                Thread.Sleep(500);
            }
        }
        #endregion

        static void Main(string[] args)
        {
            SetConsoleCtrlHandler(ActionsOnExit);

            Program p = new Program();
            p.Start();

            KeepThreadOn();
        }

        private void Start()
        {
            Chat chat = new Chat();
            chat.Start();
            ActionsOnExit();
        }

    }
}
