namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.children", "photo", c => c.String());
            AlterColumn("dbo.shirts", "photo", c => c.String());
            DropColumn("dbo.children", "NoOfpiece");
            DropColumn("dbo.children", "cheldrenId");
            DropColumn("dbo.children", "NoOfCategory");
            DropColumn("dbo.shirts", "NoOfpiece");
            DropColumn("dbo.shirts", "shirtsId");
            DropColumn("dbo.shirts", "NoOfCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.shirts", "NoOfCategory", c => c.Int(nullable: false));
            AddColumn("dbo.shirts", "shirtsId", c => c.Int(nullable: false));
            AddColumn("dbo.shirts", "NoOfpiece", c => c.Int(nullable: false));
            AddColumn("dbo.children", "NoOfCategory", c => c.Int(nullable: false));
            AddColumn("dbo.children", "cheldrenId", c => c.Int(nullable: false));
            AddColumn("dbo.children", "NoOfpiece", c => c.Int(nullable: false));
            AlterColumn("dbo.shirts", "photo", c => c.String(nullable: false));
            AlterColumn("dbo.children", "photo", c => c.String(nullable: false));
        }
    }
}
