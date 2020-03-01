namespace OnlineTermWorkSubmission.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init0103201 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Branch_Id = c.Int(nullable: false, identity: true),
                        Branch_Name = c.String(),
                    })
                .PrimaryKey(t => t.Branch_Id);
            
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Class_Id = c.Int(nullable: false, identity: true),
                        Class_Name = c.String(),
                    })
                .PrimaryKey(t => t.Class_Id);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Division_Id = c.Int(nullable: false, identity: true),
                        Division_Name = c.String(),
                    })
                .PrimaryKey(t => t.Division_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Divisions");
            DropTable("dbo.Classes");
            DropTable("dbo.Branches");
        }
    }
}
