using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("Tb_M_Account")]
    public class Account
    {
        [Key]
        public string NIK { get; set; }
        [Required]
        public string password { get; set; }
        //define relations
        public virtual Employee Employee { get; set; }
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
}
}
