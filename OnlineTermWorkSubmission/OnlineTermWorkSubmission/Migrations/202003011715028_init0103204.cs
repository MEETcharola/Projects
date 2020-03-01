namespace OnlineTermWorkSubmission.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init0103204 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "Branch_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Divisions", "Class_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Classes", "Branch_Id");
            CreateIndex("dbo.Divisions", "Class_Id");
            AddForeignKey("dbo.Classes", "Branch_Id", "dbo.Branches", "Branch_Id", cascadeDelete: true);
            AddForeignKey("dbo.Divisions", "Class_Id", "dbo.Classes", "Class_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Divisions", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Classes", "Branch_Id", "dbo.Branches");
            DropIndex("dbo.Divisions", new[] { "Class_Id" });
            DropIndex("dbo.Classes", new[] { "Branch_Id" });
            DropColumn("dbo.Divisions", "Class_Id");
            DropColumn("dbo.Classes", "Branch_Id");
        }
    }
}
