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
    public class MemberSponsorshipsController : Controller
    {
        private OtsDb db = new OtsDb();

        // GET: MemberSponsorships
        public ActionResult Index()
        {
            return View(db.Sponsorships.ToList());
        }

        // GET: MemberSponsorships/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSponsorship memberSponsorship = db.Sponsorships.Find(id);
            if (memberSponsorship == null)
            {
                return HttpNotFound();
            }
            return View(memberSponsorship);
        }

        // GET: MemberSponsorships/Create
        public ActionResult Create(int memberId)
        {
            Session["PrevUrl"] = Request.UrlReferrer;
            string tempUrl = Session["PrevUrl"].ToString();
            var member = db.OTSMembers.Where(m => m.id == memberId);
            ViewBag.Title = member.Select(m => m.FirstName) +","+ member.Select(m => m.LastName);
            MemberSponsorship memberSponsorship = new MemberSponsorship { OtsMember_id = memberId, PaymentDate = DateTime.Today };
            return View(memberSponsorship);
        }

        // POST: MemberSponsorships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PaymentDate,Amount,TypeOfPayment,ReferredBy,PaymentID,Occassion,OtsMember_id")] MemberSponsorship memberSponsorship)
        {
            if (ModelState.IsValid)
            {
                db.Sponsorships.Add(memberSponsorship);
                db.SaveChanges();
                string tempUrl = Session["PrevUrl"].ToString();
                return Redirect(tempUrl);
            }
            return View(memberSponsorship);
        }

        // GET: MemberSponsorships/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSponsorship memberSponsorship = db.Sponsorships.Find(id);
            if (memberSponsorship == null)
            {
                return HttpNotFound();
            }
            return View(memberSponsorship);
        }

        // POST: MemberSponsorships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PaymentDate,Amount,TypeOfPayment,ReferredBy,PaymentID,Occassion,OtsMember_id")] MemberSponsorship memberSponsorship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberSponsorship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberSponsorship);
        }

        // GET: MemberSponsorships/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberSponsorship memberSponsorship = db.Sponsorships.Find(id);
            if (memberSponsorship == null)
            {
                return HttpNotFound();
            }
            return View(memberSponsorship);
        }

        // POST: MemberSponsorships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberSponsorship memberSponsorship = db.Sponsorships.Find(id);
            db.Sponsorships.Remove(memberSponsorship);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult payPalAction()
        {
            return PartialView("PaypalPmt");
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
