using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OTSMembers.Models;
using MailChimp;
using MailChimp.Lists;
using System.Runtime.Serialization;
using System.Collections;
using System.IO;
using System.Xml;
using MailChimp.Helper;


namespace OTSMembers.Controllers
{
    public class OtsMembersController : Controller
    {
        private OtsDb db = new OtsDb();
        private string mailChimpListId = "17decdb0b8";
        private string mailChimpAPI = "78c45040af2421f3ce69b8a8961189ff-us4";
        public List<string> oldOTSEmailList = new List<string>();
        public ActionResult Sponsorships(int? memberID)
        {
            var model = db.Sponsorships.Where(s => s.OtsMember_id == memberID).ToList();
            return PartialView(model);
        }

        // GET: OtsMembers
        [Authorize]
        public ActionResult Index()
        {

            if (User.Identity.Name != null)
            {
                var model = db.OTSMembers
                .Where(r => r.Email.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));
                return View(model);
            }
            else { 
               return View();
            }
        }
        [Authorize]
        public ActionResult MembersList()
        {
            var model = db.OTSMembers;
            if (model == null)
            {
                return HttpNotFound("Member List Not Found"); //TODO: CHANGE THIS TO A NICER MESSAGE BOX.
            }
            return View(model);
        }
        public ActionResult Directory(string searchEmail = null, string firstName = null, string lastName = null)
        {
            if (searchEmail != null)
            {
                var model = db.OTSMembers
                .Where(r => r.Email.Equals(searchEmail, StringComparison.InvariantCultureIgnoreCase) && r.OkToPublish);
                return View(model);
            }
            else if (lastName != null || firstName != null)
            {
                var model = db.OTSMembers
                .Where(r => (r.LastName.Contains(lastName) && r.FirstName.Contains(firstName)) && r.OkToPublish);
                return View(model);
            }
            else
            {
                return View();
            }
        }
        public ActionResult SearchByEmail(string searchEmail = null)
        {
            var model = db.OTSMembers
                .Where(r => r.Email.Equals(searchEmail, StringComparison.InvariantCultureIgnoreCase));
            return PartialView(model);
        }

