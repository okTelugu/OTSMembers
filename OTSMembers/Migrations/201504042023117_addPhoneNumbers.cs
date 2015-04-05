namespace OTSMembers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPhoneNumbers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.C2013",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Last_Name = c.String(),
                        First_Name = c.String(),
                        Street_Address = c.String(),
                        City = c.String(),
                        Phone_Number_1 = c.String(),
                        Phone_Number_2 = c.String(),
                        EmailAddress_1 = c.String(),
                        Fees = c.Decimal(precision: 18, scale: 2),
                        OkToAddToAddressBook = c.Boolean(),
                        Company = c.String(),
                        Job_Title = c.String(),
                        Work_Number = c.String(),
                        SSMA_TimeStamp = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.OtsMembers", "Phone1", c => c.String());
            AddColumn("dbo.OtsMembers", "Phone2", c => c.String());
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo._2013",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Lastname = c.String(),
                        Firstname = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        PhoneNumber1 = c.String(),
                        PhoneNumber2 = c.String(),
                        EmailAddress1 = c.String(),
                        Fees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OkToAddToAddressBook = c.Boolean(nullable: false),
                        Company = c.String(),
                        JobTitle = c.String(),
                        WorkNumber = c.String(),
                        SSMA_TimeStamp = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.OtsMembers", "Phone2");
            DropColumn("dbo.OtsMembers", "Phone1");
            DropTable("dbo.C2013");
        }
    }
}
