namespace Persada.Fr.ModelView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TOURIS_TV_SLIDESHOW
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string TITTLE { get; set; }

        public string CONTENT_DESCRIPTION { get; set; }

        [Required]
        [StringLength(100)]
        public string CLASS { get; set; }
        
        [StringLength(100)]
        public string PHOTO_PATH { get; set; }

        public string URL { get; set; }
        public DateTime CREATED_TIME { get; set; }

        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }
    }
}
