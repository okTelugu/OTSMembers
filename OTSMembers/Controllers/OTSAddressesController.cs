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
    [Authorize(Roles = "Administrator,Committee")]
    public class OTSAddressesController : Controller
    {
        private OtsDb db = new OtsDb();

        // GET: OTSAddresses
        public ActionResult Index()
        {
            return View(db.OTSAddresses.ToList());
        }
        // GET: OTSAddresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OTSAddress oTSAddress = db.OTSAddresses.Find(id);
            if (oTSAddress == null)
            {
                return HttpNotFound();
            }
            return View(oTSAddress);
        }

        // GET: OTSAddresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OTSAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StreetAddress1,StreetAddress2,City,State,Zip")] OTSAddress oTSAddress)
        {
            if (ModelState.IsValid)
            {
                db.OTSAddresses.Add(oTSAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oTSAddress);
        }

        // GET: OTSAddresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OTSAddress oTSAddress = db.OTSAddresses.Find(id);
            if (oTSAddress == null)
            {
                return HttpNotFound();
            }
            return View(oTSAddress);
        }

        // POST: OTSAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StreetAddress1,StreetAddress2,City,State,Zip")] OTSAddress oTSAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oTSAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oTSAddress);
        }

        // GET: OTSAddresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OTSAddress oTSAddress = db.OTSAddresses.Find(id);
            if (oTSAddress == null)
            {
                return HttpNotFound();
            }
            return View(oTSAddress);
        }

        // POST: OTSAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OTSAddress oTSAddress = db.OTSAddresses.Find(id);
            db.OTSAddresses.Remove(oTSAddress);
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
