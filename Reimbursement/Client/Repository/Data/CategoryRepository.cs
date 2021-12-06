using API.Models;
using Client.Base.Url;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Net.Http;

namespace Client.Repository.Data
{
    public class CategoryRepository : GeneralRepository<Category, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        public CategoryRepository(Address address, string request = "Categorys/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
    }
}
