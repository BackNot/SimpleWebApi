using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiProject.Models;
using WebApiProject.BasicAuth;
namespace WebApiProject.Controllers
{// api/teacher
    [BasicAuthentication]
    public class TeacherController : ApiController
    {
        private SchoolDbContext context = new SchoolDbContext();
        public IHttpActionResult Get()
        {
            return Ok(context.Teachers.GetAll());
        }
        public IHttpActionResult Get(int id)
        {
            var teacher = context.Teachers.GetById(id);
            if (teacher != null)
                return Ok(teacher);
            return NotFound();
        }
        public IHttpActionResult Post(Teacher tch)
        {
            if (ModelState.IsValid)
            {
                if (context.Teachers.GetAll().Where(entity => entity.TeacherNumber == tch.TeacherNumber).Count() >= 1) // if we already have record with that number
                    return BadRequest("Teacher number already exist.");
                // else => everything is ok.
                context.Teachers.Add(tch);
                return Ok();
            }
            return BadRequest(ModelState);
        }
        public IHttpActionResult Delete(int id)
        {
            var entity = context.Teachers.GetAll().Where(tch => tch.TeacherNumber == id).SingleOrDefault(); // try to find the teacher with this id/number.
            if (entity == null) return NotFound();
            context.Teachers.Delete(entity);
            return Ok();
        }
    }
}
