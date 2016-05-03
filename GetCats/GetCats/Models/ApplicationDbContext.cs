using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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
            //modelBuilder.Entity<Order>().HasMany(o => o.Items).WithRequired().HasForeignKey(oi => oi.OrderId); //configure a relationship between Orders and OrderItems
            //modelBuilder.Entity<OrderItems>().HasKey(i => new {i.Id, i.Quanity, i.OrderId}).Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); //composie key for OrderItems. Also auto-generate by datase. 
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public static void Initialize()
        {
            Create().Database.Initialize(false);
        }

    }
}