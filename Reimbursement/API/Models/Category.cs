using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_M_Category")]
    public class Category
    {
        [Key]
        public int id { get; set; }
        public string categoryName { get; set; }
        public int maxValue { get; set; }

        //relations
        [JsonIgnore]
        public virtual ICollection<Reimbursement> Reimbursements { get; set; }
    }
}
