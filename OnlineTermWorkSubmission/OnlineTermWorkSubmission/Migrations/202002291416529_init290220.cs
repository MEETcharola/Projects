namespace OnlineTermWorkSubmission.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init290220 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.StudentSubjects", new[] { "Student_student_id" });
            AddColumn("dbo.Students", "Branch", c => c.String(nullable: false));
            AddColumn("dbo.Students", "Semester", c => c.String(nullable: false));
            AddColumn("dbo.Students", "Class", c => c.String(nullable: false));
            AddColumn("dbo.Students", "Division", c => c.String(nullable: false));
            CreateIndex("dbo.StudentSubjects", "Student_Student_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.StudentSubjects", new[] { "Student_Student_Id" });
            DropColumn("dbo.Students", "Division");
            DropColumn("dbo.Students", "Class");
            DropColumn("dbo.Students", "Semester");
            DropColumn("dbo.Students", "Branch");
            CreateIndex("dbo.StudentSubjects", "Student_student_id");
        }
    }
}
