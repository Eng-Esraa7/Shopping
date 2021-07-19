namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.shoes", "Size", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.shoes", "Size", c => c.String(nullable: false));
        }
    }
}
