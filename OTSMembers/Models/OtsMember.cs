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
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name must be entered.")]
        [Display(Name = "Last Name")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid Last Name")]
        public string LastName { get; set; }
        [Display (Name="Spouse Name")]
        [StringLength(30, MinimumLength = 0, ErrorMessage = "Invalid Spouse Name")]
        public string SpouseName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a valid street address.")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Invalid City Name")]
        [Required(ErrorMessage = "Please enter a valid City.")]
        public string City { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Invalid State Name")]
        [Required(ErrorMessage = "Please enter a valid State.")]
        public string State { get; set; }
        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = " Zip code must be 5 characters length")] 
        [Display(Name="Zip")]
        public int Zip { get; set; }
        [Display(Name = "Phone Number 1")]
        public string Phone1 { get; set; }
        [Display(Name = "Phone Number 2")]
        public string Phone2 { get; set; }
        public string Notes { get; set; }
        [Display(Name = "Okay to publish to the directory?")]
        public bool OkToPublish { get; set; }
        public IQueryable<MemberSponsorship> Sponsorship { get; set; }
    }
}