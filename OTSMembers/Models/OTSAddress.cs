using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTSMembers.Models
{
    public class OTSAddress
    {
        public int Id { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
    }
}