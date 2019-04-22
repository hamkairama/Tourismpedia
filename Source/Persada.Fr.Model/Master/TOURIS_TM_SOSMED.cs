namespace Persada.Fr.Model.Master
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class TOURIS_TM_SOSMED
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string TYPE { get; set; }

        [Required]
        [StringLength(500)]
        public string NAME { get; set; }

        [StringLength(500)]
        public string DESCRIPTION { get; set; }

        [StringLength(100)]
        public string ICON { get; set; }

        [StringLength(100)]
        public string URL { get; set; }

        [StringLength(100)]
        public string DATA_EMBED { get; set; }

        public int? HEIGHT { get; set; }

        public int? WIDTH { get; set; }

        [StringLength(100)]
        public string DATA_WIDGET_ID { get; set; }

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
