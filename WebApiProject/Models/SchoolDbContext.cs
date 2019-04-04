using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public class SchoolDbContext
    { // We won't use MSSQL Database and ORM in this project. This will be our context with the entities.

        public  StudentDbSet Students { get; set; }
        public TeacherDbSet Teachers { get; set; }

        public SchoolDbContext()
        { // initialize our entities
            this.Students = new StudentDbSet();
            this.Teachers = new TeacherDbSet();
            if (Students.GetAll().Count()==0 || Teachers.GetAll().Count()==0) // This is test environment and we will need default data to work with.
                SeedDatabase();                 // If we don't have students/teachers added populate it with def. records
        }

        public void SeedDatabase() // this is our Seed method (like in Entity Framework)
        {
            if (Students != null)
            {
                Students.DeleteAll(); // clear it from past records

                Students.Add(new Student() { FacultyNumber = 101, FirstName = "John", LastName = "Johnson", Discipline = "Mathematics" });
                Students.Add(new Student() { FacultyNumber = 201, FirstName = "Jessica", LastName = "Patrick", Discipline = "Informatics"});
                Students.Add(new Student() { FacultyNumber = 102, FirstName = "Melissa", LastName = "Patrick", Discipline = "Mathematics" });
                Students.Add(new Student() { FacultyNumber = 202, FirstName = "Greg", LastName = "Brown", Discipline = "Informatics"});
                // done
            }
            if (this.Teachers != null)
            {
                this.Teachers.DeleteAll(); // clear it from past records

                this.Teachers.Add(new Teacher() { TeacherNumber=1001, FirstName = "Lucas ", LastName = "Puckett", Discipline = "Mathematics" });
                this.Teachers.Add(new Teacher() { TeacherNumber=1002, FirstName = "Hermione  ", LastName = "Cowan", Discipline = "Mathematics" });
                this.Teachers.Add(new Teacher() { TeacherNumber=1003, FirstName = "Rickie   ", LastName = "Ramsay", Discipline = "Informatics" });
                // done
            }

        }
    }
}