namespace Persada.Fr.ModelView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class TOURIS_TV_COUNTRY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOURIS_TV_COUNTRY()
        {
            TOURIS_TV_PROVINCE = new HashSet<TOURIS_TV_PROVINCE>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string COUNTRY_NAME { get; set; }

        [Required]
        [StringLength(100)]
        public string COUNTRY_CODE { get; set; }

        [Required]
        [StringLength(500)]
        public string COUNTRY_DESCRIPTION { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOURIS_TV_PROVINCE> TOURIS_TV_PROVINCE { get; set; }
    }
}
