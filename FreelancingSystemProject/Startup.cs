using FreelancingSystemProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FreelancingSystemProject.Startup))]
namespace FreelancingSystemProject
{
    public partial class Startup
    {
        private ApplicationDbContext db;
        public Startup()
        {
            db = new ApplicationDbContext();
        }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }

        public void CreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));


            IdentityRole role = new IdentityRole();

            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);

                ApplicationUser user = new ApplicationUser();

                user.FirstName = "Abdelrahman";
                user.LastName = "Gamal";
                user.PhoneNumber = "01127982490";
                user.imageUrl = "2018-04-04_21.43.23.jpg";
                user.UserName = "Admin@admin.com";
                user.Email = "Admin@admin.com";
                user.UserType = "Admin";
                

                var check = userManager.Create(user,"Admin@123");

                if (check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
                else
                {
                    throw new System.Exception();
                }
            }
        }
    }
}
