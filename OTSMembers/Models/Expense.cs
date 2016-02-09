using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OTSMembers.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name must be entered.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid First Name")]
        [Display(Name = "Expense made by: First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name must be entered.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Invalid Last Name")]
        [Display(Name = "Expense made by: Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:f2}")]
        [Range(1, 10000000, ErrorMessage = "Please enter a valid amount.")]
        [Required(ErrorMessage = "Please enter a valid amount.")]
        public decimal Amount { get; set; }
        [Display(Name="Reciept Included?")]
        public bool RecieptIncluded { get; set; }
        [Display(Name = "Reimbursed?")]
        public bool Reimbursed { get; set; }
        [Display(Name = "OTS Check Number")]
        public int CheckNumber { get; set; }
        [Display(Name = "Reciept /Invoice Number if any:")]
        public string RecieptNumber { get; set; }
        [Display(Name = "Expense made for")]
        public string Description { get; set; }
        [Display(Name = "Approved by")]
        public string ApprovedBy { get; set; }
    }
}