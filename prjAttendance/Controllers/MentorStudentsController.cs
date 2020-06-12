using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using prjAttendance.Models;
using prjAttendance.Security;

namespace prjAttendance.Controllers
{
    public class MentorStudentsController : ApiController
    {
        private Model db = new Model();

        [Route("api/mentor/getstudents")]
        public IHttpActionResult GetStudents()
        {
            string Token = Request.Headers.Authorization.Parameter;
            int id = JwtAuthUtil.GetId(Token);
            var result = db.Students.Where(x => x.TeacherId == id).Select(x =>new
            {
                x.Id,
                x.Name
            }).OrderBy(x=>x.Id);
            return Ok(new
            {
                code=1,
                data=result
            });
        }

    }
}