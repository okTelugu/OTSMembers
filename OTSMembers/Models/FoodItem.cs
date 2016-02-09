using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OTSMembers.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Please enter item description such as curry name.")]
        [Display(Name="Item Description")]
        public string ItemDescription { get; set; }
        [Required(ErrorMessage="Please enter the quantity such as number of trays (big / small).")]
        [Display(Name = "Quantity Ordered")]
        public string QuantityOrdered { get; set; }
        [Display(Name = "Cost if known per item")]
        public decimal Cost { get; set; }
        [Display(Name="Comments about the food, if any")]
        public string CommentsIfAny { get; set; }
        public int FoodMenu_Id { get; set; }
    }
}