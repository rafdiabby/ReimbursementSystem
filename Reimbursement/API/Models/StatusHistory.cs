using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_T_StatusHistory")]
    public class StatusHistory
    {
        [Key]
        public int id { get; set; }
        
        //reimbursement id
        public int reimId { get; set; }
        public int statusId { get; set; }
        public DateTime date { get; set; }

        //relations
        [JsonIgnore]
        [ForeignKey("reimId")]
        public virtual Reimbursement Reimbursement { get; set; }
        [JsonIgnore]
        [ForeignKey("statusId")]
        public virtual Status Status { get; set; }

    }
}
