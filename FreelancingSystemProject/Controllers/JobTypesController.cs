using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FreelancingSystemProject.Models;


namespace FreelancingSystemProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class JobTypesController : Controller
    {

        public ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobTypes
        public ActionResult Index()
        {
            return View(db.JobTypes.ToList());
        }

        // GET: JobTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type")] JobType jobType)
        {
            if (ModelState.IsValid)
            {
                db.JobTypes.Add(jobType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobType);
        }

        // GET: JobTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobType jobType = db.JobTypes.Find(id);
            if (jobType == null)
            {
                return HttpNotFound();
            }
            return View(jobType);
        }

        // POST: JobTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type")] JobType jobType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobType);
        }

        // GET: JobTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobType jobType = db.JobTypes.Find(id);
            if (jobType == null)
            {
                return HttpNotFound();
            }
            return View(jobType);
        }

        // POST: JobTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobType jobType = db.JobTypes.Find(id);
            db.JobTypes.Remove(jobType);
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
