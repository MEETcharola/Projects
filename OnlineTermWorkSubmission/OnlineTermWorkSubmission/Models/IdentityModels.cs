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


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
    }
    public class Student
    {
        [Key]
        public int student_id { get; set; }
        [Required]
        public string student_name { get; set; }
        [Required]
        public string student_email { get; set; }
        [Required]
        public string student_address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string student_contact { get; set; }
        [Required]
        public DateTime student_dob { get; set; }
        [Required]
        public string student_password { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }


    }

    public class Faculty
    {
        [Key]
        public int faculty_id { get; set; }
        [Required]
        public string faculty_name { get; set; }
        [Required]
        public string faculty_email { get; set; }
        [Required]

        [DataType(DataType.PhoneNumber)]
        public string faculty_contact { get; set; }
        [Required]
        public DateTime faculty_dob { get; set; }
        [Required]
        public string faculty_password { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

    }

    public class Subject
    {
        [Key]
        public int subject_id { get; set; }
        [Required]
        public string subject_name { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Lab> Labs { get; set; }
    }

    public class Lab
    {
        [Key]
        public int lab_id { get; set; }
        [Required]
        public int lab_no { get; set; }
        [Required]
        public DateTime lab_startdate { get; set; }


        public int subject_id { get; set; }
        [ForeignKey("subject_id")]
        public virtual Subject Subjects { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

    }

    public class Assignment
    {
        [Key]
        public int assignment_id { get; set; }
        [Required]
        public string assignment_text { get; set; }


        public int lab_id { get; set; }
        [ForeignKey("lab_id")]
        public virtual Lab Labs { get; set; }
    }
}