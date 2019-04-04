using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public  class StudentDbSet
    {
        private static IList<Student> students;
        public StudentDbSet()
        {
            if (students==null)
            students = new List<Student>();
        }
        public  void Add(Student std)
        {
            if (std != null) // let's check for null value
                students.Add(std);
        }
        public  void Delete(Student std)
        {
            if (std != null)
                students.Remove(std);
        }
        public void DeleteAll()
        {
            if (students != null)
                students.Clear();
        }
        public void Update(Student std)
        {
            if (std != null)
            {
                var entity = students.Where(obj => obj == std).SingleOrDefault();
                entity = std;
            }
        }
        public Student GetById(int num)
        {
            Student std;
            if (students != null)
            {
                std = students.Where(obj => obj.FacultyNumber == num).FirstOrDefault();
                return std;
            }
            return null;
        }
        public IEnumerable<Student> GetAll()
        {
            if(students!=null)
            {
                return students;
            }
            return null;
        }
    }
}