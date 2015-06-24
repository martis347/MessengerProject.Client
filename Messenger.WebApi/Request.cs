using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Messenger.WebApi
{
    public static class Request
    {
        public static string Username { set; private get; }
        private static string RoomName { set; get; }

        public static async Task<bool> Register()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1234/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(String.Concat("api/users/createuser?username=", Username));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static async Task<bool> JoinRoom(string roomName)
        {
            RoomName = roomName;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1234/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync(String.Concat("api/users/joinRoom?username=", Username, "&&roomName=", roomName));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }

        public static async Task<bool> CreateRoom(string roomName)
        {
            RoomName = roomName;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1234/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(String.Concat("api/users/createRoom?username=", Username, "&&roomName=", roomName));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static async Task<bool> Write(string message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1234/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(String.Concat("api/message/send?username=",Username,"&message=",message,"&roomname=",RoomName));

                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}