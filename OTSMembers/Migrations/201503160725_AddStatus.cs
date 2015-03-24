using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace OTSMembers.Migrations
{
    public class _201503160725_AddStatus : DbMigration
    {
        public override void Up()
        {

            AddColumn(
                "dbo.OtsMembers","Zip",
                c => c.Int(nullable: true));

            AddColumn("dbo.MemberSponsorships", "verificationStatus", c => c.Int(nullable:false));
            AddColumn("dbo.MemberSponsorships", "Notes", c => c.String(nullable: true));
        }

        public override void Down()
        {
            DropColumn("dbo.OtsMembers", "Zip");
            DropColumn("dbo.OtsMembers", "verificationStatus");
            DropColumn("dbo.OtsMembers", "Notes");
        }
    }
}