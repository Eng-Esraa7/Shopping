namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.pants", "photo", c => c.String());
            DropColumn("dbo.pants", "NoOfpiece");
            DropColumn("dbo.pants", "pantsId");
            DropColumn("dbo.pants", "NoOfCategory");
        }
        
        public override void Down()
        {
            AddColumn("dbo.pants", "NoOfCategory", c => c.Int(nullable: false));
            AddColumn("dbo.pants", "pantsId", c => c.Int(nullable: false));
            AddColumn("dbo.pants", "NoOfpiece", c => c.Int(nullable: false));
            AlterColumn("dbo.pants", "photo", c => c.String(nullable: false));
        }
    }
}
