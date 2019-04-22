namespace Persada.Fr.Model.Master
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TOURIS_TM_ROLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOURIS_TM_ROLE()
        {
            TOURIS_TM_ROLE_MENU = new HashSet<TOURIS_TM_ROLE_MENU>();
            TOURIS_TM_USER_DT = new HashSet<TOURIS_TM_USER_DT>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ROLE_NAME { get; set; }

        [Required]
        [StringLength(100)]
        public string ROLE_DESCRIPTION { get; set; }

        public int IS_ACTIVE { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [Required]
        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        public int? DEFAULT_SELECTED { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOURIS_TM_ROLE_MENU> TOURIS_TM_ROLE_MENU { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOURIS_TM_USER_DT> TOURIS_TM_USER_DT { get; set; }
    }
}
