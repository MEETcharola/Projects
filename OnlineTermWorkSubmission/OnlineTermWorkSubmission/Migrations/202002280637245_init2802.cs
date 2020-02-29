namespace OnlineTermWorkSubmission.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2802 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assignments", "assignment_no", c => c.Int(nullable: false));
            AddColumn("dbo.Assignments", "assignment_enddate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Subjects", "semester", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "semester");
            DropColumn("dbo.Assignments", "assignment_enddate");
            DropColumn("dbo.Assignments", "assignment_no");
        }
    }
}
