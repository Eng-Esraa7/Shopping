namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.shoes", "Girls", c => c.Boolean(nullable: false));
            AddColumn("dbo.shoes", "Man", c => c.Boolean(nullable: false));
            DropColumn("dbo.shoes", "Girlsid");
            DropColumn("dbo.shoes", "Manid");
            DropColumn("dbo.shoes", "Mixid");
            DropColumn("dbo.shoes", "NoOfCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.shoes", "NoOfCategory", c => c.Int(nullable: false));
            AddColumn("dbo.shoes", "Mixid", c => c.Int(nullable: false));
            AddColumn("dbo.shoes", "Manid", c => c.Int(nullable: false));
            AddColumn("dbo.shoes", "Girlsid", c => c.Int(nullable: false));
            DropColumn("dbo.shoes", "Man");
            DropColumn("dbo.shoes", "Girls");
        }
    }
}
