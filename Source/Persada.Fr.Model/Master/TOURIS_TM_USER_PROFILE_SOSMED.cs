namespace Persada.Fr.Model.Master
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TOURIS_TM_USER_PROFILE_SOSMED
    {
        public int ID { get; set; }

        public int USER_PROFILE_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string SOSMED_NAME { get; set; }

        [Required]
        [StringLength(100)]
        public string SOSMED_PATH { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [Required]
        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        public virtual TOURIS_TM_USER_PROFILE TOURIS_TM_USER_PROFILE { get; set; }
    }
}
