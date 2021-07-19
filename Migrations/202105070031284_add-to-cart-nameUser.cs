namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtocartnameUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.infoOfCarts", "nameUser", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.infoOfCarts", "nameUser");
        }
    }
}
