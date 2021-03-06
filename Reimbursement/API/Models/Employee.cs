using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_M_Employee")]
    public class Employee
    {
        [Key]
        public string NIK { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int bankAccount { get; set; }
        public Gender gender { get; set; }

        //define relations
        [JsonIgnore]
        public virtual Account Account { get; set; } //one to one
        [JsonIgnore]
        public virtual ICollection<Reimbursement> Reimbursements { get; set; }
    }

    public enum Gender
    { Male, Female}
}
