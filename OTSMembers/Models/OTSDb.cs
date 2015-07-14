using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OTSMembers.Models
{
    public class OtsDb : DbContext
    {
        public OtsDb() : base("name=DefaultConnection")
        {
        }
        public DbSet<OtsMember> OTSMembers { get; set; }
        public DbSet<MemberSponsorship> Sponsorships { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<FoodMenu> FoodMenus { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<OTSMembers.Models.RoleViewModel> RoleViewModels { get; set; }
        public DbSet<OTSMembers.Models.EditUserViewModel> EditUserViewModels { get; set; }
        public DbSet<OTSMembers.Models.OTSAddress> OTSAddresses { get; set; }
        public DbSet<C2013> _20131 { get; set; }

        public System.Data.Entity.DbSet<OTSMembers.Models.PaidMembersVM> PaidMembersVMs { get; set; }
    }
}