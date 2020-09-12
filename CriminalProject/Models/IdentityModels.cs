using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CriminalProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        
        public ICollection<CrimeHistory> SolvedCrimes { get; set; }

        public ApplicationUser()
        {
            SolvedCrimes = new HashSet<CrimeHistory>();
           
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CrimeHistory>().HasRequired(c => c.CrimeType).WithMany(c => c.Crimes)
                .HasForeignKey(c => c.FkCrimeTypeId);
            modelBuilder.Entity<CrimeHistory>().HasRequired(c => c.Weapon).WithMany(w => w.Crimes)
                .HasForeignKey(c => c.FkWeaponId);
            modelBuilder.Entity<CrimeHistory>().HasRequired(c => c.City).WithMany(c => c.Crimes)
                .HasForeignKey(c => c.FkCityId);
            modelBuilder.Entity<CrimeHistory>().HasMany(c => c.Suspects).WithMany(s => s.CommittedCrimes).Map(m =>
                m.ToTable("SuspectCrime").MapLeftKey("FkSuspectId").MapRightKey("FkCommittedCrimeId"));
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.SolvedCrimes).WithRequired(c => c.Officer)
                .HasForeignKey(c => c.FkOfficerId);
            modelBuilder.Entity<Suspect>().HasRequired(s => s.SuspectPicture).WithRequiredPrincipal(c => c.Suspect);


        }

        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Weapon> Weapons{ get; set; }
        public virtual DbSet<Suspect> Suspects{ get; set; }
        public virtual DbSet<CrimeType> CrimeTypes { get; set; }
        public virtual DbSet<CrimeHistory> CrimeHistories { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<City> Cities { get; set; }
    }
}