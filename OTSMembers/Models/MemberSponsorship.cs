using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTSMembers.Models
{
    public class MemberSponsorship
    {
        public enum ModeOfPayment {
            Check,
            Online,
            CreditCard,
            Cash
        };
        public int Id { get; set; }
        public DateTime PaymentDate{ get; set; }
        public decimal Amount { get; set; }
        public ModeOfPayment TypeOfPayment  { get; set; }
        public string ReferredBy { get; set; }
        public string PaymentID { get; set; }
        public string Occassion { get; set; }
    }
}