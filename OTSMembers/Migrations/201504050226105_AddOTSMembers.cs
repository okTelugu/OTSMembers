namespace OTSMembers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOTSMembers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
    "dbo.OtsMembers",
    c => new
    {
        id = c.Int(nullable: false, identity: true),
        FirstName = c.String(),
        LastName = c.String(),
        SpouseName = c.String(),
        Email = c.String(),
        StreetAddress = c.String(),
        City = c.String(),
        State = c.String(),
        Zip = c.Int(nullable: false),
        Phone1 = c.String(),
        Phone2 = c.String(),
        Notes = c.String(),
        OkToPublish = c.Boolean(nullable: false),

    })
    .PrimaryKey(t => t.id);


        }
        
        public override void Down()
        {
            DropTable("dbo.OtsMembers");
        }
    }
}
