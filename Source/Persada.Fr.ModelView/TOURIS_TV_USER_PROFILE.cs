namespace Persada.Fr.ModelView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TOURIS_TV_USER_PROFILE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOURIS_TV_USER_PROFILE()
        {
            TOURIS_TV_USER_PROFILE_SOSMED = new HashSet<TOURIS_TV_USER_PROFILE_SOSMED>();
        }

        public int ID { get; set; }

        public int USER_ID_ID { get; set; }
        
        public string PHOTO_PATH { get; set; }

        [StringLength(1)]
        public string GENDER { get; set; }

        public DateTime? BORN { get; set; }

        [StringLength(100)]
        public string ADDRESS { get; set; }

        [StringLength(100)]
        public string DESCRIPTION { get; set; }

        [StringLength(100)]
        public string JOB { get; set; }

        [StringLength(100)]
        public string COMPANY { get; set; }

        [StringLength(100)]
        public string HOBBY { get; set; }

        public DateTime CREATED_TIME { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

        public virtual TOURIS_TV_USER TOURIS_TV_USER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOURIS_TV_USER_PROFILE_SOSMED> TOURIS_TV_USER_PROFILE_SOSMED { get; set; }
    }
}
