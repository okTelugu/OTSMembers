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
            CreditCard_Manual_Swipe,
            Cash
        };
        public enum VerificationPending
        {
            VerificationPending,
            Approved
        }
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate{ get; set; }
        [Display(Name = "Sponsorship Amount")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:f2}")]
        [Range(1,10000000,ErrorMessage="Please enter a valid amount.")]
        [Required(ErrorMessage = "Please enter a valid amount.")]
        public decimal Amount { get; set; }
        [Display(Name = "Mode of Payment")]
        [Required(ErrorMessage = "Select a valid mode of payment.")]
        public ModeOfPayment TypeOfPayment  { get; set; }
        [Display(Name = "Referred by")]
        public string ReferredBy { get; set; }
        [Display(Name="Receipt or Check Number")]
        public string PaymentID { get; set; }
        [Required(ErrorMessage="Please enter the occassion for which this sponsorship is attributed to.")]
        public string Occassion { get; set; }
        public int OtsMember_id { get; set; }
        [Display(Name="Verification Status")]
        public VerificationPending verificationStatus { get; set; }
        [Display(Name="Notes if any")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
        public string TransactionId { get; set; }
    }
}