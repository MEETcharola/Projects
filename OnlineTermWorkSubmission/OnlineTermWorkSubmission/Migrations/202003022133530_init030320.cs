namespace OnlineTermWorkSubmission.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init030320 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.Divisions", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Classes", new[] { "Branch_Id" });
            DropIndex("dbo.Divisions", new[] { "Class_Id" });
            CreateTable(
                "dbo.Batches",
                c => new
                    {
                        Batch_Id = c.Int(nullable: false, identity: true),
                        Batch_Name = c.String(),
                        Division_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Batch_Id)
                .ForeignKey("dbo.Divisions", t => t.Division_Id, cascadeDelete: true)
                .Index(t => t.Division_Id);
            
            AddColumn("dbo.Students", "Batch", c => c.String(nullable: false));
            AddColumn("dbo.Students", "Roll_No", c => c.Int(nullable: false));
            AddColumn("dbo.Divisions", "Branch_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Students", "Semester", c => c.Int(nullable: false));
            CreateIndex("dbo.Divisions", "Branch_Id");
            AddForeignKey("dbo.Divisions", "Branch_Id", "dbo.Branches", "Branch_Id", cascadeDelete: true);
            DropColumn("dbo.Students", "Class");
            DropColumn("dbo.Students", "Student_Address");
            DropColumn("dbo.Divisions", "Class_Id");
            DropTable("dbo.Classes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Class_Id = c.Int(nullable: false, identity: true),
                        Class_Name = c.String(),
                        Branch_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Class_Id);
            
            AddColumn("dbo.Divisions", "Class_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Students", "Student_Address", c => c.String(nullable: false));
            AddColumn("dbo.Students", "Class", c => c.String(nullable: false));
            DropForeignKey("dbo.Divisions", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.Batches", "Division_Id", "dbo.Divisions");
            DropIndex("dbo.Divisions", new[] { "Branch_Id" });
            DropIndex("dbo.Batches", new[] { "Division_Id" });
            AlterColumn("dbo.Students", "Semester", c => c.String(nullable: false));
            DropColumn("dbo.Divisions", "Branch_Id");
            DropColumn("dbo.Students", "Roll_No");
            DropColumn("dbo.Students", "Batch");
            DropTable("dbo.Batches");
            CreateIndex("dbo.Divisions", "Class_Id");
            CreateIndex("dbo.Classes", "Branch_Id");
            AddForeignKey("dbo.Divisions", "Class_Id", "dbo.Classes", "Class_Id", cascadeDelete: true);
            AddForeignKey("dbo.Classes", "Branch_Id", "dbo.Branches", "Branch_Id", cascadeDelete: true);
        }
    }
}
