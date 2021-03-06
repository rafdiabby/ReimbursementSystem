using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.ViewModels
{
    public class RegisterVM
    {
        public string NIK { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int bankAccount { get; set; }
        public Gender gender { get; set; }
        public string Password { get; set; }
        public int roleId { get; set; }
    }
}
