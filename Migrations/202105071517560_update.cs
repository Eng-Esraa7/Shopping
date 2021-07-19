namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Dresses", "NoOfpiece");
            DropColumn("dbo.Dresses", "DressId");
            DropColumn("dbo.Dresses", "NoOfCategory");
            DropColumn("dbo.shorts", "NoOfpiece");
            DropColumn("dbo.shorts", "shortId");
            DropColumn("dbo.shorts", "NoOfCategory");
            DropColumn("dbo.suits", "NoOfpiece");
            DropColumn("dbo.suits", "suitId");
            DropColumn("dbo.suits", "NoOfCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.suits", "NoOfCategory", c => c.Int(nullable: false));
            AddColumn("dbo.suits", "suitId", c => c.Int(nullable: false));
            AddColumn("dbo.suits", "NoOfpiece", c => c.Int(nullable: false));
            AddColumn("dbo.shorts", "NoOfCategory", c => c.Int(nullable: false));
            AddColumn("dbo.shorts", "shortId", c => c.Int(nullable: false));
            AddColumn("dbo.shorts", "NoOfpiece", c => c.Int(nullable: false));
            AddColumn("dbo.Dresses", "NoOfCategory", c => c.Int(nullable: false));
            AddColumn("dbo.Dresses", "DressId", c => c.Int(nullable: false));
            AddColumn("dbo.Dresses", "NoOfpiece", c => c.Int());
        }
    }
}
