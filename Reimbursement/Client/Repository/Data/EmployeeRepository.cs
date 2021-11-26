
using API.Models;
using API.Viewmodels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Client.Base.Url;
using Client.Repository;
using API.ViewModels;

namespace Client.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<ResultVM> Register(RegisterVM register)
        {
            ResultVM entities = new ResultVM();
            StringContent content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(request + "Register", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<ResultVM>(apiResponse);
            }
            return entities;
        }

        public HttpStatusCode DeleteEmployees(string id)
        {
            var result = httpClient.DeleteAsync(request + "Hapus?nik=" + id).Result;
            return result.StatusCode;
        }

        public async Task<List<GetRoleVM>> GetRole(string id)
        {
            List<GetRoleVM> entities = new List<GetRoleVM>();

            using (var response = await httpClient.GetAsync(request + "GetRole?nik=" + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<GetRoleVM>>(apiResponse);
            }
            return entities;
        }

        public async Task<ResultVM> AddAccountRole(AccountRole accountRole)
        {
            ResultVM entities = new ResultVM();
            StringContent content = new StringContent(JsonConvert.SerializeObject(accountRole), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(request + "AddAccountRole", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<ResultVM>(apiResponse);
            }
            return entities;
        }
    }
}
