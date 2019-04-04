using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{        //api/student
    public class StudentController : ApiController
    {
        private SchoolDbContext context = new SchoolDbContext();
        public IHttpActionResult Get()
        {
            return Ok(context.Students.GetAll());
        }
        public IHttpActionResult Get(int id)
        {
            var student = context.Students.GetById(id);
            if (student != null)
                return Ok(student);
            return NotFound();
        }
      
        public IHttpActionResult Post(Student std)
        {
            if (ModelState.IsValid)
            {
                if (context.Students.GetAll().Where(entity => entity.FacultyNumber == std.FacultyNumber).Count() >= 1) // if we already have record with that number
                    return BadRequest("Faculty number already exist.");
                // else => everything is ok.
                context.Students.Add(std);
                return Ok();
            }
            return BadRequest(ModelState);
        }

        public IHttpActionResult Delete(int id)
        {
            var entity = context.Students.GetAll().Where(std => std.FacultyNumber == id).SingleOrDefault(); // try to find the student with this id/number.
            if (entity == null) return NotFound();
            context.Students.Delete(entity);
            return Ok();
        }
    }
}
