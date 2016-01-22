using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Identity.Models;

namespace Identity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
                
        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var adminRole = new IdentityRole { Name = "Admin" };
                var managerRole = new IdentityRole { Name = "Manager" };
                var teacherRole = new IdentityRole { Name = "Teacher" };
                var parentRole = new IdentityRole { Name = "Parent" };
                var studentRole = new IdentityRole { Name = "Student" };
                context.Entry(adminRole);
                context.SaveChanges();
                manager.Create(adminRole);
                manager.Create(managerRole);
                manager.Create(teacherRole);
                manager.Create(parentRole);
                manager.Create(studentRole);

            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);
                var admin = new ApplicationUser { UserName = "admin" };
                var manager = new ApplicationUser { UserName = "manager" };
                var teacher = new ApplicationUser { UserName = "teacher" };
                var parent = new ApplicationUser { UserName = "parent" };
                var student = new ApplicationUser { UserName = "student" };

                userManager.Create(admin, "123456");
                userManager.AddToRole(admin.Id, "Admin");

                userManager.Create(manager, "123456");
                userManager.AddToRole(manager.Id, "Manager");

                userManager.Create(teacher, "123456");
                userManager.AddToRole(teacher.Id, "Teacher");

                userManager.Create(parent, "123456");
                userManager.AddToRole(parent.Id, "Parent");

                userManager.Create(student, "123456");
                userManager.AddToRole(student.Id, "Student");

            }
            
            context.SaveChanges();
            base.Seed(context);
        }
  
    }
}