using System;
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
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        
        public int EnrollmentNUmber { get; set; }
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        
    }
}
