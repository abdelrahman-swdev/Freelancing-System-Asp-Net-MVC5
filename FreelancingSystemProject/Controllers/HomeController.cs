using FreelancingSystemProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancingSystemProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;


        public HomeController()
        {
            db = new ApplicationDbContext();
        }


        /*******************************index in home page***************************/
        public ActionResult Index()
        {
            return View(db.JobTypes.ToList());
        }

        /*******************************post Details ***************************/
        public ActionResult Details(int id)
        {
            var post = db.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null) {
                return HttpNotFound();
            }
            Session["postId"] = post.Id;
            return View(post);
        }



        /*******************************Apply for job***************************/
        [Authorize(Roles ="Freelancer")]
        public ActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var userId = User.Identity.GetUserId();
            var postId = (int)Session["postId"];

            var check = db.SendProposals.Where(p => p.PostId == postId && p.UserId == userId).ToList();
            if(check.Count == 0)
            {

                var proposal = new SendProposal
                {
                    Message = Message,
                    PostId = postId,
                    UserId = userId,
                    ProposalDate = DateTime.Now
                };

                db.SendProposals.Add(proposal);
                db.SaveChanges();
                ViewBag.Result = "Done, You have applied successfully";
            }
            else
            {
                ViewBag.Result = "Sorry, you have applied for this job before";
            }
            return View();
        }



        /*******************************Save Job By Freelancer***************************/
        public ActionResult SaveJob(int id)
        {
            var job = db.Posts.FirstOrDefault(j => j.Id == id);
            if (job == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();
            var model = new SavedJobs
            {
                PostId = job.Id,
                UserId = userId
            };

            var check = db.SavedJobs.Where(s => s.UserId == model.UserId && s.PostId == model.PostId).ToList();
            if(check.Count == 0)
            {
                db.SavedJobs.Add(model);
                db.SaveChanges();

                return RedirectToAction("GetSavedJobs");
            }
            else
            {
                return RedirectToAction("GetSavedJobs");
            }

        }


        /*******************************remove saved Jobs By Freelancer***************************/
        public ActionResult RemoveFromSaved(int id)
        {
            var post = db.Posts.FirstOrDefault(c => c.Id == id);
            if (post == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();
            var job = db.SavedJobs.FirstOrDefault( j => j.UserId == userId && j.PostId == post.Id);

            if (job == null)
                return HttpNotFound();

            db.SavedJobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("GetSavedJobs");
        }

        /*******************************get saved Jobs By Freelancer***************************/
        public ActionResult GetSavedJobs()
        {
            var userId = User.Identity.GetUserId();
            var jobs = db.SavedJobs.Where(j => j.UserId == userId).ToList();

            if (jobs == null)
                return HttpNotFound();

            return View(jobs);
        }


        /*******************************get jobs freelancer applied to***************************/
        [Authorize(Roles ="Freelancer")]
        public ActionResult GetJobsByUser()
        {
            var userId = User.Identity.GetUserId();
            var jobs = db.SendProposals.Where(p => p.UserId == userId).ToList();

            return View(jobs);
        }


        /*******************************get recieved proposals***************************/
        [Authorize(Roles ="Client")]
        public ActionResult GetJobsByClient()
        {
            var userId = User.Identity.GetUserId();
            var jobs = from app in db.SendProposals
                       join job in db.Posts
                       on app.PostId equals job.Id
                       where job.User.Id == userId
                       select app;

            var grouped = from j in jobs
                          group j by j.Post.JobDescription
                          into gr
                          select new PostViewModel
                          {
                              JobDescription = gr.Key,
                              proposals = gr
                          };

            return View(grouped.ToList());
        }


        /*******************************get all client posts***************************/
        [Authorize(Roles ="Client")]
        public ActionResult GetMyPosts()
        {
            var UserId = User.Identity.GetUserId();
            var posts = db.Posts.Include(p=>p.JobType).Where(p => p.User.Id == UserId && p.IsApprovedByAdmin);
            return View(posts.ToList());
        }


        /*******************************Searching***************************/
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Posts.Where(s => s.JobDescription.Contains(searchName)
            || s.JobBudget.Contains(searchName)
            || s.JobType.Type.Contains(searchName)
            || s.CreationDate.ToString().Contains(searchName)).ToList();

            return View(result);
        }



        /*******************************About page***************************/
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        /*******************************Contact Page***************************/
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}