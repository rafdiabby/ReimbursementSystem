using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public int reimId { get; set; }
        public string file { get; set; }

        [JsonIgnore]
        [ForeignKey("reimId")]
        public virtual Reimbursement Reimbursement { get; set; } 
    }
}
