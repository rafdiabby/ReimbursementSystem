using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace API.ViewModels
{
    public class ProfilVM
    {
        public string NIK { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int bankAccount { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }
        public string roleName { get; set; }
    }
}
