namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class infoOfCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.infoOfCarts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        categoryId = c.Int(nullable: false),
                        nameCategory = c.String(),
                        price = c.Single(nullable: false),
                        noCategory = c.Int(nullable: false),
                        UserId = c.String(),
                        check = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.infoOfCarts");
        }
    }
}
