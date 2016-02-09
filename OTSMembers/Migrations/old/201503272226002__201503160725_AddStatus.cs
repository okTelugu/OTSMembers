namespace OTSMembers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201503160725_AddStatus : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.MemberSponsorships", "verificationStatus", c => c.Int(nullable:false));
            //AddColumn("dbo.MemberSponsorships", "Notes", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.MemberSponsorships", "verificationStatus");
            //DropColumn("dbo.MemberSponsorships", "Notes");
        }
    }
}