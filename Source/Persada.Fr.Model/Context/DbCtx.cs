namespace Persada.Fr.Model.Master
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbCtx : DbContext
    {
        public DbCtx() : base("name=DbConextion") { }

        public DbSet<RegisterUser> RegisterUser { get; set; }
        public DbSet<ClientKey> ClientKeys { get; set; }
        public DbSet<TokensManager> TokensManager { get; set; }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TOURIS_TM_CATEGORY> TOURIS_TM_CATEGORY { get; set; }
        public virtual DbSet<TOURIS_TM_CITY> TOURIS_TM_CITY { get; set; }
        public virtual DbSet<TOURIS_TM_COUNTRY> TOURIS_TM_COUNTRY { get; set; }
        public virtual DbSet<TOURIS_TM_DISTRICT> TOURIS_TM_DISTRICT { get; set; }
        public virtual DbSet<TOURIS_TM_MENU> TOURIS_TM_MENU { get; set; }
        public virtual DbSet<TOURIS_TM_PARAMETER> TOURIS_TM_PARAMETER { get; set; }
        public virtual DbSet<TOURIS_TM_PROVINCE> TOURIS_TM_PROVINCE { get; set; }
        public virtual DbSet<TOURIS_TM_ROLE> TOURIS_TM_ROLE { get; set; }
        public virtual DbSet<TOURIS_TM_ROLE_MENU> TOURIS_TM_ROLE_MENU { get; set; }
        public virtual DbSet<TOURIS_TM_SLIDESHOW> TOURIS_TM_SLIDESHOW { get; set; }
        public virtual DbSet<TOURIS_TM_SUB_CATEGORY> TOURIS_TM_SUB_CATEGORY { get; set; }
        public virtual DbSet<TOURIS_TM_SUB_CATEGORY_PHOTO> TOURIS_TM_SUB_CATEGORY_PHOTO { get; set; }
        public virtual DbSet<TOURIS_TM_TESTIMONIAL> TOURIS_TM_TESTIMONIAL { get; set; }
        public virtual DbSet<TOURIS_TM_USER> TOURIS_TM_USER { get; set; }
        public virtual DbSet<TOURIS_TM_USER_DT> TOURIS_TM_USER_DT { get; set; }
        public virtual DbSet<TOURIS_TM_USER_PROFILE> TOURIS_TM_USER_PROFILE { get; set; }
        public virtual DbSet<TOURIS_TM_USER_PROFILE_SOSMED> TOURIS_TM_USER_PROFILE_SOSMED { get; set; }
        public virtual DbSet<TOURIS_TM_VILLAGE> TOURIS_TM_VILLAGE { get; set; }
        public virtual DbSet<TOURIS_TM_SOSMED> TOURIS_TM_SOSMED { get; set; }
        public virtual DbSet<TOURIS_TM_CONTACT_US> TOURIS_TM_CONTACT_US { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TOURIS_TM_CATEGORY>()
                .Property(e => e.CATEGORY_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CATEGORY>()
                .Property(e => e.CATEGORY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CATEGORY>()
                .Property(e => e.CATEGORY_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CATEGORY>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CATEGORY>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CATEGORY>()
                .HasMany(e => e.TOURIS_TM_SUB_CATEGORY)
                .WithRequired(e => e.TOURIS_TM_CATEGORY)
                .HasForeignKey(e => e.CATEGORY_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_CITY>()
                .Property(e => e.CITY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CITY>()
                .Property(e => e.CITY_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CITY>()
                .Property(e => e.CITY_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CITY>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CITY>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CITY>()
                .HasMany(e => e.TOURIS_TM_DISTRICT)
                .WithRequired(e => e.TOURIS_TM_CITY)
                .HasForeignKey(e => e.CITY_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_COUNTRY>()
                .Property(e => e.COUNTRY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_COUNTRY>()
                .Property(e => e.COUNTRY_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_COUNTRY>()
                .Property(e => e.COUNTRY_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_COUNTRY>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_COUNTRY>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_COUNTRY>()
                .HasMany(e => e.TOURIS_TM_PROVINCE)
                .WithRequired(e => e.TOURIS_TM_COUNTRY)
                .HasForeignKey(e => e.COUNTRY_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_DISTRICT>()
                .Property(e => e.DISTRICT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_DISTRICT>()
                .Property(e => e.DISTRICT_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_DISTRICT>()
                .Property(e => e.DISTRICT_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_DISTRICT>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_DISTRICT>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_DISTRICT>()
                .HasMany(e => e.TOURIS_TM_VILLAGE)
                .WithRequired(e => e.TOURIS_TM_DISTRICT)
                .HasForeignKey(e => e.DISTRICT_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_MENU>()
                .Property(e => e.MENU_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_MENU>()
                .Property(e => e.MENU_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_MENU>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_MENU>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_MENU>()
                .HasMany(e => e.TOURIS_TM_ROLE_MENU)
                .WithRequired(e => e.TOURIS_TM_MENU)
                .HasForeignKey(e => e.MENU_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_PARAMETER>()
                .Property(e => e.PARAMETER_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_PARAMETER>()
                .Property(e => e.PARAMETER_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_PARAMETER>()
                .Property(e => e.PARAMETER_VALUE)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_PARAMETER>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_PARAMETER>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_PROVINCE>()
                .Property(e => e.PROVINCE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_PROVINCE>()
                .Property(e => e.PROVINCE_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_PROVINCE>()
                .Property(e => e.PROVINCE_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_PROVINCE>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_PROVINCE>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_PROVINCE>()
                .HasMany(e => e.TOURIS_TM_CITY)
                .WithRequired(e => e.TOURIS_TM_PROVINCE)
                .HasForeignKey(e => e.PROVINCE_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_ROLE>()
                .Property(e => e.ROLE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_ROLE>()
                .Property(e => e.ROLE_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_ROLE>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_ROLE>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_ROLE>()
                .HasMany(e => e.TOURIS_TM_ROLE_MENU)
                .WithRequired(e => e.TOURIS_TM_ROLE)
                .HasForeignKey(e => e.ROLE_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_ROLE>()
                .HasMany(e => e.TOURIS_TM_USER_DT)
                .WithRequired(e => e.TOURIS_TM_ROLE)
                .HasForeignKey(e => e.ROLE_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_ROLE_MENU>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_ROLE_MENU>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SLIDESHOW>()
                .Property(e => e.TITTLE)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SLIDESHOW>()
                .Property(e => e.CONTENT_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SLIDESHOW>()
                .Property(e => e.CLASS)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SLIDESHOW>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SLIDESHOW>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SUB_CATEGORY>()
                .Property(e => e.SUB_CATEGORY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SUB_CATEGORY>()
                .Property(e => e.SUB_CATEGORY_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SUB_CATEGORY>()
                .Property(e => e.ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SUB_CATEGORY>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SUB_CATEGORY>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SUB_CATEGORY>()
                .HasMany(e => e.TOURIS_TM_SUB_CATEGORY_PHOTO)
                .WithRequired(e => e.TOURIS_TM_SUB_CATEGORY)
                .HasForeignKey(e => e.SUB_CATEGORY_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_SUB_CATEGORY_PHOTO>()
                .Property(e => e.PHOTO_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SUB_CATEGORY_PHOTO>()
                .Property(e => e.PHOTO_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SUB_CATEGORY_PHOTO>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SUB_CATEGORY_PHOTO>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_TESTIMONIAL>()
                .Property(e => e.TESTIMONI_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_TESTIMONIAL>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_TESTIMONIAL>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER>()
                .Property(e => e.USER_ID)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER>()
                .Property(e => e.USER_MAIL)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER>()
                .Property(e => e.USER_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER>()
                .HasMany(e => e.TOURIS_TM_USER_PROFILE)
                .WithRequired(e => e.TOURIS_TM_USER)
                .HasForeignKey(e => e.USER_ID_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_USER_DT>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_DT>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE>()
                .Property(e => e.GENDER)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE>()
                .Property(e => e.ADDRESS)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE>()
                .Property(e => e.JOB)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE>()
                .Property(e => e.COMPANY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE>()
                .Property(e => e.HOBBY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE>()
                .HasMany(e => e.TOURIS_TM_USER_PROFILE_SOSMED)
                .WithRequired(e => e.TOURIS_TM_USER_PROFILE)
                .HasForeignKey(e => e.USER_PROFILE_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE_SOSMED>()
                .Property(e => e.SOSMED_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE_SOSMED>()
                .Property(e => e.SOSMED_PATH)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE_SOSMED>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_USER_PROFILE_SOSMED>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_VILLAGE>()
                .Property(e => e.VILLAGE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_VILLAGE>()
                .Property(e => e.VILLAGE_CODE)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_VILLAGE>()
                .Property(e => e.VILLAGE_DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_VILLAGE>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_VILLAGE>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_VILLAGE>()
                .HasMany(e => e.TOURIS_TM_SUB_CATEGORY)
                .WithRequired(e => e.TOURIS_TM_VILLAGE)
                .HasForeignKey(e => e.VILLAGE_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TOURIS_TM_SOSMED>()
                .Property(e => e.TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SOSMED>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SOSMED>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SOSMED>()
                .Property(e => e.ICON)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SOSMED>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SOSMED>()
                .Property(e => e.DATA_EMBED)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SOSMED>()
                .Property(e => e.DATA_WIDGET_ID)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SOSMED>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_SOSMED>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CONTACT_US>()
                .Property(e => e.NAME_SENDER)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CONTACT_US>()
                .Property(e => e.EMAIL_SENDER)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CONTACT_US>()
                .Property(e => e.PHONE_SENDER)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CONTACT_US>()
                .Property(e => e.DESCRIPTION)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CONTACT_US>()
                .Property(e => e.CREATED_BY)
                .IsUnicode(false);

            modelBuilder.Entity<TOURIS_TM_CONTACT_US>()
                .Property(e => e.LAST_MODIFIED_BY)
                .IsUnicode(false);
        }
    }
}
