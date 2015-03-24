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
        [Required(ErrorMessage = "Please enter a valid street address.")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "Please enter a valid City.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter a valid State.")]
        public string State { get; set; }
        [RegularExpression(@"/(^\d{5}(-\d{4})?)$/", ErrorMessage = " Zip code must be 5 characters length")] 
        [Display(Name="Zip")]
        public int Zip { get; set; }
        public string Notes { get; set; }
        [Display(Name = "Okay to publish to the directory?")]
        public bool OkToPublish { get; set; }
        public IQueryable<MemberSponsorship> Sponsorship { get; set; }
    }
}