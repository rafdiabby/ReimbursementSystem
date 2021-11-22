using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_T_ReimbursementHistory")]
    public class ReimbursementHistory
    {
        [Key]
        public int id { get; set; }
        //reimbursement id
        public int reimId { get; set; }
        public int statusId { get; set; }
        public DateTime date { get; set; }
        public int amountReimbursed { get; set; }

        //relations
        public virtual Reimbursement Reimbursement { get; set; }
        public virtual Status Status { get; set; }
    }
}
