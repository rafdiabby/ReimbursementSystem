using API.Models;
using Client.Base.Url;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class ReimburseRepository : GeneralRepository<Reimbursement, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;

        public ReimburseRepository(Address address, string request = "Reimbursements/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public HttpStatusCode RequestReim(Reimbursement reimbursement)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(reimbursement), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request, content).Result;
            return result.StatusCode;
        }

        public HttpStatusCode AddAttachment(Attachment attachment)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(attachment), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + "Attachments" , content).Result;
            return result.StatusCode;
        }

        public async Task<int> GetLastReimId()
        {
            var result = 0;
            using (var response = await httpClient.GetAsync(request + "LastId/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<int>(apiResponse);
            }
            return result;
        }

        public HttpStatusCode UpdateStatus(Reimbursement reimbursement)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(reimbursement), Encoding.UTF8, "application/json");
            var result = httpClient.PatchAsync(address.link + request+ "UpdateStatus", content).Result;
            return result.StatusCode;
        }
        public HttpStatusCode UpdateStatusHistory(StatusHistory statusHistory)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(statusHistory), Encoding.UTF8, "application/json");
            var result = httpClient.PatchAsync(address.link + "StatusHistories", content).Result;
            return result.StatusCode;
        }
    }
}
