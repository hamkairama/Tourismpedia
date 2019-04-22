namespace Persada.Fr.Model.Master
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TOURIS_TM_CONTACT_US
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string NAME_SENDER { get; set; }

        [Required]
        [StringLength(100)]
        public string EMAIL_SENDER { get; set; }

        [Required]
        [StringLength(100)]
        public string PHONE_SENDER { get; set; }

        [StringLength(500)]
        public string DESCRIPTION { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [Required]
        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }
    }
}
