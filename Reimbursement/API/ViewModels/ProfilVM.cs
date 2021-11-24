using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class ProfilVM
    {
        public string NIK { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int bankAccount { get; set; }
        public Gender gender { get; set; }
        public string roleName { get; set; }
    }
}
