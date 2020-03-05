using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineTermWorkSubmission.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Faculty> Faculties { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<Lab> Labs { get; set; }

        public virtual DbSet<Assignment> Assignments { get; set; }
        
        public virtual DbSet<Branch> Branches { get; set; }

        public virtual DbSet<Division> Divisions { get; set; }

        public virtual DbSet<Batch> Batches { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
    }
    public class Student
    {
        [Key]
        [Display(Name = "Id")]
        public int Student_Id { get; set; }
        [Required]
        [Display(Name = "Student Name")]
        public string Student_Name { get; set; }
        [Required]
        [Display(Name = "Branch")]
        public string Branch { get; set; }
        [Required]
        [Display(Name = "Division")]
        public string Division { get; set; }
        [Required]
        [Display(Name = "Batch")]
        public string Batch { get; set; }
        [Required]
        [Display(Name = "Roll No")]
        public int Roll_No { get; set; }
        [Required]
        [Display(Name = "Semester")]
        public int Semester { get; set; }
        [Required]
        [Display(Name = "Email Id")]
        public string Student_Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string Student_Contact { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime Student_Dob { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Student_Password { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }


    }

    public class Branch
    {
        [Key]
        public int Branch_Id { get; set; }
        public string Branch_Name { get; set; }

        public virtual ICollection<Division> Divisions { get; set; }
    }

    public class Division
    {
        [Key]
        public int Division_Id { get; set; }
        public string Division_Name { get; set; }

        public int Branch_Id { get; set; }
        [ForeignKey("Branch_Id")]
        public virtual Branch Branches { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
    }

    public class Batch
    {
        [Key]
        public int Batch_Id { get; set; }
        public string Batch_Name { get; set; }

        public int Division_Id { get; set; }
        [ForeignKey("Division_Id")]
        public virtual Division Divisions { get; set; }
    }

    
    public class Faculty
    {
        [Key]
        [Display(Name = "Id")]
        public int faculty_id { get; set; }
        [Required]
        [Display(Name = "Facluty Name")]
        public string faculty_name { get; set; }
        [Required]
        [Display(Name = "Email Id")]
        public string faculty_email { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string faculty_contact { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime faculty_dob { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string faculty_password { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

    }

    public class Subject
    {
        [Key]
        [Display(Name = "Id")]
        public int subject_id { get; set; }
        [Required]
        [Display(Name = "Subject Name")]
        public string subject_name { get; set; }

        [Required]
        [Display(Name = "Semester")]
        public int semester { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Lab> Labs { get; set; }
    }

    public class Lab
    {
        [Key]
        [Display(Name = "Id")]
        public int lab_id { get; set; }
        [Required]
        [Display(Name = "Lab No")]
        public int lab_no { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime lab_startdate { get; set; }


        public int subject_id { get; set; }
        [ForeignKey("subject_id")]
        public virtual Subject Subjects { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

    }

    public class Assignment
    {
        [Key]
        [Display(Name = "Id")]
        public int assignment_id { get; set; }

        [Required]
        [Display(Name = "Assignment No")]
        public int assignment_no { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string assignment_text { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime assignment_enddate { get; set; }

        public int lab_id { get; set; }
        [ForeignKey("lab_id")]
        public virtual Lab Labs { get; set; }
    }
}