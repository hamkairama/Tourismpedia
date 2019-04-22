namespace Persada.Fr.Model.Master
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TOURIS_TM_USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOURIS_TM_USER()
        {
            TOURIS_TM_USER_PROFILE = new HashSet<TOURIS_TM_USER_PROFILE>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string USER_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string USER_MAIL { get; set; }

        [Required]
        [StringLength(50)]
        public string USER_NAME { get; set; }

        public bool IS_SUPER_ADMIN { get; set; }

        public DateTime? LAST_LOGIN { get; set; }

        [StringLength(50)]
        public string PASSWORD { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [Required]
        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOURIS_TM_USER_PROFILE> TOURIS_TM_USER_PROFILE { get; set; }
    }
}
