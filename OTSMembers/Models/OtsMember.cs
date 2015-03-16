using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OTSMembers.Models
{
    public class OtsMember
    {
        public int id { get; set; }
        [Required(ErrorMessage="First Name must be entered.")]
        [MinLength(2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name must be entered.")]
        [Display(Name = "Last Name")]
        [MinLength(2)]
        public string LastName { get; set; }
        [Display (Name="Spouse Name")]
        public string SpouseName { get; set; }
        [Required(ErrorMessage = "Email must be entered.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Notes { get; set; }
        [Display(Name = "Okay to publish to the directory?")]
        public bool OkToPublish { get; set; }
        public IQueryable<MemberSponsorship> Sponsorship { get; set; }
    }
}