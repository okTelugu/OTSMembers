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
    public class FoodMenusController : Controller
    {
        private OtsDb db = new OtsDb();

        // GET: FoodMenus
        public ActionResult Index()
        {
            return View(db.FoodMenus.ToList());
        }

        // GET: FoodMenus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodMenu foodMenu = db.FoodMenus.Find(id);
            if (foodMenu == null)
            {
                return HttpNotFound();
            }
            return View(foodMenu);
        }

        // GET: FoodMenus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodMenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Occassion,NotesIfAny")] FoodMenu foodMenu)
        {
            if (ModelState.IsValid)
            {
                db.FoodMenus.Add(foodMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(foodMenu);
        }

        // GET: FoodMenus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodMenu foodMenu = db.FoodMenus.Find(id);
            if (foodMenu == null)
            {
                return HttpNotFound();
            }
            return View(foodMenu);
        }

        // POST: FoodMenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Occassion,NotesIfAny")] FoodMenu foodMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodMenu);
        }

        // GET: FoodMenus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodMenu foodMenu = db.FoodMenus.Find(id);
            if (foodMenu == null)
            {
                return HttpNotFound();
            }
            return View(foodMenu);
        }

        // POST: FoodMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodMenu foodMenu = db.FoodMenus.Find(id);
            db.FoodMenus.Remove(foodMenu);
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
