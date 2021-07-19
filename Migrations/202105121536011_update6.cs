namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.shoes", "photo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.shoes", "photo", c => c.String(nullable: false));
        }
    }
}
