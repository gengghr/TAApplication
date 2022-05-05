using Microsoft.AspNetCore.Identity;
using PS4.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using PS4.Areas.Identity.Data;

namespace PS4.Data
{
    public class SeedUsersRolesDB
    {
        public static async Task InitializeAsync(TAUsersRolesDB db, UserManager<TAUser> um, RoleManager<IdentityRole> rm)
        {
            IdentityResult result;
            db.Database.EnsureCreated();
            if (!rm.RoleExistsAsync("Administrator").Result)
                result = rm.CreateAsync(new IdentityRole("Administrator")).Result;
            if (!rm.RoleExistsAsync("Professor").Result)
                result = rm.CreateAsync(new IdentityRole("Professor")).Result;
            if (!rm.RoleExistsAsync("Applicant").Result)
                result = rm.CreateAsync(new IdentityRole("Applicant")).Result;
            db.SaveChanges();

            if (um.Users.Any())
                return;

            //var users = new TAUser[5];

            //TAUser 1 Administrator
            TAUser u1 = new TAUser();
            u1.Email = "admin@utah.edu";
            u1.UserName = "admin@utah.edu";
            u1.Name = "admin";
            u1.uID = "u9999999";
            u1.EmailConfirmed = true;
            result = um.CreateAsync(u1, "123ABC!@#def").Result;
            if (result.Succeeded)
            {
                um.AddToRoleAsync(u1, "Administrator").Wait();
            }

            db.SaveChanges();

            //TAUser 2 Professor
            TAUser u2 = new TAUser();
            u2.Email = "professor@utah.edu";
            u2.UserName = "professor@utah.edu";
            u2.Name = "professor";
            u2.uID = "u8888888";
            u2.EmailConfirmed = true;
            result = um.CreateAsync(u2, "123ABC!@#def").Result;
            if (result.Succeeded)
            {
                um.AddToRoleAsync(u2, "Professor").Wait();
            }
            db.SaveChanges();
            //TAUser 3 Applicant 
            TAUser u3 = new TAUser();
            u3.Email = "u0000000@utah.edu";
            u3.Name = "u0000000";
            u3.uID = "u0000000";
            u3.UserName = "u0000000@utah.edu";
            u3.EmailConfirmed = true;
            result = um.CreateAsync(u3, "123ABC!@#def").Result;
            if (result.Succeeded)
            {
                um.AddToRoleAsync(u3, "Applicant").Wait();
            }
            db.SaveChanges();
            //TAUser 4 Applicant 
            TAUser u4 = new TAUser();
            u4.Email = "u0000001@utah.edu";
            u4.Name = "u0000001";
            u4.uID = "u0000001";
            u4.UserName = "u0000001@utah.edu";
            u4.EmailConfirmed = true;
            result = um.CreateAsync(u4, "123ABC!@#def").Result;
            if (result.Succeeded)
            {
                um.AddToRoleAsync(u4, "Applicant").Wait();
            }
            db.SaveChanges();
            //TAUser 5 Applicant 
            TAUser u5 = new TAUser();
            u5.Email = "u0000002@utah.edu";
            u5.Name = "u0000002";
            u5.uID = "u0000002";
            u5.UserName = "u0000002@utah.edu";
            u5.EmailConfirmed = true;
            result = um.CreateAsync(u5, "123ABC!@#def").Result;
            if (result.Succeeded)
            {
                um.AddToRoleAsync(u5, "Applicant").Wait();
            }
            db.SaveChanges();

        }
    }
}
