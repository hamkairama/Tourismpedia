namespace Persada.Fr.Model.Master
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TOURIS_TM_ROLE_MENU
    {
        public int ID { get; set; }

        public int ROLE_ID { get; set; }

        public int MENU_ID { get; set; }

        public int IS_ACCESS { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [Required]
        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        public virtual TOURIS_TM_MENU TOURIS_TM_MENU { get; set; }

        public virtual TOURIS_TM_ROLE TOURIS_TM_ROLE { get; set; }
    }
}
