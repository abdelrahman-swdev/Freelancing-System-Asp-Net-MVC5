using FreelancingSystemProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancingSystemProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;

        public AdminController()
        {
            db = new ApplicationDbContext();
        }


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        /*******************************get all Users***************************/
        
        public ActionResult GetAllUsers()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userId = User.Identity.GetUserId();
            return View(userManager.Users.Where(u => u.Id != userId));
        }




        /*******************************Delete User***************************/
        public ActionResult DeleteUser(string id)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return HttpNotFound();

            return View(user);
        }


        [HttpPost]
        [ActionName("DeleteUser")]
        public ActionResult DeleteUserConfirmed(string id)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindById(id);

            if (user == null)
                return HttpNotFound();

            var roleForUser = userManager.GetRoles(id).First();

            if (roleForUser == null)
                return HttpNotFound();

            if(roleForUser == MagicStrings.FreelancerRole)
            {
                
                var proposalsForFreelancer = db.SendProposals.Where(s => s.User.Id == id).ToList();
                if(proposalsForFreelancer != null)
                {

                    foreach(var pro in proposalsForFreelancer)
                    {
                        db.SendProposals.Remove(pro);
                    
                        db.SaveChanges();
                    }
                }

                var SavedJobsForFreelancer = db.SavedJobs.Where(s => s.User.Id == id).ToList();
                if(SavedJobsForFreelancer != null)
                {

                    foreach (var job in SavedJobsForFreelancer)
                    {
                        db.SavedJobs.Remove(job);

                        db.SaveChanges();
                    }
                }
            }
            else if(roleForUser == MagicStrings.ClientRole)
            {
                var postsByClient = db.Posts.Where(p => p.User.Id == id).ToList();
                foreach (var post in postsByClient)
                {
                    db.Posts.Remove(post);
                    
                    db.SaveChanges();
                }
            }
            

            var check = userManager.Delete(user);
            if (check.Succeeded)
            {
                return RedirectToAction("GetAllUsers");
            }
            
            
            return HttpNotFound();
        }
    }
}