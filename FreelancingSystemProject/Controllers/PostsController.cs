using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FreelancingSystemProject.Models;
using Microsoft.AspNet.Identity;

namespace FreelancingSystemProject.Controllers
{
    [Authorize(Roles = "Admin,Client")]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        [Authorize(Roles = "Admin")]
        public ActionResult GetAllPostsAccepted()
        {
            IQueryable<Posts> posts = db.Posts.Include(p => p.JobType).Where(p=>p.IsApprovedByAdmin);
            return View(posts.ToList());
        }

        public ActionResult ExportPosts()
        {
            List<Posts> allPosts = new List<Posts>();
            allPosts = db.Posts.ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/CrystalReports"), "PostsReport.rpt"));

            rd.SetDataSource(allPosts);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {

                Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "PostsList.pdf");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        // GET: Posts
        [Authorize(Roles ="Admin")]
        public ActionResult GetAllPendingPosts()
        {
            var posts = db.Posts.Include(p => p.JobType).Where(p => !p.IsApprovedByAdmin);
            return View(posts.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AcceptPost(int id)
        {
            var post = db.Posts.Include(p => p.JobType).FirstOrDefault(p => p.Id ==id);
            if (post == null)
                return HttpNotFound();

            post.IsApprovedByAdmin = true;
            db.SaveChanges();
            return RedirectToAction("GetAllPostsAccepted");
        }

        // GET: Posts
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("GetAllPostsAccepted");
            }
            var userId = User.Identity.GetUserId();
            var posts = db.Posts.Include(p => p.JobType).Where(p=>p.UserId == userId && p.IsApprovedByAdmin);
            return View(posts.ToList());
            
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.JobTypeId = new SelectList(db.JobTypes, "Id", "Type");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Posts posts)
        {
            if (ModelState.IsValid)
            {
                posts.UserId = User.Identity.GetUserId();
                posts.IsApprovedByAdmin = false;
                db.Posts.Add(posts);
                db.SaveChanges();
                ViewBag.MessageAboutPost = "Your Post is created, the admin will review it and accept if of refuse";
                ViewBag.JobTypeId = new SelectList(db.JobTypes, "Id", "Type", posts.JobTypeId);
                return View(posts);
            }
            else
            {
                ViewBag.JobTypeId = new SelectList(db.JobTypes, "Id", "Type", posts.JobTypeId);
                return View(posts);

            }

        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Client,Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobTypeId = new SelectList(db.JobTypes, "Id", "Type", posts.JobTypeId);
            return View(posts);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Posts posts)
        {
            if (ModelState.IsValid)
            {
                var postInDb = db.Posts.Find(posts.Id);
                if(postInDb == null)
                {
                    return HttpNotFound();
                }
                postInDb.JobBudget = posts.JobBudget;
                postInDb.JobDescription = posts.JobDescription;
                postInDb.JobTypeId = posts.JobTypeId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobTypeId = new SelectList(db.JobTypes, "Id", "Type", posts.JobTypeId);
            return View(posts);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return HttpNotFound();
            }
            return View(posts);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Posts posts = db.Posts.Find(id);
            db.Posts.Remove(posts);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
