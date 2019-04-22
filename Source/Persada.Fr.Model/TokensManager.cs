using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persada.Fr.Model
{
    [Table("TokensManager")]
    public class TokensManager
    {
        [Key]
        public int TokenID { get; set; }
        public string TokenKey { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CompanyID { get; set; }
    }
}
