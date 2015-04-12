namespace OTSMembers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStreetAddress2SpouseEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OtsMembers", "OtherEmail", c => c.String());
            AddColumn("dbo.OtsMembers", "StreetAddress2", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OtsMembers", "StreetAddress2");
            DropColumn("dbo.OtsMembers", "OtherEmail");
        }
    }
}
