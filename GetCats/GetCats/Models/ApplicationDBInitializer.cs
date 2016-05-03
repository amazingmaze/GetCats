using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GetCats.Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GetCats.Models
{
    /**
    Andreas Svensson 
    */
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext dbContext)
        {
            //creating two new identityRoles. 
            var admin = new IdentityRole { Name = "Admin", Id = Guid.NewGuid().ToString() }; 
            var user = new IdentityRole { Name = "User", Id = Guid.NewGuid().ToString() };
            var newAdmin = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "AdminGetCats@AdminGetCats.com",
                Email = "AdminGetCats@AdminGetCats.com",
                PasswordHash = "ANgwBdpQon5+Jp4d2/6HiWutOoDwEVDo3OghvU3cH0lsPUQNUUyqRAP70sb/zZRMgA==",
                //Password is Abc@123
                SecurityStamp = "a4a45109-d97f-4f74-9f71-1ff60fd90dca",
                Roles = {new IdentityUserRole {RoleId = admin.Id, UserId = user.Id}},
            };
            dbContext.Roles.Add(admin); //create admin role
            dbContext.Roles.Add(user); //Create user role
            dbContext.Users.Add(newAdmin); 
            base.Seed(dbContext);
        }
    }
}