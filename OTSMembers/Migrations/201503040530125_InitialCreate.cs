namespace OTSMembers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                        Notes = c.String(),
                        OkToPublish = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.MemberSponsorships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TypeOfPayment = c.Int(nullable: false),
                        ReferredBy = c.String(),
                        Anonymous = c.Boolean(nullable: false),
                        OtsMember_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OtsMembers", t => t.OtsMember_id)
                .Index(t => t.OtsMember_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberSponsorships", "OtsMember_id", "dbo.OtsMembers");
            DropIndex("dbo.MemberSponsorships", new[] { "OtsMember_id" });
            DropTable("dbo.MemberSponsorships");
            DropTable("dbo.OtsMembers");
        }
    }
}
