using API.ViewModels;
using Client.Base.Url;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Client.Repository.Data
{
    public class MailRepository : GeneralRepository<MailRequestVM, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        public MailRepository(Address address, string request = "Mails/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public HttpStatusCode Send(MailRequestVM mail)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(mail), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "Send/", content).Result;
            return result.StatusCode;
        }
    }
    
}
