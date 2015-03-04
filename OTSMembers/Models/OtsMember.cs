using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTSMembers.Models
{
    public class OtsMember
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SpouseName { get; set; }
        public string StreeAddress { get;   set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public string Notes { get; set; }
        public bool OkToPublish { get; set; }
        public ICollection<MemberSponsorship> Sponsorship { get; set; }
    }
}