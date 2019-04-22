using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persada.Fr.Model
{
    [Table("ClientKey")]
    public class ClientKey
    {
        [Key]
        public int ClientKeyID { get; set; }
        public int CompanyID { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public DateTime CreateOn { get; set; }
        public int UserID { get; set; }
    }
}
