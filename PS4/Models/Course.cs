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
Course controller.

*/
namespace PS4.Models
{
    public enum Semester
    {
        Spring, Summer, Fall
    }
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Course ID")]
        public int CourseID { get; set; }
        [Required]
        [Display(Name = "Course Title")]
        public string Title { get; set; }
        [Required]
        public int Credits { get; set; }



        [Required]
        public Semester Semester{get; set;}
        [Required]
        public int Year { get; set; }
        [Required]
        public string Department { get; set; }

        [Required]
        public int Section { get; set; }
        [Required]
        public string Description { set; get; }
        [Required]
        [Display(Name = "University ID", ShortName = "Unid", Prompt = "u1234567")]
        [RegularExpression(@"^(u{1}[0-9]+)", ErrorMessage = "A uID should start with u and number. e.g. u1234567")]
        public string Pid { set; get; }
        [Required]
        [Display(Name = "Professor Name", ShortName = "Pro_Name", Prompt = "Name")]
        public string PName { set; get; }
        [Required]
        [Display(Name = "Time and Days offered",  Prompt = "M 1:00-3:00")]
        public string Time { get; set; }
        [Required]
        public string Location { get; set; }

        public string? Note { get; set; }
        public ICollection<Application> Application { get; set; }
        public ICollection<TimeSchedule> TimeSchedule { get; set; }
    }
}
