namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.accessories", "Bag", c => c.Boolean(nullable: false));
            AddColumn("dbo.accessories", "Access", c => c.Boolean(nullable: false));
            AddColumn("dbo.accessories", "Watch", c => c.Boolean(nullable: false));
            AlterColumn("dbo.accessories", "photo", c => c.String());
            AlterColumn("dbo.accessories", "description", c => c.String(nullable: false));
            DropColumn("dbo.accessories", "NoOfpiece");
            DropColumn("dbo.accessories", "Bagid");
            DropColumn("dbo.accessories", "Accessid");
            DropColumn("dbo.accessories", "Watchid");
            DropColumn("dbo.accessories", "NoOfCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.accessories", "NoOfCategory", c => c.Int(nullable: false));
            AddColumn("dbo.accessories", "Watchid", c => c.Int(nullable: false));
            AddColumn("dbo.accessories", "Accessid", c => c.Int(nullable: false));
            AddColumn("dbo.accessories", "Bagid", c => c.Int(nullable: false));
            AddColumn("dbo.accessories", "NoOfpiece", c => c.Int(nullable: false));
            AlterColumn("dbo.accessories", "description", c => c.String());
            AlterColumn("dbo.accessories", "photo", c => c.String(nullable: false));
            DropColumn("dbo.accessories", "Watch");
            DropColumn("dbo.accessories", "Access");
            DropColumn("dbo.accessories", "Bag");
        }
    }
}
