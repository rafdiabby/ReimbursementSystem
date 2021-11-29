using API.Models;
using Client.Base.Url;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Repository.Data
{
    public class RoleRepository : GeneralRepository<Role, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        public RoleRepository(Address address, string request = "Roles/") : base(address, request)
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
