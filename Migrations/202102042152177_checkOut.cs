namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkOut : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.checkOuts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        FullName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Total = c.Single(nullable: false),
                        finish = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.checkOuts");
        }
    }
}
