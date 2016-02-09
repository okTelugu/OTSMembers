using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTSMembers.Models
{
    public class PaymentInstructionsVM
    {
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string TransactionID { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public  OTSMembers.Models.MemberSponsorship.ModeOfPayment TypeofPayment{ get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
