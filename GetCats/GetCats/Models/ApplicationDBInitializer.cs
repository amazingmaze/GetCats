using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GetCats.Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GetCats.Models
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext dbContext)
        {
            //creating two new identityRoles. 
            var admin = new IdentityRole { Name = User.PermissionStatus.Admin.ToString(), Id = Guid.NewGuid().ToString() }; 
            var user = new IdentityRole { Name = User.PermissionStatus.User.ToString(), Id = Guid.NewGuid().ToString() };
            var sweden = new Country()
            {
                Id = Guid.NewGuid(),
                Name = "Sweden"
            };

            var denmark = new Country()
            {
                Id = Guid.NewGuid(),
                Name = "Denmark"
            };

            var finland = new Country
            {
                Id = Guid.NewGuid(),
                Name = "Finland"
            };

            var newAdmin = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin@getcats.com",
                Street = "Baba 12", 
                Region = "Skåne", 
                PostalCode = "12345", 
                Country = sweden,
                Email = "Admin@getcats.com",
                LockoutEnabled = true,
                PasswordHash = "ANgwBdpQon5+Jp4d2/6HiWutOoDwEVDo3OghvU3cH0lsPUQNUUyqRAP70sb/zZRMgA==",
                //Password is Abc@123
                SecurityStamp = "a4a45109-d97f-4f74-9f71-1ff60fd90dca",
                Roles = {new IdentityUserRole {RoleId = admin.Id, UserId = user.Id}},
                
            };

            var newUser1 = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "User@getcats.com",
                Street = "ewoighweo",
                Region = "gwepgjweg",
                PostalCode = "gwpgjweg",
                Country = denmark,
                Email = "User@getcats.com",
                LockoutEnabled = true,
                PasswordHash = "ANgwBdpQon5+Jp4d2/6HiWutOoDwEVDo3OghvU3cH0lsPUQNUUyqRAP70sb/zZRMgA==",
                //Password is Abc@123
                SecurityStamp = "a4a45109-d97f-4f74-9f71-1ff60fd90dca",
                Roles = { new IdentityUserRole { RoleId = user.Id, UserId = user.Id } },
            };

            dbContext.Roles.Add(admin); //create admin role
            dbContext.Roles.Add(user); //Create user role
            dbContext.Users.Add(newAdmin);
            dbContext.Users.Add(newUser1);
            dbContext.Countries.Add(sweden);
            dbContext.Countries.Add(denmark);
            dbContext.Countries.Add(finland);
            base.Seed(dbContext);
        }
    }
}