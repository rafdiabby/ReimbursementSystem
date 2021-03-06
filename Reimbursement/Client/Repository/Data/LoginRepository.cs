using API.Viewmodels;
using API.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Client.Base.Url;
using Client.Repository;
using API.Models;

namespace Client.Repositories.Data
{
    public class LoginRepository : GeneralRepository<LoginVM, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        public LoginRepository(Address address, string request = "Logins/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public async Task<JWTokenVM> Auth(LoginVM login)
        {
            JWTokenVM token = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(request + "Login", content);

            string apiResponse = await result.Content.ReadAsStringAsync();
            token = JsonConvert.DeserializeObject<JWTokenVM>(apiResponse);

            return token;
        }

        public async Task<ResultVM> Cek(LoginVM login)
        {
            ResultVM entities = new ResultVM();
            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(request + "Cek", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<ResultVM>(apiResponse);
            }
            return entities;
        }

        public async Task<ResultVM> ResetPW(Account account)
        {
            ResultVM entities = new ResultVM();
            StringContent content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync(request + "ResetPassword", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<ResultVM>(apiResponse);
            }
            return entities;
        }
    }
}
