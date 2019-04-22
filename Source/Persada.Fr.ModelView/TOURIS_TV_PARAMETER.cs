namespace Persada.Fr.ModelView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TOURIS_TV_PARAMETER
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string PARAMETER_CODE { get; set; }

        [StringLength(500)]
        public string PARAMETER_DESCRIPTION { get; set; }

        [Required]
        [StringLength(500)]
        public string PARAMETER_VALUE { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }
    }
}
