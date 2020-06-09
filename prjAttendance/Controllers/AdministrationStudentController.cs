using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using prjAttendance.Models;

namespace prjAttendance.Controllers
{
    public class AdministrationStudentController : ApiController
    {
        private Model db = new Model();

        [Route("api/administration/records/getstudents")]
        [HttpGet]
        public IHttpActionResult GetStudents(int ClassId)
        {
            var result=db.Students.Where(x=>x.ClasssId== ClassId).Select(x => new
            {
                StudentId=x.Id,
                x.Name
            }).OrderBy(x => x.StudentId);
            return Ok(new
            {
                code = 1,
                data = result
            });
        }
    }
}
