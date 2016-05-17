using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Text;
using GetCats.Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GetCats.Models
{
    /**
    Andreas Svensson 
        */
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<PurchaseOption> PurchaseOptions { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            if(modelBuilder == null) { throw new ArgumentException("ModelBuilder is null"); }
            base.OnModelCreating(modelBuilder);

            //renaming the database tables.... 
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public static void Initialize()
        {
            Create().Database.Initialize(false);
        }

        public override int SaveChanges() //Taken from http://stackoverflow.com/questions/10219864/ef-code-first-how-do-i-see-entityvalidationerrors-property-from-the-nuget-pac
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
        }
    }
}