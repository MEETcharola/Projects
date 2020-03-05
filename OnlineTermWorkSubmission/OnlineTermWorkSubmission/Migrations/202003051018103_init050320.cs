namespace OnlineTermWorkSubmission.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init050320 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Semesters");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        Semester_Id = c.Int(nullable: false, identity: true),
                        Semester_No = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Semester_Id);
            
        }
    }
}
