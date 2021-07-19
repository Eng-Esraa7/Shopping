namespace Shopping.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.accessories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photo = c.String(nullable: false),
                        oldPrice = c.Single(nullable: false),
                        newPrice = c.Single(nullable: false),
                        description = c.String(),
                        NoOfpiece = c.Int(nullable: false),
                        Bagid = c.Int(nullable: false),
                        Accessid = c.Int(nullable: false),
                        Watchid = c.Int(nullable: false),
                        NoOfCategory = c.Int(nullable: false),
                        AddToCart = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.children",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photo = c.String(nullable: false),
                        oldPrice = c.Single(nullable: false),
                        newPrice = c.Single(nullable: false),
                        description = c.String(nullable: false),
                        NoOfpiece = c.Int(nullable: false),
                        cheldrenId = c.Int(nullable: false),
                        NoOfCategory = c.Int(nullable: false),
                        AddToCart = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Dresses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photo = c.String(nullable: false),
                        oldPrice = c.Single(nullable: false),
                        newPrice = c.Single(nullable: false),
                        description = c.String(nullable: false),
                        NoOfpiece = c.Int(nullable: false),
                        DressId = c.Int(nullable: false),
                        NoOfCategory = c.Int(nullable: false),
                        AddToCart = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.pants",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photo = c.String(nullable: false),
                        oldPrice = c.Single(nullable: false),
                        newPrice = c.Single(nullable: false),
                        description = c.String(nullable: false),
                        NoOfpiece = c.Int(nullable: false),
                        pantsId = c.Int(nullable: false),
                        NoOfCategory = c.Int(nullable: false),
                        AddToCart = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.shirts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photo = c.String(nullable: false),
                        oldPrice = c.Single(nullable: false),
                        newPrice = c.Single(nullable: false),
                        description = c.String(nullable: false),
                        NoOfpiece = c.Int(nullable: false),
                        shirtsId = c.Int(nullable: false),
                        NoOfCategory = c.Int(nullable: false),
                        AddToCart = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.shoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photo = c.String(nullable: false),
                        oldPrice = c.Single(nullable: false),
                        newPrice = c.Single(nullable: false),
                        color = c.String(),
                        Size = c.String(nullable: false),
                        description = c.String(nullable: false),
                        Girlsid = c.Int(nullable: false),
                        Manid = c.Int(nullable: false),
                        Mixid = c.Int(nullable: false),
                        NoOfCategory = c.Int(nullable: false),
                        AddToCart = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.shorts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photo = c.String(nullable: false),
                        oldPrice = c.Single(nullable: false),
                        newPrice = c.Single(nullable: false),
                        description = c.String(nullable: false),
                        NoOfpiece = c.Int(nullable: false),
                        shortId = c.Int(nullable: false),
                        NoOfCategory = c.Int(nullable: false),
                        AddToCart = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.suits",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        photo = c.String(nullable: false),
                        oldPrice = c.Single(nullable: false),
                        newPrice = c.Single(nullable: false),
                        description = c.String(nullable: false),
                        NoOfpiece = c.Int(nullable: false),
                        suitId = c.Int(nullable: false),
                        NoOfCategory = c.Int(nullable: false),
                        AddToCart = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.suits");
            DropTable("dbo.shorts");
            DropTable("dbo.shoes");
            DropTable("dbo.shirts");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.pants");
            DropTable("dbo.Dresses");
            DropTable("dbo.children");
            DropTable("dbo.accessories");
        }
    }
}
