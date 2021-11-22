using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_M_Status")]
    public class Status
    {
        [Key]
        public int id { get; set; }
        public string statusName { get; set; }

        //relations
        public virtual ICollection<Reimbursement> Reimbursements { get; set; }
        public virtual ICollection<StatusHistory> StatusHistories { get; set; }
    }
}
