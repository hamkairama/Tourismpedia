namespace Persada.Fr.ModelView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TOURIS_TV_ROLE_MENU
    {
        public int ID { get; set; }

        public int ROLE_ID { get; set; }

        public int MENU_ID { get; set; }

        public int IS_ACCESS { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        public virtual TOURIS_TV_MENU TOURIS_TV_MENU { get; set; }

        public virtual TOURIS_TV_ROLE TOURIS_TV_ROLE { get; set; }
    }
}
