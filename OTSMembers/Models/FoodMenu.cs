using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OTSMembers.Models
{
    public class FoodMenu
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Please enter the occassion for which food was ordered.")]
        public string Occassion { get; set; }
        public virtual IEnumerable<FoodItem> FoodItemList { get; set; }
        [Display(Name="Notes if any")]
        public string NotesIfAny { get; set; }
    }
}