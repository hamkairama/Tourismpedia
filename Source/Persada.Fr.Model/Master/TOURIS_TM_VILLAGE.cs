namespace Persada.Fr.Model.Master
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TOURIS_TM_VILLAGE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOURIS_TM_VILLAGE()
        {
            TOURIS_TM_SUB_CATEGORY = new HashSet<TOURIS_TM_SUB_CATEGORY>();
        }

        public int ID { get; set; }

        public int DISTRICT_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string VILLAGE_NAME { get; set; }

        [Required]
        [StringLength(100)]
        public string VILLAGE_CODE { get; set; }

        [Required]
        [StringLength(500)]
        public string VILLAGE_DESCRIPTION { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [Required]
        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        public virtual TOURIS_TM_DISTRICT TOURIS_TM_DISTRICT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOURIS_TM_SUB_CATEGORY> TOURIS_TM_SUB_CATEGORY { get; set; }
    }
}
