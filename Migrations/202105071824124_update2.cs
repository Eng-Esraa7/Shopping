namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.shorts", "photo", c => c.String());
            AlterColumn("dbo.suits", "photo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.suits", "photo", c => c.String(nullable: false));
            AlterColumn("dbo.shorts", "photo", c => c.String(nullable: false));
        }
    }
}
