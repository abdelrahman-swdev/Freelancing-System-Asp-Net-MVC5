using FreelancingSystemProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreelancingSystemProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole role)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",role);
            }

            db.Roles.Add(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
                return HttpNotFound();


            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, IdentityRole role)
        {
            if (id == null || id != role.Id)
                return HttpNotFound();

            var roleInDb = db.Roles.Find(id);

            roleInDb.Name = role.Name;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
                return HttpNotFound();


            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (id == null)
                return HttpNotFound();

            var role = db.Roles.Find(id);

            if (role == null)
                return HttpNotFound();

            db.Roles.Remove(role);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
