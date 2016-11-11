using System;
using System.Collections.Generic;
using Chat.Model;
using Chat.Utils;
using System.Net.Http;
using System.Net.Http.Headers;
using Chat.DTOs;
using System.Linq;
using System.Net;
using System.Globalization;
using System.IO;

namespace Chat
{
    public class ChatClient : IChatClient
    {
        private HttpClient client;
        private string username = null;
        private string password = null;

        public ChatClient()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);

            client.BaseAddress = ChatClientSettings.ApiUrl;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Register(string username, string password)
        {
            this.username = username;
            this.password = password;
            RegisterImpl();
        }

        private void RegisterImpl()
        {
            if (!IsRegistered())
            {
                ThrowRegistrationException();
            }

            User dto = new User
            {
                Username = username,
                Password = password
            };

            HttpResponseMessage response = client.PostAsJsonAsync("Login/RegisterAndLogin", dto).Result;
            if (!response.IsSuccessStatusCode)
            {
                username = null;
                password = null;
                throw new AuthenticationException("Prisijungimas nepavyko");
            }
        }

        public bool IsRegistered()
        {
            return username != null;
        }

        public void SendMessage(string to, string message)
        {
            if (!IsRegistered())
            {
                ThrowRegistrationException();
            }

            try
            {
                SendMessageImpl(to, message);
            }
            catch (AuthenticationException)
            {
                RegisterImpl();
                SendMessageImpl(to, message);
            }
        }

        private void SendMessageImpl(string to, string message)
        {
            OutgoingMessage dto = new OutgoingMessage
            {
                To = to,
                Message = message
            };

            HttpResponseMessage response = client.PostAsJsonAsync("api/Message/Send", dto).Result;
            HandleResponse(response);
        }

        public void SendFileMessage(string to, string fileName, byte[] content)
        {
            if (!IsRegistered())
            {
                ThrowRegistrationException();
            }

            try
            {
                SendFileMessageImpl(to, fileName, content);
            }
            catch (AuthenticationException)
            {
                RegisterImpl();
                SendFileMessageImpl(to, fileName, content);
            }
        }

        private void SendFileMessageImpl(string to, string fileName, byte[] content)
        {
            MultipartFormDataContent requestContent = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture));
            requestContent.Add(new StreamContent(new MemoryStream(content)), "File", fileName);
            requestContent.Add(new StringContent(to.ToString()), "To");

            HttpResponseMessage response = client.PostAsync("api/Message/SendFile", requestContent).Result;
            HandleResponse(response);
        }

        public List<Message> GetAllMessages(int maxMessages = 20)
        {
            if (!IsRegistered())
            {
                ThrowRegistrationException();
            }

            try
            {
                return GetAllMessagesImpl(maxMessages);
            }
            catch (AuthenticationException)
            {
                RegisterImpl();
                return GetAllMessagesImpl(maxMessages);
            }
        }

        private List<Message> GetAllMessagesImpl(int maxMessages = 20)
        {
            HttpResponseMessage response = client.GetAsync("api/Message/GetAll?count=" + maxMessages).Result;
            if (!response.IsSuccessStatusCode)
            {
                HandleResponse(response);
                return null;
            }
            else
            {
                List<IncomingMessage> messages = Json.Deserialize<List<IncomingMessage>>(response.Content.ReadAsStringAsync().Result);
                return MapMessages(messages);
            }
        }

        public List<Message> GetUnreadMessages(int maxMessages = 20)
        {
            if (!IsRegistered())
            {
                ThrowRegistrationException();
            }

            try
            {
                return GetUnreadMessagesImpl(maxMessages);
            }
            catch (AuthenticationException)
            {
                RegisterImpl();
                return GetUnreadMessagesImpl(maxMessages);
            }
        }

        private List<Message> GetUnreadMessagesImpl(int maxMessages = 20)
        {
            HttpResponseMessage response = client.GetAsync("api/Message/GetUnread?count=" + maxMessages).Result;
            if (!response.IsSuccessStatusCode)
            {
                HandleResponse(response);
                return null;
            }
            else
            {
                List<IncomingMessage> messages = Json.Deserialize<List<IncomingMessage>>(response.Content.ReadAsStringAsync().Result);
                return MapMessages(messages);
            }
        }

        public void DeleteMessage(int id)
        {
            if (!IsRegistered())
            {
                ThrowRegistrationException();
            }

            try
            {
                DeleteMessageImpl(id);
            }
            catch (AuthenticationException)
            {
                RegisterImpl();
                DeleteMessageImpl(id);
            }
        }

        public void DeleteMessageImpl(int id)
        {
            MessageCommand dto = new MessageCommand
            {
                Id = id
            };

            HttpResponseMessage response = client.PostAsJsonAsync("api/Message/Delete", dto).Result;
            HandleResponse(response);
        }

        public void MarkMessageAsRead(int id)
        {
            if (!IsRegistered())
            {
                ThrowRegistrationException();
            }

            try
            {
                MarkMessageAsReadImpl(id);
            }
            catch (AuthenticationException)
            {
                RegisterImpl();
                MarkMessageAsReadImpl(id);
            }
        }

        private void MarkMessageAsReadImpl(int id)
        {
            MessageCommand dto = new MessageCommand
            {
                Id = id
            };

            HttpResponseMessage response = client.PostAsJsonAsync("api/Message/MarkAsRead", dto).Result;
            HandleResponse(response);
        }

        public byte[] GetFileContent(int id)
        {
            if (!IsRegistered())
            {
                ThrowRegistrationException();
            }

            try
            {
                return GetFileContentImpl(id);
            }
            catch (AuthenticationException)
            {
                RegisterImpl();
                return GetFileContentImpl(id);
            }
        }

        private byte[] GetFileContentImpl(int id)
        {
            HttpResponseMessage response = client.GetAsync("api/Message/GetFileContent?id=" + id).Result;
            if (!response.IsSuccessStatusCode)
            {
                HandleResponse(response);
                return null;
            }
            else
            {
                byte[] result = response.Content.ReadAsByteArrayAsync().Result;
                return result;
            }
        }

        private List<Message> MapMessages(List<IncomingMessage> messages)
        {
            return messages.Select(MapMessage).ToList();
        }

        private Message MapMessage(IncomingMessage message)
        {
            DateTime localTime = TimeZone.CurrentTimeZone.ToLocalTime(message.Date);
            return new Message(message.Id, message.From, message.Message, localTime, message.IsRead, message.IsFile);
        }

        private void ThrowRegistrationException()
        {
            throw new ApplicationException("Prašome pirma prisijungti prie serverio");
        }

        private void HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new AuthenticationException("Prašome pirma prisijungti prie serverio");
                }
                else
                {
                    throw new ApplicationException(GetExceptionMessage(response));
                }
            }
        }

        private string GetExceptionMessage(HttpResponseMessage response)
        {
            string text = response.Content.ReadAsStringAsync().Result.Trim();
            if (text.StartsWith("{"))
            {
                return Json.Deserialize<ErrorMessage>(text)?.exceptionMessage ?? "Įvyko klaida";
            }
            else if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.Forbidden)
            {
                return "Prašome pirma prisijungti prie serverio";
            }
            else
            {
                return "Įvyko klaida";
            }
        }
    }
}
