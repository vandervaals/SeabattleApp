namespace Seabattle.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AreaDb_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaDb_Id)
                .Index(t => t.AreaDb_Id);
            
            CreateTable(
                "dbo.CellDbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        IsAlive = c.Boolean(nullable: false),
                        ShipDb_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ships", t => t.ShipDb_Id)
                .Index(t => t.ShipDb_Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConnectionId = c.String(),
                        ComputerArea_Id = c.Int(),
                        UserArea_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.ComputerArea_Id)
                .ForeignKey("dbo.Areas", t => t.UserArea_Id)
                .Index(t => t.ComputerArea_Id)
                .Index(t => t.UserArea_Id);
            
            CreateTable(
                "dbo.ShotDbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        GameDb_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameDb_Id)
                .Index(t => t.GameDb_Id);
            
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
            DropForeignKey("dbo.Games", "UserArea_Id", "dbo.Areas");
            DropForeignKey("dbo.ShotDbs", "GameDb_Id", "dbo.Games");
            DropForeignKey("dbo.Games", "ComputerArea_Id", "dbo.Areas");
            DropForeignKey("dbo.Ships", "AreaDb_Id", "dbo.Areas");
            DropForeignKey("dbo.CellDbs", "ShipDb_Id", "dbo.Ships");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ShotDbs", new[] { "GameDb_Id" });
            DropIndex("dbo.Games", new[] { "UserArea_Id" });
            DropIndex("dbo.Games", new[] { "ComputerArea_Id" });
            DropIndex("dbo.CellDbs", new[] { "ShipDb_Id" });
            DropIndex("dbo.Ships", new[] { "AreaDb_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ShotDbs");
            DropTable("dbo.Games");
            DropTable("dbo.CellDbs");
            DropTable("dbo.Ships");
            DropTable("dbo.Areas");
        }
    }
}
