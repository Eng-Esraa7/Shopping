namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkoutnameUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.checkOuts", "nameUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.checkOuts", "nameUser");
        }
    }
}
