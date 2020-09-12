namespace CriminalProject.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CrimeHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        FkCrimeTypeId = c.Int(nullable: false),
                        FkWeaponId = c.Int(nullable: false),
                        FkOfficerId = c.String(nullable: false, maxLength: 128),
                        Area = c.String(),
                        FkCityId = c.Int(nullable: false),
                        ResourceUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.FkCityId, cascadeDelete: true)
                .ForeignKey("dbo.CrimeType", t => t.FkCrimeTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.FkOfficerId, cascadeDelete: true)
                .ForeignKey("dbo.Weapon", t => t.FkWeaponId, cascadeDelete: true)
                .Index(t => t.FkCrimeTypeId)
                .Index(t => t.FkWeaponId)
                .Index(t => t.FkOfficerId)
                .Index(t => t.FkCityId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CrimeType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Suspect",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Birthdate = c.DateTime(nullable: false),
                        Address = c.String(),
                        FkSuspectPictureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Url = c.String(),
                        FkSuspectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suspect", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Weapon",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.SuspectCrime",
                c => new
                    {
                        FkSuspectId = c.Int(nullable: false),
                        FkCommittedCrimeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FkSuspectId, t.FkCommittedCrimeId })
                .ForeignKey("dbo.CrimeHistory", t => t.FkSuspectId, cascadeDelete: true)
                .ForeignKey("dbo.Suspect", t => t.FkCommittedCrimeId, cascadeDelete: true)
                .Index(t => t.FkSuspectId)
                .Index(t => t.FkCommittedCrimeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CrimeHistory", "FkWeaponId", "dbo.Weapon");
            DropForeignKey("dbo.SuspectCrime", "FkCommittedCrimeId", "dbo.Suspect");
            DropForeignKey("dbo.SuspectCrime", "FkSuspectId", "dbo.CrimeHistory");
            DropForeignKey("dbo.Image", "Id", "dbo.Suspect");
            DropForeignKey("dbo.CrimeHistory", "FkOfficerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CrimeHistory", "FkCrimeTypeId", "dbo.CrimeType");
            DropForeignKey("dbo.CrimeHistory", "FkCityId", "dbo.City");
            DropIndex("dbo.SuspectCrime", new[] { "FkCommittedCrimeId" });
            DropIndex("dbo.SuspectCrime", new[] { "FkSuspectId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Image", new[] { "Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.CrimeHistory", new[] { "FkCityId" });
            DropIndex("dbo.CrimeHistory", new[] { "FkOfficerId" });
            DropIndex("dbo.CrimeHistory", new[] { "FkWeaponId" });
            DropIndex("dbo.CrimeHistory", new[] { "FkCrimeTypeId" });
            DropTable("dbo.SuspectCrime");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Weapon");
            DropTable("dbo.Image");
            DropTable("dbo.Suspect");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.CrimeType");
            DropTable("dbo.City");
            DropTable("dbo.CrimeHistory");
        }
    }
}
