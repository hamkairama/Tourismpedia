namespace Persada.Fr.ModelView
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TOURIS_TV_USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TOURIS_TV_USER()
        {
            TOURIS_TV_USER_PROFILE = new HashSet<TOURIS_TV_USER_PROFILE>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string USER_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string USER_NAME { get; set; }

        public bool IS_SUPER_ADMIN { get; set; }

        public DateTime? LAST_LOGIN { get; set; }
        

        [Required]
        [EmailAddress]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string USER_MAIL { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "PASSWORD")]
        public string PASSWORD { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "CONFIRM PASSWORD")]
        [Compare("PASSWORD", ErrorMessage = "The password and confirmation password do not match.")]
        public string CONFIRM_PASSWORD { get; set; }        
        public DateTime CREATED_TIME { get; set; }

        [StringLength(50)]
        public string CREATED_BY { get; set; }

        public DateTime? LAST_MODIFIED_TIME { get; set; }

        [StringLength(50)]
        public string LAST_MODIFIED_BY { get; set; }

        public int ROW_STATUS { get; set; }

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


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOURIS_TV_USER_PROFILE> TOURIS_TV_USER_PROFILE { get; set; }
    }
}
