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
    public class TimetablesController : ApiController
    {
        private Model db = new Model();

        // GET: api/Timetables
        [Route("api/teach/timetables")]
        public IHttpActionResult GetTimetable()
        {
            string Token = Request.Headers.Authorization.Parameter;
            int tid = JwtAuthUtil.GetId(Token);
            int Week = Utility.GetWeek();
            if (Week == 0)
            {
                return Ok(new
                {
                    code=5588,
                    message="假日無課表"
                });
            }
            var result = db.Timetables
                .Where(x => x.TeacherId == tid && (int)x.Week == Week).OrderBy(x=>x.LessonOrder)
                .Select(x => new
                {
                    x.Subject,
                    LessonOrder = x.LessonOrder,
                    x.ClassId,
                    x.Class.ClassName
                });
            return Ok(new
            {
                code = 1,
                data = result
            });
        }

    }
}