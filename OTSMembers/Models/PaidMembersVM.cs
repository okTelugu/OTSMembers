using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTSMembers.Models
{
    public class PaidMembersVM
    {
        public int Id { get; set; }
        public int OtsMember_id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Spouse Name")]
        public string SpouseName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Additional Email")]
        public string OtherEmail { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Name = "Street Address Contd.")]
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Invalid State Name")]
        public string State { get; set; }
        [Display(Name = "Zip")]
        public int Zip { get; set; }
        [Display(Name = "Phone Number 1")]
        public string Phone1 { get; set; }
        [Display(Name = "Phone Number 2")]
        public string Phone2 { get; set; }
        [Display(Name = "Annual Sponsorship")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:f2}")]
        public decimal AnnualSponsorship { get; set; }

    }
}
