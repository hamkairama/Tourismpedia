namespace Persada.Fr.ModelView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TOURIS_TV_SUB_CATEGORY_PHOTO
    {
        public int ID { get; set; }

        public int SUB_CATEGORY_ID { get; set; }

        public String SUB_CATEGORY_NAME { get; set; }

        [Required]
        [StringLength(500)]
        public string PHOTO_NAME { get; set; }

        [Required]
        public string PHOTO_DESCRIPTION { get; set; }
        
        [StringLength(100)]
        public string PHOTO_PATH { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        public virtual TOURIS_TV_SUB_CATEGORY TOURIS_TV_SUB_CATEGORY { get; set; }
    }
}
