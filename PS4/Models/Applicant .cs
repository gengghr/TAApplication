using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/*
Author: Haoran Geng
Partner:   None
Date:      9 / 30 / 2021
Course:    CS 4540, University of Utah, School of Computing
Copyright: CS 4540 and Haoran Geng - This work may not be copied for use in Academic Coursework.


File Contents:
Applicant controller for TAs 
*/

namespace PS4.Models
{
    public enum EnglishFluency
    {
         Native, Fluent, Adequate, Poor, None
    }

    public enum Degree
    {
        BS,MS,PHD
    }
    public class Applicant
    {
        [Key]
        public int ID { get; set; }//the Applicant ID
        [Required]
        [Display(Name = "University ID", ShortName ="Unid", Prompt ="u1234567")]
        [RegularExpression(@"^(u{1}[0-9]+)", ErrorMessage = "A uID should start with u and number. e.g. u1234567")]
        public string uID { get; set; }//University ID

        [ScaffoldColumn(false)]
        public string? UserId { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "u1234567@utah.edu")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage ="your name is too long")]
        [Display(Name = "Last Name" , Prompt = "LastN" )]
        public string LastName { get; set; }

        [Required]
        [StringLength(50,ErrorMessage = "your name is too long")]
        [Display(Name = "First Name", Prompt = "FirstN")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number", Prompt = "1234567890")]
        public string Phone { get; set; }

        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "Phone Number", Prompt = "1234567890")]
        //public string email { get; set; }

        public string? Address { get; set; }

        [Required]
        [Display(Name = "Current Degree", Prompt = "BS")]
        public Degree CurrentDegree { get; set; }

        [Required]
        [Display(Name = "Current Program", Prompt = "CS")]
        public string CurrentProgram { get; set; }

        [Required]
        [Display( Prompt = "4.0")]
        [Range(0,4)]
        public double GPA { get; set; }

        [Required]
        [Display(Name = "Work Hours", Prompt = "20")]
        public int WorkHours { get; set; }

        [Display(Name = "Personal Statement")]
        public string? Statement { get; set; }

        [Required]
        [Display(Name = "English Fluency", Prompt = "None")]
        public EnglishFluency EnglishFluency { get; set; }

        [Required]
        [Display(Name = "Semesters completed at U",Prompt = "3")]
        public int Semesters { get; set; }

        public string? LinkedIn { get; set; }

        public string? Resume { get; set; }

        [Required]
        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Modification Date")]
        [ScaffoldColumn(false)]
        public DateTime? ModificationDate { get; set; }
        public ICollection<Application> Application { get; set; }
        public ICollection<TimeSchedule> TimeSchedule { get; set; }
    }
}
