using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace FirstTry.Models
{
    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "director" };
            var role3 = new IdentityRole { Name = "manager" };
            var role4 = new IdentityRole { Name = "doctor" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);
            roleManager.Create(role4);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
            string password = "Adminadmin1-";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                userManager.AddToRole(admin.Id, role3.Name);
                userManager.AddToRole(admin.Id, role4.Name);
            }

            var director = new ApplicationUser { Email = "director@gmail.com", UserName = "director@gmail.com" };
            var password2 = "Directordirector1-";
            var result2 = userManager.Create(director, password2);

            // если создание пользователя прошло успешно
            if (result2.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(director.Id, role2.Name);
            }

            var manager = new ApplicationUser { Email = "manager@gmail.com", UserName = "manager@gmail.com" };
            var password3 = "Managermanager1-";
            var result3 = userManager.Create(manager, password3);

            // если создание пользователя прошло успешно
            if (result3.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(manager.Id, role3.Name);
            }

            var doctor = new ApplicationUser { Email = "doctor@gmail.com", UserName = "doctor@gmail.com" };
            var password4 = "Doctordoctor1-";
            var result4 = userManager.Create(doctor, password4);

            // если создание пользователя прошло успешно
            if (result4.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(doctor.Id, role4.Name);
            }

            base.Seed(context);
        }
    }
}