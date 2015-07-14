using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OTSMembers.Models;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Mandrill;

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
            //ViewBag.Title = member.Select(m => m.FirstName) +","+ member.Select(m => m.LastName);
            MemberSponsorship memberSponsorship = new MemberSponsorship { OtsMember_id = memberId, PaymentDate = DateTime.Today };
            return View(memberSponsorship);
        }

        // POST: MemberSponsorships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PaymentDate,Amount,TypeOfPayment,ReferredBy,PaymentID,Occassion,Notes,OtsMember_id")] MemberSponsorship memberSponsorship)
        {
            if (ModelState.IsValid)
            {
                memberSponsorship.TransactionId = GenerateTransactionID(memberSponsorship);
                db.Sponsorships.Add(memberSponsorship);
                db.SaveChanges();
                var address = db.OTSAddresses.FirstOrDefault();

                PaymentInstructionsVM paymentIns = new PaymentInstructionsVM
                                    {
                                        MemberId = memberSponsorship.OtsMember_id,
                                        TransactionID = memberSponsorship.TransactionId,
                                        StreetAddress1 = address.StreetAddress1,
                                        StreetAddress2 = address.StreetAddress2,
                                        City = address.City,
                                        State = address.State,
                                        Zip = address.Zip,
                                        TypeofPayment = memberSponsorship.TypeOfPayment,
                                        Amount =memberSponsorship.Amount,
                                        Email = db.OTSMembers
                                                .Where(m => m.id == memberSponsorship.OtsMember_id)
                                                .Select(m => m.Email).ToList().First(),
                                        FirstName = db.OTSMembers
                                                .Where(m => m.id == memberSponsorship.OtsMember_id)
                                                .Select(m => m.FirstName).ToList().First(),
                                        LastName = db.OTSMembers
                                                .Where(m => m.id == memberSponsorship.OtsMember_id)
                                                .Select(m => m.LastName).ToList().First(),
                                            
                                    };
                EmailHelper emailhelper = new EmailHelper();
                await emailhelper.SendTransactionEmail(paymentIns);
                
                TempData["paymentInstructions"] = paymentIns;
                return RedirectToAction("Thankyou");
                //string tempUrl = Session["PrevUrl"].ToString();
                //return Redirect(tempUrl);
            }
            return View(memberSponsorship);
        }

   
        public ActionResult Thankyou()
        {
            var model = TempData["paymentInstructions"] as PaymentInstructionsVM;
            return View(model);
        }

        private string GenerateTransactionID(MemberSponsorship memberSponsorship)
        {
            string transId = "000";
            var randId = RandomThreadSafe.Next();
            
            transId = 
                //memberSponsorship.OtsMember_id.ToString()
                //+ memberSponsorship.PaymentDate.Year.ToString()
                //+ memberSponsorship.PaymentDate.Day.ToString()
                //+ memberSponsorship.PaymentDate.Month.ToString()+
                randId.ToString();

            switch(memberSponsorship.TypeOfPayment){
                case OTSMembers.Models.MemberSponsorship.ModeOfPayment.Online :
                    transId = "ONL-" + transId;
                    break;
                case OTSMembers.Models.MemberSponsorship.ModeOfPayment.Cash:
                    transId = "CSH-" + transId;
                    break;
                case OTSMembers.Models.MemberSponsorship.ModeOfPayment.CreditCard_Manual_Swipe:
                    transId = "CCM-" + transId;
                    break;
                case OTSMembers.Models.MemberSponsorship.ModeOfPayment.Check:
                    transId = "CHK-" + transId;
                    break;
                default :
                    break;

            }
            return (transId);
        }

        private class RandomThreadSafe
        {
            private static RNGCryptoServiceProvider _global = 
            new RNGCryptoServiceProvider();
            [ThreadStatic]
            private static Random _local;
            public static int Next()
            {
                Random inst = _local;
                if (inst == null)
                {
                    byte[] buffer = new byte[4];
                    _global.GetBytes(buffer);
                    _local = inst = new Random(
                        BitConverter.ToInt32(buffer, 0));
                }
                return inst.Next();
            }

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
        public ActionResult Edit([Bind(Include = "Id,PaymentDate,Amount,TypeOfPayment,ReferredBy,PaymentID,Occassion,TransactionId, verificationStatus, OtsMember_id")] MemberSponsorship memberSponsorship)
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
        public ActionResult CurrentMembersList()
        {
            return View();
        }
        public ActionResult PaidMembersList(int? year, decimal? amount, DateTime fromDate, DateTime toDate)
        {
            //List<PaidMembersVM> model = new List<PaidMembersVM>();
            // build the list...
            var sponsorsList = //db.Sponsorships.ToList().GroupBy(r=>r.OtsMember_id);
            from s in db.Sponsorships 
            group s by new {s.OtsMember_id} into g
            select new {g.Key.OtsMember_id ,
                        Sum = g.Sum(s => s.Amount)
            };

            var model = GetcurrentList(year, amount, fromDate, toDate);
            return View(model);
        }
        private List<PaidMembersVM> GetcurrentList(int? year, decimal? amount, DateTime fromDate, DateTime toDate)
        {
         
            var sponsorsList = 
            from s in db.Sponsorships
            where (System.Data.Entity.DbFunctions.TruncateTime(s.PaymentDate) >= fromDate.Date && System.Data.Entity.DbFunctions.TruncateTime(s.PaymentDate) <= toDate.Date)
            group s by new { s.OtsMember_id } into g
            select new
            {
                g.Key.OtsMember_id,
                Sum = g.Sum(s => s.Amount)
            };

            var query =

            (from s in sponsorsList
             where s.Sum >= amount
             join m in db.OTSMembers on s.OtsMember_id equals m.id
             select new 
             {
                 s.OtsMember_id,
                 m.FirstName,
                 m.LastName,
                 m.SpouseName,
                 m.Email,
                 m.OtherEmail,
                 m.StreetAddress,
                 m.StreetAddress2,
                 m.City,
                 m.State,
                 m.Zip,
                 m.Phone1,
                 m.Phone2,
                 s.Sum
             }).ToList()
             .Select( x =>new PaidMembersVM
             {
                 OtsMember_id = x.OtsMember_id,
                 FirstName = x.FirstName,
                 LastName = x.LastName,
                 SpouseName = x.SpouseName,
                 Email = x.Email,
                 OtherEmail = x.OtherEmail,
                 StreetAddress = x.StreetAddress,
                 StreetAddress2 = x.StreetAddress2,
                 City = x.City,
                 State = x.State,
                 Zip = x.Zip,
                 Phone1 = x.Phone1,
                 Phone2 = x.Phone2,
                 AnnualSponsorship = x.Sum
             });
            return (query.ToList());
        }

    }
    public class EmailHelper
    {
        public Task SendTransactionEmail(PaymentInstructionsVM instructions)
        {
            //if (string.IsNullOrEmpty(instructions.Email)) {
            //    return();
            //}
            var transactionMessage = new EmailMessage();

            // Subject
            transactionMessage.subject = "Thank you for your pledge!";
            
            //To address
            EmailAddress toEmail = new EmailAddress { email = instructions.Email, name = instructions.FirstName + " " + instructions.LastName, type = "to" };
            EmailAddress ccEmail = new EmailAddress { email = "president@oktelugu.org" , name = "OTS Treasurer", type = "cc" };
            IEnumerable<EmailAddress> e1 = new EmailAddress[] { toEmail, ccEmail };
            transactionMessage.to = e1;
            
            //From address
            transactionMessage.from_email = "president@oktelugu.org";
            transactionMessage.from_name = "OTS President";
            
            //Body
            string name = (instructions.FirstName.Trim() + " " + instructions.LastName.Trim()).Trim();
            transactionMessage.text = "Dear " + name + " garu," +
                                      " Thank you for your pledge of " + instructions.Amount + "  Dollars." +
                                      " Transaction number for your pledge is : "+ instructions.TransactionID +"."+
                                      " If you want to mail your donations, please mail us to "+
                                      "Oklahoma Telugu Sangham " + instructions.StreetAddress1 + " " +
                                      instructions.StreetAddress2 + " " + instructions.City +" " + 
                                      instructions.State + " " + instructions.Zip +" quoting the above transaction ID."+
                                      "As soon as we recieve your payments and match with the pledge, we will update the pledge status on the website. If you made a donation online, you will get a seperate email confirmation." +
                                      " On behalf of OTS executive committee and board we thank you for your"+ 
                                      " continued support to the services of the organization. " + 
                                      " If you have any feedback or suggestions on how we can improve the services, " + 
                                      "please feel free to email us at any time. - Oklahoma Telugu Sangham";
            transactionMessage.html = "<p>Dear " + name + " garu,</p>"+
                                     "<p>Thank you for your pledge of <strong>" + instructions.Amount + "USD. </strong>" +
                                        " Transaction number for your pledge is : <strong>" + instructions.TransactionID + "</strong>.</br>" +
                                        " If you want to mail your donations, please mail us to: " +
                                        "<strong>Oklahoma Telugu Sangham " + instructions.StreetAddress1 + " " +
                                        instructions.StreetAddress2 + " " + instructions.City + " " +
                                        instructions.State + " " + instructions.Zip + "</strong> quoting the above transaction ID. As soon as we recieve your payments and match with the pledge, we will update the pledge status on the website.</p><p> If you made a donation online, you will get a seperate email confirmation.</p></p>" +
                                        " On behalf of OTS executive committee and board we thank you for your continued support to the services of the organization. If you have any feedback or suggestions on how we can improve the services, please feel free to email us at any time. </p>"+
                                        "<p> - Oklahoma Telugu Sangham</p>"; 
            
            return configMandrillasyncSend(transactionMessage);

        }

        private Task configMandrillasyncSend(EmailMessage message)
        {

            MandrillApi api = new MandrillApi("C9iWQgGOhPjavOv6D1t0WQ");

            var task = api.SendMessageAsync(message);

            if (task.IsCompleted)
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }

    }
}
