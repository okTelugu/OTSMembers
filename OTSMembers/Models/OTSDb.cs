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
        public DbSet<OtsMember> OTSMembers{ get; set; }
        public DbSet<MemberSponsorship> Sponsorships{ get; set; }
    }
}