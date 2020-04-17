namespace BikeDekho_CRUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bikes",
                c => new
                    {
                        BikeId = c.Int(nullable: false, identity: true),
                        BikeModel = c.String(nullable: false),
                        BikeCompany = c.String(nullable: false),
                        BikeDetails = c.String(nullable: false),
                        BikePhoto = c.Binary(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.BikeId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        UserEmail = c.String(nullable: false),
                        UserPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bikes", "User_UserId", "dbo.Users");
            DropIndex("dbo.Bikes", new[] { "User_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Bikes");
        }
    }
}
