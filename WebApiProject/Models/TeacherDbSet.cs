using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiProject.Models
{
    public  class TeacherDbSet
    {
        private static IList<Teacher> teachers = null;
        public TeacherDbSet()
        {
            if (teachers == null)
                teachers = new List<Teacher>();
        }
        public  void Add(Teacher tch)
        {
            if (tch != null) // let's check for null value
                teachers.Add(tch);
        }
        public  void Delete(Teacher tch)
        {
            if (tch != null)
                teachers.Remove(tch);
        }
        public void DeleteAll()
        {
            if (teachers != null)
                teachers.Clear();
        }
        public  void Update(Teacher tch)
        {
            if (tch != null)
            {
                var entity = teachers.Where(obj => obj == tch).SingleOrDefault();
                entity = tch;
            }
        }
        public Teacher GetById(int num)
        {
            Teacher tch;
            if (teachers != null)
            {
                tch = teachers.Where(obj => obj.TeacherNumber == num).FirstOrDefault();
                return tch;
            }
            return null;
        }
        public IEnumerable<Teacher> GetAll()
        {
            if (teachers != null)
            {
                return teachers;
            }
            return null;
        }
    }
}