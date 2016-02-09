namespace OTSMembers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201503160725_AddTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberSponsorships", "TransactionId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberSponsorships", "TransactionId");
        }
    }
}
