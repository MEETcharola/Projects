
namespace OnlineTermWorkSubmission.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assignments",
                c => new
                    {
                        assignment_id = c.Int(nullable: false, identity: true),
                        assignment_text = c.String(nullable: false),
                        lab_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.assignment_id)
                .ForeignKey("dbo.Labs", t => t.lab_id, cascadeDelete: true)
                .Index(t => t.lab_id);
            
            CreateTable(
                "dbo.Labs",
                c => new
                    {
                        lab_id = c.Int(nullable: false, identity: true),
                        lab_no = c.Int(nullable: false),
                        lab_startdate = c.DateTime(nullable: false),
                        subject_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.lab_id)
                .ForeignKey("dbo.Subjects", t => t.subject_id, cascadeDelete: true)
                .Index(t => t.subject_id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        subject_id = c.Int(nullable: false, identity: true),
                        subject_name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.subject_id);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        faculty_id = c.Int(nullable: false, identity: true),
                        faculty_name = c.String(nullable: false),
                        faculty_email = c.String(nullable: false),
                        faculty_contact = c.Int(nullable: false),
                        faculty_dob = c.DateTime(nullable: false),
                        faculty_password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.faculty_id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        student_id = c.Int(nullable: false, identity: true),
                        student_name = c.String(nullable: false),
                        student_email = c.String(nullable: false),
                        student_address = c.String(nullable: false),
                        student_contact = c.Int(nullable: false),
                        student_dob = c.DateTime(nullable: false),
                        student_password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.student_id);
            
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
            
            CreateTable(
                "dbo.FacultySubjects",
                c => new
                    {
                        Faculty_faculty_id = c.Int(nullable: false),
                        Subject_subject_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Faculty_faculty_id, t.Subject_subject_id })
                .ForeignKey("dbo.Faculties", t => t.Faculty_faculty_id, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_subject_id, cascadeDelete: true)
                .Index(t => t.Faculty_faculty_id)
                .Index(t => t.Subject_subject_id);
            
            CreateTable(
                "dbo.StudentSubjects",
                c => new
                    {
                        Student_student_id = c.Int(nullable: false),
                        Subject_subject_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Student_student_id, t.Subject_subject_id })
                .ForeignKey("dbo.Students", t => t.Student_student_id, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_subject_id, cascadeDelete: true)
                .Index(t => t.Student_student_id)
                .Index(t => t.Subject_subject_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.StudentSubjects", "Subject_subject_id", "dbo.Subjects");
            DropForeignKey("dbo.StudentSubjects", "Student_student_id", "dbo.Students");
            DropForeignKey("dbo.Labs", "subject_id", "dbo.Subjects");
            DropForeignKey("dbo.FacultySubjects", "Subject_subject_id", "dbo.Subjects");
            DropForeignKey("dbo.FacultySubjects", "Faculty_faculty_id", "dbo.Faculties");
            DropForeignKey("dbo.Assignments", "lab_id", "dbo.Labs");
            DropIndex("dbo.StudentSubjects", new[] { "Subject_subject_id" });
            DropIndex("dbo.StudentSubjects", new[] { "Student_student_id" });
            DropIndex("dbo.FacultySubjects", new[] { "Subject_subject_id" });
            DropIndex("dbo.FacultySubjects", new[] { "Faculty_faculty_id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Labs", new[] { "subject_id" });
            DropIndex("dbo.Assignments", new[] { "lab_id" });
            DropTable("dbo.StudentSubjects");
            DropTable("dbo.FacultySubjects");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Students");
            DropTable("dbo.Faculties");
            DropTable("dbo.Subjects");
            DropTable("dbo.Labs");
            DropTable("dbo.Assignments");
        }
    }
}
