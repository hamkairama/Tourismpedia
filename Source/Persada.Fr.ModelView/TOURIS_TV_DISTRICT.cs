namespace Persada.Fr.ModelView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TOURIS_TV_DISTRICT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOURIS_TV_DISTRICT()
        {
            TOURIS_TV_VILLAGE = new HashSet<TOURIS_TV_VILLAGE>();
        }

        public int ID { get; set; }

        public int COUNTRY_ID { get; set; }
        public string COUNTRY_NAME { get; set; }

        public int PROVINCE_ID { get; set; }
        public string PROVINCE_NAME { get; set; }

        public int CITY_ID { get; set; }
        public string CITY_NAME { get; set; }

        [Required]
        [StringLength(100)]
        public string DISTRICT_NAME { get; set; }

        [Required]
        [StringLength(100)]
        public string DISTRICT_CODE { get; set; }

        [Required]
        [StringLength(500)]
        public string DISTRICT_DESCRIPTION { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        public virtual TOURIS_TV_CITY TOURIS_TV_CITY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOURIS_TV_VILLAGE> TOURIS_TV_VILLAGE { get; set; }
    }
}
