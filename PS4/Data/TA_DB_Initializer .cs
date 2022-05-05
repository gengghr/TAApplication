using Microsoft.AspNetCore.Identity;
using PS4.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using PS4.Areas.Identity.Data;

using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace PS4.Data
{
    public static class DBInitializer
    {
        public static void Initialize(TAAPPContext context, UserManager<TAUser> um, RoleManager<IdentityRole> rm)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Applicant.Any())
            {
                return;   // DB has been seeded
            }
            var u1 =  um.Users.FirstOrDefault(x => x.uID == "u0000000");
            var u2 = um.Users.FirstOrDefault(x => x.uID == "u0000002");

            var students = new Applicant[]
            {
            new Applicant{ Address="500S 43E", uID ="u0000000",Phone="1234567890",Email="u0000000@utah.edu",UserId=u1.Id, CurrentDegree=Degree.BS,CurrentProgram="CS",GPA=4.0, WorkHours=20,EnglishFluency=EnglishFluency.Native ,Semesters=4,CreationDate=DateTime.Parse("2005-09-01"),  FirstName ="Carson",LastName="Alexander"},
            new Applicant{Address="70N 63E",uID="u0000002",Phone="1234567890", Email="u0000002@utah.edu", UserId=u2.Id,CurrentDegree=Degree.BS,CurrentProgram="CS",GPA=3.0, WorkHours=10,EnglishFluency=EnglishFluency.Adequate ,Semesters=4,CreationDate=DateTime.Parse("2002-09-01"),FirstName="Meredith",LastName="Alonso"},
            new Applicant{Address="1254 State St",uID="u2244567",Phone="1234567890",Email="u2244567@utah.edu", CurrentDegree=Degree.PHD,CurrentProgram="CS",GPA=3.5, WorkHours=20,EnglishFluency=EnglishFluency.Native ,Semesters=4,CreationDate=DateTime.Parse("2003-09-01"),FirstName="Arturo",LastName="Anand"},
            new Applicant{uID="u5684567",Phone="1234567890", Email="u5684567@utah.edu",CurrentDegree=Degree.BS,CurrentProgram="ART",GPA=3.2, WorkHours=10,EnglishFluency=EnglishFluency.Native ,Semesters=4,CreationDate=DateTime.Parse("2002-09-01"),FirstName="Gytis",LastName="Barzdukas"},
            new Applicant{uID="u7845697",Phone="1234567890", Email="u7845697@utah.edu",CurrentDegree=Degree.MS,CurrentProgram="CS",GPA=3.8, WorkHours=20,EnglishFluency=EnglishFluency.None ,Semesters=4,CreationDate=DateTime.Parse("2002-09-01"),FirstName="Yan",LastName="Li"},
            new Applicant{uID="u0564467",Phone="1234567890", Email="u0564467@utah.edu",CurrentDegree=Degree.MS,CurrentProgram="ME",GPA=3.4, WorkHours=20,EnglishFluency=EnglishFluency.Fluent ,Semesters=4,CreationDate=DateTime.Parse("2001-09-01"),FirstName="Peggy",LastName="Justice"},
            new Applicant{uID="u1846597",Phone="1234567890", Email="u1846597@utah.edu",CurrentDegree=Degree.BS,CurrentProgram="CS",GPA=2.8, WorkHours=20,EnglishFluency=EnglishFluency.Poor ,Semesters=4,CreationDate=DateTime.Parse("2003-09-01"),FirstName="Laura",LastName="Norman"},
            new Applicant{uID="u0235867",Phone="1234567890",Email="u0235867@utah.edu", CurrentDegree=Degree.MS,CurrentProgram="CE",GPA=4.0, WorkHours=20,EnglishFluency=EnglishFluency.Native ,Semesters=4,CreationDate=DateTime.Parse("2005-09-01"),FirstName="Nino",LastName="Olivetto"}
            };
            foreach (Applicant s in students)
            {
                context.Applicant.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3,Semester = Semester.Spring, Year= 2020,Department="CHEM", Section=001, Description="A Universtiy Course", Pid="u9000001", PName="p1", Time="M/W 1:00-3:00", Location="building 1" },
            new Course{CourseID=4022,Title="Microeconomics",Credits=3,Semester = Semester.Fall, Year= 2020,Department="ECON", Section=001, Description="A Universtiy Course", Pid="u9000002", PName="p2", Time="M/W 2:00-3:00", Location="building 2"},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3,Semester = Semester.Fall, Year= 2020,Department="ECON", Section=001, Description="A Universtiy Course", Pid="u9000003", PName="p3", Time="Tu/Th 1:00-3:00", Location="building 3"},
            new Course{CourseID=1045,Title="Calculus",Credits=4,Semester = Semester.Spring, Year= 2020,Department="MATH", Section=001, Description="A Universtiy Course", Pid="u9000004", PName="p4", Time="M/W 3:00-5:00", Location="building 1",Note="Need more TAs"},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4,Semester = Semester.Fall, Year= 2020,Department="MATH", Section=001, Description="A Universtiy Course", Pid="u9000005", PName="p5", Time="Tu/Th 11:00-12:00", Location="building 5"},
            new Course{CourseID=2021,Title="Composition",Credits=3,Semester = Semester.Summer, Year= 2020,Department="WRTG", Section=001, Description="A Universtiy Course", Pid="u9000006", PName="p6", Time="M/W 10:00-11:00", Location="building 6"},
            new Course{CourseID=2042,Title="Literature",Credits=4,Semester = Semester.Spring, Year= 2020,Department="WRTG", Section=001, Description="A Universtiy Course", Pid="u9000007", PName="p7", Time="Tu/Th 1:00-3:00", Location="building 7"}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Application[]
            {
            new Application{ApplicantID=1,CourseID=1050},
            new Application{ApplicantID=1,CourseID=4022},
            new Application{ApplicantID=1,CourseID=4041},
            new Application{ApplicantID=2,CourseID=1045},
            new Application{ApplicantID=2,CourseID=3141},
            new Application{ApplicantID=2,CourseID=2021},
            new Application{ApplicantID=3,CourseID=1050},
            new Application{ApplicantID=4,CourseID=1050},
            new Application{ApplicantID=4,CourseID=4022},
            new Application{ApplicantID=5,CourseID=4041},
            new Application{ApplicantID=6,CourseID=1045},
            new Application{ApplicantID=7,CourseID=3141},
            };
            foreach (Application e in enrollments)
            {
                context.Application.Add(e);
            }
            context.SaveChanges();


            var ts = new TimeSchedule[]
            {
                new TimeSchedule{CourseID=1050,startTime= new DateTime(2021,12,01,15,15,0), endTime=new DateTime(2021,12,01,16,30,0),isOpen=true,ApplicantID=1  },
                new TimeSchedule{CourseID=1050,startTime= new DateTime(2021,12,02,9,15,0), endTime=new DateTime(2021,12,02,11,30,0),isOpen=true,ApplicantID=1  }
            };
            foreach (TimeSchedule t in ts)
            {
                context.TimeSchedule.Add(t);
            }
            context.SaveChanges();


            List<Enrollment> er = new List<Enrollment>();
            //string FilePath = "C:/Users/Haoran/source/repos/PS4/PS4/wwwroot/temp.csv";
            //var Lines = File.ReadLines("./wwwroot/Temp.csv").Select(a => a.Split(';'));
            //foreach(var l in Lines)
            //{
            //    var line = l.Select(a => a.Split(','));


            //}




                StreamReader reader = new StreamReader("./wwwroot/temp.csv");
            
                int n = 0;
                List<string> header = new List<string>();
                while (!reader.EndOfStream)
                {
                    int i = 0;

                    //int j = 0;
                    //Process row
                    string line = reader.ReadLine();
                    var fields = line.Split(',');
                    string className = null;
                    foreach (string field in fields)
                    {
                        //TODO: Process field
                        if (n == 0)
                        {
                            header.Add(field);
                        }
                        else
                        {
                            if (i == 0)
                            {
                                className = field;
                            }
                            else
                            {
                                er.Add(new Enrollment { CourseName = className, EnrollmentNUmber = Int32.Parse(field), EnrollmentDate = DateTime.Parse(header[i], CultureInfo.InvariantCulture) });
                                
                            }
                            i++;
                        }
                    }
                    n++;
                }
            reader.Close();
            

            

            foreach (Enrollment e in er)
            {
               context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }

   
}