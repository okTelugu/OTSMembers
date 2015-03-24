using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OTSMembers.Models;

namespace OTSMembers.Controllers
{
    public class OtsMembersController : Controller
    {
        private OtsDb db = new OtsDb();

        public ActionResult Sponsorships(int? memberID)
        {
            var model = db.Sponsorships.Where(s => s.OtsMember_id == memberID).ToList();
            return PartialView(model);
        }

        // GET: OtsMembers
        [Authorize]
        public ActionResult Index(string searchEmail = null, string firstName =null, string lastName =null)
        {

            if (User.Identity.Name != null)
            {
                var model = db.OTSMembers
                .Where(r => r.Email.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));
                return View(model);
            }
            else if (lastName != null)
            {
                var model = db.OTSMembers
                .Where(r => r.LastName.Contains(lastName));
                return View(model);
            }
            else { 
                var model = db.OTSMembers
                .Where(r => r.Email.Equals(searchEmail, StringComparison.InvariantCultureIgnoreCase));
                return View(model);
            }
        }
        public ActionResult Directory(string searchEmail = null, string firstName = null, string lastName = null)
        {
            if (searchEmail != null)
            {
                var model = db.OTSMembers
                .Where(r => r.Email.Equals(searchEmail, StringComparison.InvariantCultureIgnoreCase));
                return View(model);
            }
            else if (lastName != null)
            {
                var model = db.OTSMembers
                .Where(r => r.LastName.Contains(lastName));
                return View(model);
            }
            else
            {
                var model = db.OTSMembers
                .Where(r => r.Email.Equals(searchEmail, StringComparison.InvariantCultureIgnoreCase));
                return View(model);
            }
        }
        public ActionResult SearchByEmail(string searchEmail = null)
        {
            var model = db.OTSMembers
                .Where(r => r.Email.Equals(searchEmail,StringComparison.InvariantCultureIgnoreCase));
            return PartialView(model);
        }

        // GET: OtsMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtsMember otsMember = db.OTSMembers.Find(id);
            if (otsMember == null)
            {
                return HttpNotFound();
            }
            return View(otsMember);
        }

        // GET: OtsMembers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OtsMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,FirstName,LastName,SpouseName,Email,StreetAddress,City,State,Zip,Notes,OkToPublish")] OtsMember otsMember)
        {
            if (ModelState.IsValid)
            {
                db.OTSMembers.Add(otsMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(otsMember);
        }

        // GET: OtsMembers/Edit/5
        [Authorize()]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtsMember otsMember = db.OTSMembers.Find(id);
            if (otsMember == null)
            {
                return HttpNotFound();
            }
            return View(otsMember);
        }

        // POST: OtsMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FirstName,LastName,SpouseName,Email,StreetAddress,City,State,Zip,Notes,OkToPublish")] OtsMember otsMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(otsMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(otsMember);
        }

        // GET: OtsMembers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OtsMember otsMember = db.OTSMembers.Find(id);
            if (otsMember == null)
            {
                return HttpNotFound();
            }
            return View(otsMember);
        }

        // POST: OtsMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OtsMember otsMember = db.OTSMembers.Find(id);
            db.OTSMembers.Remove(otsMember);
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
