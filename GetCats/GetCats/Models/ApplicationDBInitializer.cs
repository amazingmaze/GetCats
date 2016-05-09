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
                UserName = "AdminGetCats@AdminGetCats.com",
                Street = "Baba 12", 
                Region = "Skåne", 
                PostalCode = "12345", 
                Country = sweden,
                Email = "AdminGetCats@AdminGetCats.com",
                Status = User.UserStatus.Active, 
                PasswordHash = "ANgwBdpQon5+Jp4d2/6HiWutOoDwEVDo3OghvU3cH0lsPUQNUUyqRAP70sb/zZRMgA==",
                //Password is Abc@123
                SecurityStamp = "a4a45109-d97f-4f74-9f71-1ff60fd90dca",
                Roles = {new IdentityUserRole {RoleId = admin.Id, UserId = user.Id}},
            };

            //DUMMY IMAGES, THESE WONT WORK TO VIEW!!!
            var img1 = new Image { FileName = "baba1.jpg", Name = "Some baba cat!", Options = new PurchaseOption[]
            {
                new PurchaseOption { Resolution = PurchaseOption.ImageResolution.High, Price = 11, Id = Guid.Parse("7305926d-15f5-4adc-820d-ab9443acf963")},
                new PurchaseOption { Resolution = PurchaseOption.ImageResolution.Low, Price = 6, Id = Guid.Parse("e035e7a4-56a9-48e1-9b4f-157f21dbbe51") }
            }};
            var img2 = new Image { FileName = "baba1.jpg", Name = "1337 cat", Options = new PurchaseOption[] {
                new PurchaseOption { Resolution = PurchaseOption.ImageResolution.High, Price = 11, Id = Guid.Parse("5afff234-e76c-477f-8263-810020a1ef6d") },
                new PurchaseOption { Resolution = PurchaseOption.ImageResolution.Low, Price = 6, Id = Guid.Parse("899012aa-2b03-4eb8-b283-4f897aa9f1a0") }
            }};

            dbContext.Images.AddRange(new List<Image> {img1, img2});

            dbContext.Roles.Add(admin); //create admin role
            dbContext.Roles.Add(user); //Create user role
            dbContext.Users.Add(newAdmin);
            dbContext.Countries.Add(sweden);
            dbContext.Countries.Add(denmark);
            dbContext.Countries.Add(finland);
            base.Seed(dbContext);
        }
    }
}