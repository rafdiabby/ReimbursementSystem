using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_T_AccountRole")]
    public class AccountRole
    {
        [Key]
        public int accountRoleId { get; set; }
        public string NIK { get; set; }
        public int roleId { get; set; }

        //define relations
        [ForeignKey("NIK")]
        public virtual Account Account { get; set; }
        [ForeignKey("roleId")]
        public virtual Role Role { get; set; }
    }
}
