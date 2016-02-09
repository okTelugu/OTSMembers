namespace OTSMembers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFoodItemMenuId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItems", "FoodMenu_Id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodItems", "FoodMenu_Id");
        }
    }
}
