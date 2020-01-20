namespace OnlineTermWorkSubmission.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Faculties", "faculty_contact", c => c.String(nullable: false));
            AlterColumn("dbo.Students", "student_contact", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "student_contact", c => c.Int(nullable: false));
            AlterColumn("dbo.Faculties", "faculty_contact", c => c.Int(nullable: false));
        }
    }
}
