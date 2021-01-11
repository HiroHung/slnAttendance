using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using prjAttendance.Models;
using prjAttendance.Security;

namespace prjAttendance.Controllers
{
    public class StudentsRecordsController : ApiController
    {
        private Model db = new Model();

        [HttpGet]
        [Route("api/students/records")]
        public IHttpActionResult GetStudentsRecords([FromUri] ViewSearch viewSearch)
        {
            int id = JwtAuthUtil.GetId(Request.Headers.Authorization.Parameter);
            var result = db.Records.Where(x => x.StudentId == id).AsQueryable();
            if (viewSearch.StartDate.HasValue && viewSearch.EndDate.HasValue)
            {
                //因為LessonDate設定為Datetime.today，所以不須再加一天，如下行處理
                //Search.EndDate = viewSearch.EndDate.Value.AddDays(1);
                result = result.Where(x => x.LessonDate >= viewSearch.StartDate && x.LessonDate <= viewSearch.EndDate);
            }
            if (viewSearch.Attendance.HasValue)
            {
                result = result.Where(x => x.Attendance == viewSearch.Attendance);
            }
            var teacher = db.Teachers.AsQueryable();
            var data = result.Select(x => new
            {
                x.LessonDate,
                x.LessonOrder,
                x.Subject,
                Teacher = teacher.FirstOrDefault(y => y.Id == x.RollCallTeacherId).Name,
                Attendance = x.Attendance.ToString()
            }).OrderBy(x => x.LessonDate).ToList();
            return Ok(new
            {
                code = 1,
                data = data
            });
        }

        [HttpGet]
        [Route("api/students/records/deduction")]
        public IHttpActionResult GetStudentsDeduction()
        {
            int id = JwtAuthUtil.GetId(Request.Headers.Authorization.Parameter);
            var groups = db.Records.Where(x => x.StudentId == id).GroupBy(x => x.StudentId);
            var record = groups.Select(x => new
            {
                事假次數 = x.Count(y => y.Attendance == AttendanceType.事假),
                事假扣分 = x.Count(y => y.Attendance == AttendanceType.事假) / 20 * -1,
                病假次數 = x.Count(y => y.Attendance == AttendanceType.病假),
                病假扣分 = x.Count(y => y.Attendance == AttendanceType.病假) / 50 * -1,
                喪假次數 = x.Count(y => y.Attendance == AttendanceType.喪假),
                喪假扣分 = x.Count(y => y.Attendance == AttendanceType.喪假) * 0,
                曠課次數 = x.Count(y => y.Attendance == AttendanceType.曠課),
                曠課扣分 = x.Count(y => y.Attendance == AttendanceType.曠課) / 2 * -1,
                遲到次數 = x.Count(y => y.Attendance == AttendanceType.遲到),
                遲到扣分 = x.Count(y => y.Attendance == AttendanceType.遲到) / 3 * -1, 
                Deduction = x.Count(y => y.Attendance == AttendanceType.事假) / 20 * -1 + x.Count(y => y.Attendance == AttendanceType.病假) / 50 * -1 + x.Count(y => y.Attendance == AttendanceType.曠課) / 2 * -1 + x.Count(y => y.Attendance == AttendanceType.遲到) / 3 * -1,
            });
            return Ok(new
            {
                code = 1,
                data = record.ToList()
            });
        }

    }
}
