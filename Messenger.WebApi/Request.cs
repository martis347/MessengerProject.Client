using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Messenger.WebApi.Enums;

namespace Messenger.WebApi
{
    public static class Request
    {
        public static string  Username { set; private get; }
        private static string RoomName { set; get; }

        public static async Task<RequestStatus> Register()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1234/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(String.Concat("api/users/createuser?username=", Username));
                    RequestStatus request = GetRequestStatus(response);
                    return request;
                }
                catch (Exception)
                {
                    return RequestStatus.ConnectionError;
                }
            }
        }

        public static async Task<RequestStatus> JoinRoom(string roomName)
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
                    RequestStatus request = GetRequestStatus(response);
                    return request;
                }
                catch (Exception)
                {
                    return RequestStatus.ConnectionError;
                }

            }
        }

        public static async Task<RequestStatus> CreateRoom(string roomName)
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
                    RequestStatus request = GetRequestStatus(response);
                    return request;
                }
                catch (Exception)
                {
                    return RequestStatus.ConnectionError;
                }
            }
        }

        public static async Task<RequestStatus> Write(string message)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1234/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(String.Concat("api/message/send?username=",Username,"&message=",message,"&roomname=",RoomName));
                    RequestStatus request = GetRequestStatus(response);
                    return request;
                }
                catch (Exception)
                {
                    return RequestStatus.ConnectionError;
                }
            }
        }

        public static async Task<ChatInfo> GetNewestText()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1234/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync(String.Concat("api/message/getText?username=", Username, "&roomname=", RoomName));
                    
                    ChatInfo request = GetChatInfo(response);
                    return request;
                }
                catch (Exception)
                {
                    return new ChatInfo() {Status = RequestStatus.ConnectionError};
                }
            }
        }

        private static string FormatResponse(string value)
        {
            value = value.Replace("\"", "");
            value = value.Replace("\\n", "\n");
            return value;
        }

        private static RequestStatus GetRequestStatus(HttpResponseMessage response)
        {
            var value = response.Content.ReadAsStringAsync().Result;
            return (RequestStatus)Enum.Parse(typeof (RequestStatus), value);
        }

        private static ChatInfo GetChatInfo(HttpResponseMessage response)
        {
            var value = response.Content.ReadAsStringAsync().Result;

            JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            ChatInfo info = (ChatInfo)jsonSerializer.DeserializeObject(value);

            info.NewMessages = FormatResponse(info.NewMessages);
            return info;
        }

    }
}