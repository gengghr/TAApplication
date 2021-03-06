using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


/*
Author: Haoran Geng
Partner:   None
Date:      9 / 30 / 2021
Course:    CS 4540, University of Utah, School of Computing
Copyright: CS 4540 and Haoran Geng - This work may not be copied for use in Academic Coursework.


File Contents:
Applicantion connect Applicate and Course tables 
*/
namespace PS4.Models
{
    public class Application
    {
        [Key]
        public int ApplicationID { get; set; }

        [Display(Name = "Course ID")]
        public int CourseID { get; set; }

        [Display(Name = "Applicant ID")]
        public int ApplicantID { get; set; }

        public Applicant Applicant { get; set; }
        public Course Course { get; set; }
    }
}