        public ActionResult MemberName(int? OtsMember_id)
        {
            if (OtsMember_id == null)
            {
                return Content("");
            }
            OtsMember otsMember = db.OTSMembers.Find(OtsMember_id);
            if (otsMember == null)
            {
                return Content("");
            }
            return Content(otsMember.FirstName + "," + otsMember.LastName);
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
        public ActionResult Create([Bind(Include = "id,FirstName,LastName,SpouseName,OtherEmail,StreetAddress,StreetAddress2,City,State,Zip,Notes,Phone1,Phone2,OkToPublish")] OtsMember otsMember)
        {
            if (ModelState.IsValid)
            {
                otsMember.Email = User.Identity.Name;
                AddThisMemberToMailChimp(otsMember);
                db.OTSMembers.Add(otsMember);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(otsMember);
        }

        // GET: OtsMembers/Create
        public ActionResult CreateMember()
        {
            return View();
        }

        // POST: OtsMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMember([Bind(Include = "id,FirstName,LastName,SpouseName,StreetAddress,StreetAddress2,City,State,Zip,Notes,Email,OtherEmail,Phone1,Phone2,OkToPublish")] OtsMember otsMember)
        {
            if (ModelState.IsValid)
            {
                AddThisMemberToMailChimp(otsMember);
                db.OTSMembers.Add(otsMember);
                db.SaveChanges();
                return RedirectToAction("MembersList");
            }

            return View(otsMember);
        }

        [DataContract()]
        public class MyMergeVar : MergeVar
        {
            [DataMember(Name = "FNAME")]
            public string FirstName { get; set; }
            [DataMember(Name = "LNAME")]
            public string LastName { get; set; }
        }
        private void AddThisMemberToMailChimp(OtsMember otsMember)
        {

            MyMergeVar myMergeVars = new MyMergeVar();
            myMergeVars.FirstName = otsMember.FirstName;
            myMergeVars.LastName = otsMember.LastName;

            MailChimpManager mc = new MailChimpManager(mailChimpAPI);

            //  Create the email parameter
            EmailParameter email = new EmailParameter()
            {
                Email = otsMember.Email
            };
            EmailParameter OtherEmail = new EmailParameter()
            {
                Email = otsMember.OtherEmail
            };
            try
            {
                //EmailParameter results = mc.Subscribe(mailChimpListId, email, myMergeVars,doubleOptIn: false);
                List<EmailParameter> list = new List<EmailParameter>();
                if (email.Email != null)
                    list.Add(email);
                if (OtherEmail.Email != null)
                    list.Add(OtherEmail);
                MemberInfoResult membInfo = mc.GetMemberInfo(mailChimpListId,list);
                int i = 0;
                foreach(var listEmail in list )
                {
                    if( i < membInfo.Data.Count ) {
                        if (
                            string.Equals ( membInfo.Data[i].Email ?? "" , listEmail.Email, StringComparison.InvariantCultureIgnoreCase ) )
                        { // already exists. so, just update the records.
                            EmailParameter results = mc.UpdateMember(mailChimpListId, listEmail, myMergeVars);
                        }
                        else
                        {// doesn't exist, subscribe
                            EmailParameter results = mc.Subscribe(mailChimpListId, listEmail, myMergeVars, doubleOptIn: false);
                        }

                    }
                    else// doesn't exist, subscribe
                    {
                        EmailParameter results = mc.Subscribe(mailChimpListId, listEmail, myMergeVars, doubleOptIn: false);
                    }
                    i++;
                }
            }
            catch
            {
                // TODO: pop up a message saying could not subscribe to the email list. 
                //Please contact president@Oktelugu.org to have the member added manually to the list.
            }
        }
        private void EditThisMemberInMailChimp(OtsMember otsMember, List<string> oldEmailList)
        {
            MailChimpManager mc = new MailChimpManager(mailChimpAPI);
            MyMergeVar myMergeVars = new MyMergeVar();
            myMergeVars.FirstName = otsMember.FirstName;
            myMergeVars.LastName = otsMember.LastName;
            int i = 0;
            try {
                foreach (var oldEmail in oldEmailList)
                {
                    string newEmail = (i == 0) ? otsMember.Email : otsMember.OtherEmail;
                    if (!string.Equals(oldEmail, newEmail, StringComparison.InvariantCultureIgnoreCase))
                    {
                        //unsubscribe old email.
                        //  Create the email parameter
                        EmailParameter oldemail = new EmailParameter()
                        {
                            Email = oldEmail
                        };
                        //check if the old email exists in the list.
                        //EmailParameter results = mc.Subscribe(mailChimpListId, email, myMergeVars,doubleOptIn: false);
                        List<EmailParameter> list = new List<EmailParameter>();
                        list.Add(oldemail);
                        MemberInfoResult membInfo = mc.GetMemberInfo(mailChimpListId, list);
                        if (membInfo.SuccessCount> 0)
                        {
                            mc.Unsubscribe(mailChimpListId, oldemail, true, false, false);
                        }
                        //else //  don't need do anything.
                        //{

                        //}
                        

                        //subscribe new email.
                        EmailParameter newemail = new EmailParameter()
                        {
                            Email = (i == 0) ? otsMember.Email : otsMember.OtherEmail
                        };
                        EmailParameter results = new EmailParameter();
                        if ( newemail.Email != null && newemail.Email.ToLower() != "notavailable@oktelugu.org")
                            results = mc.Subscribe(mailChimpListId, newemail, myMergeVars, doubleOptIn: false);

                    }
                    else // email address matches, so just update the member info.
                    {
                                             //subscribe new email.
                        EmailParameter newemail = new EmailParameter()
                        {
                            Email = (i == 0) ? otsMember.Email : otsMember.OtherEmail
                            
                        };
                        EmailParameter results = mc.UpdateMember(mailChimpListId, newemail, myMergeVars);
                    }
                    i++;
                }
                if (oldEmailList.Count < 2 && otsMember.OtherEmail != null)
                {                        //subscribe new email.
                    EmailParameter newemail = new EmailParameter()
                    {
                        Email = otsMember.OtherEmail
                    };
                    EmailParameter results = new EmailParameter();
                    if (newemail.Email != null && newemail.Email.ToLower() != "notavailable@oktelugu.org")
                        results = mc.Subscribe(mailChimpListId, newemail, myMergeVars, doubleOptIn: false);

                }
            }

            catch
            {
                // TODO: pop up a message saying could not subscribe to the email list. 
                //Please contact president@Oktelugu.org to have the member added manually to the list.
            }
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
            Session["oldEmail1"] = otsMember.Email;
            Session["oldEmail2"] = otsMember.OtherEmail;
            return View(otsMember);
        }

        // POST: OtsMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,FirstName,LastName,SpouseName,Email,OtherEmail,StreetAddress,StreetAddress2,City,State,Zip,Notes,Phone1,Phone2,OkToPublish")] OtsMember otsMember)
        {
            if (ModelState.IsValid)
            {
                if (otsMember.Email == null || otsMember.Email.Trim() == "")
                    otsMember.Email = "notavailable@oktelugu.org";
                oldOTSEmailList.Clear();
                if ( Session["oldEmail1"] != null )
                    oldOTSEmailList.Add(Session["oldEmail1"].ToString());
                if ( Session["oldEmail2"] != null )
                    oldOTSEmailList.Add(Session["oldEmail2"].ToString());
                EditThisMemberInMailChimp(otsMember, oldOTSEmailList);
                db.Entry(otsMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(otsMember);
            
            
        }
        [Authorize(Roles = "Administrator,Committee")]
        public ActionResult EditMember(int? id)
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
            Session["oldEmail1"] = otsMember.Email;
            Session["oldEmail2"] = otsMember.OtherEmail;
            return View(otsMember);
        }

        // POST: OtsMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMember([Bind(Include = "id,FirstName,LastName,SpouseName,Email,OtherEmail,StreetAddress,StreetAddress2,City,State,Zip,Notes,Phone1,Phone2,OkToPublish")] OtsMember otsMember)
        {
            if (ModelState.IsValid)
            {
                if (otsMember.Email == null || otsMember.Email.Trim() == "")
                    otsMember.Email = "notavailable@oktelugu.org";
                oldOTSEmailList.Clear();
                if (Session["oldEmail1"] != null)
                    oldOTSEmailList.Add(Session["oldEmail1"].ToString());
                if (Session["oldEmail2"] != null)
                    oldOTSEmailList.Add(Session["oldEmail2"].ToString());
                EditThisMemberInMailChimp(otsMember, oldOTSEmailList);
                db.Entry(otsMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MembersList");

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
            DeleteThisMemberFromMailChimp(otsMember);
            db.OTSMembers.Remove(otsMember);
            db.SaveChanges();
            return RedirectToAction("MembersList");
        }
        private void DeleteThisMemberFromMailChimp(OtsMember otsMember)
        {
            MailChimpManager mc = new MailChimpManager(mailChimpAPI);
            MyMergeVar myMergeVars = new MyMergeVar();
            myMergeVars.FirstName = otsMember.FirstName;
            myMergeVars.LastName = otsMember.LastName;
            List<string> emailList = new List<string>();
            emailList.Add(otsMember.Email);
            emailList.Add(otsMember.OtherEmail);
            int i = 0;
            try
            {
                foreach (var email in emailList)
                {
                    EmailParameter emailToBeDeleted = new EmailParameter()
                    {
                        Email = email
                    };
                        //check if the old email exists in the list.
                        //EmailParameter results = mc.Subscribe(mailChimpListId, email, myMergeVars,doubleOptIn: false);
                    List<EmailParameter> list = new List<EmailParameter>();
                    list.Add(emailToBeDeleted);
                    MemberInfoResult membInfo = mc.GetMemberInfo(mailChimpListId, list);
                    if (membInfo.SuccessCount > 0)
                    {
                        mc.Unsubscribe(mailChimpListId, emailToBeDeleted, true, false, false);
                    }

                    i++;
                }
            }

            catch
            {
                // TODO: pop up a message saying could not subscribe to the email list. 
                //Please contact president@Oktelugu.org to have the member added manually to the list.
            }
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
