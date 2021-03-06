using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_T_Reimbursement")]
    public class Reimbursement
    {
        [Key]
        public int id { get; set; }
        public string NIK { get; set; }
        public DateTime date { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public string statusDetails { get; set; }
        public int categoryId { get; set; }
        public int statusId { get; set; }

        //relations
        [JsonIgnore]
        [ForeignKey("NIK")]
        public virtual Employee Employee { get; set; }
        [JsonIgnore]
        [ForeignKey("categoryId")]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        [ForeignKey("statusId")]
        public virtual Status Status { get; set; }
        [JsonIgnore]
        public virtual ICollection<StatusHistory> StatusHistories { get; set; }
        [JsonIgnore]
        public virtual ICollection<Attachment> Attachments  { get; set; }
    }
}
