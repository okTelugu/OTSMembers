using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "Sponsorship Amount")]
        [Required(ErrorMessage = "Select a valid amount.")]
        public decimal Amount { get; set; }
        [Display(Name = "Mode of Payment")]
        [Required(ErrorMessage = "Select a valid mode of payment.")]
        public ModeOfPayment TypeOfPayment  { get; set; }
        [Display(Name = "Referred by")]
        public string ReferredBy { get; set; }
        public string PaymentID { get; set; }
        [Required(ErrorMessage="Please enter the occassion for which this sponsorship is attributed to.")]
        public string Occassion { get; set; }
        public int OtsMember_id { get; set; }
    }
}