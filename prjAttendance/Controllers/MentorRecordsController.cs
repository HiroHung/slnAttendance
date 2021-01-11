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
    public class MentorRecordsController : ApiController
    {
        private Model db = new Model();

        [Route("api/mentor/todayrecords")]
        [ResponseType(typeof(Record))]
        public IHttpActionResult GetRecords()
        {
            DateTime Sdate = DateTime.Today;
            DateTime Edate = DateTime.Today.AddDays(1);
            string Token = Request.Headers.Authorization.Parameter;
            int id = JwtAuthUtil.GetId(Token);
            var todaydatas = db.Records.Where(x => x.LessonDate >= Sdate && x.LessonDate <= Edate && x.Student.TeacherId == id);
            var gropBydatas = todaydatas.GroupBy(x => x.LessonOrder);
            var records = gropBydatas.Select(x => new
            {
                result = new
                {
                    LessonOrder = x.Key,
                    遲到 = x.Where(y => y.Attendance == AttendanceType.遲到).Select(y => new
                    {
                        y.Student.Name
                    }),
                    曠課 = x.Where(y => y.Attendance == AttendanceType.曠課).Select(y => new
                    {
                        y.Student.Name
                    }),
                    病假 = x.Where(y => y.Attendance == AttendanceType.病假).Select(y => new
                    {
                        y.Student.Name
                    }),
                    事假 = x.Where(y => y.Attendance == AttendanceType.事假).Select(y => new
                    {
                        y.Student.Name
                    }),
                    喪假 = x.Where(y => y.Attendance == AttendanceType.喪假).Select(y => new
                    {
                        y.Student.Name
                    }),
                    出席 = x.Where(y => y.Attendance == AttendanceType.出席).Select(y => new
                    {
                        y.Student.Name
                    })
                }
            });
            return Ok(new
            {
                code = 1,
                data = records
            });
        }

        [Route("api/mentor/absenteeism")]
        [ResponseType(typeof(Record))]
        public IHttpActionResult Getabsenteeism()
        {
            string Token = Request.Headers.Authorization.Parameter;
            int id = JwtAuthUtil.GetId(Token);
            var groups = db.Records.Where(x=>x.Student.TeacherId==id).GroupBy(x => x.StudentId);
            
            var record = groups.Select(x => new
            {
                StudentId = x.Key,
                Name = x.FirstOrDefault(y => y.StudentId == x.Key).Student.Name,
                Times = x.Where(y => y.Attendance == AttendanceType.曠課).Count(),
                //Attendance = x.Where(y => y.Attendance == AttendanceType.曠課).Select(y => y.Attendance).FirstOrDefault().ToString()
            })/*.Where(x => x.Attendance == "曠課")*/.OrderByDescending(x => x.Times).ThenBy(x=>x.StudentId);
            return Ok(new
            {
                code=1,
                data= record.ToList()
            });
        }
       
        [Route("api/mentor/leave")]
        [ResponseType(typeof(Record))]
        public IHttpActionResult Getleave()
        {
            string Token = Request.Headers.Authorization.Parameter;
            int id = JwtAuthUtil.GetId(Token);
            var groups = db.Records.Where(x => x.Student.TeacherId == id).GroupBy(x => x.StudentId);

            var record = groups.Select(x => new
            {
                StudentId = x.Key,
                Name = x.FirstOrDefault(y => y.StudentId == x.Key).Student.Name,
                Times = x.Where(y => y.Attendance == AttendanceType.事假).Count()+ x.Where(y => y.Attendance == AttendanceType.喪假).Count()+ x.Where(y => y.Attendance == AttendanceType.病假).Count(),
                //Attendance = x.Where(y => y.Attendance == AttendanceType.曠課).Select(y => y.Attendance).FirstOrDefault().ToString()
            })/*.Where(x => x.Attendance == "曠課")*/.OrderByDescending(x => x.Times).ThenBy(x => x.StudentId);
            return Ok(new
            {
                code = 1,
                data = record.ToList()
            });
        }
        
        [Route("api/mentor/deduction")]
        [ResponseType(typeof(Record))]
        public IHttpActionResult Getdeductione()
        {
            string Token = Request.Headers.Authorization.Parameter;
            int id = JwtAuthUtil.GetId(Token);
            var groups = db.Records.Where(x => x.Student.TeacherId == id).GroupBy(x => x.StudentId);

            var record = groups.Select(x => new
            {
                StudentId = x.Key,
                StudentNumber= x.FirstOrDefault(y => y.StudentId == x.Key).Student.Id,
                Name = x.FirstOrDefault(y => y.StudentId == x.Key).Student.Name,
                Deduction = x.Count(y => y.Attendance == AttendanceType.事假)/20*-1 + x.Count(y => y.Attendance == AttendanceType.病假)/50*-1 + x.Count(y => y.Attendance == AttendanceType.曠課)/2*-1+ x.Count(y => y.Attendance == AttendanceType.遲到) / 3 * -1,
                事假次數= x.Count(y => y.Attendance == AttendanceType.事假),
                事假扣分= x.Count(y => y.Attendance == AttendanceType.事假) / 20 * -1,
                病假次數 = x.Count(y => y.Attendance == AttendanceType.病假),
                病假扣分= x.Count(y => y.Attendance == AttendanceType.病假) / 50 * -1,
                喪假次數 = x.Count(y => y.Attendance == AttendanceType.喪假),
                喪假扣分 = x.Count(y => y.Attendance == AttendanceType.喪假)*0,
                曠課次數 = x.Count(y => y.Attendance == AttendanceType.曠課),
                曠課扣分= x.Count(y => y.Attendance == AttendanceType.曠課) / 2 * -1,
                遲到次數 = x.Count(y => y.Attendance == AttendanceType.遲到),
                遲到扣分= x.Count(y => y.Attendance == AttendanceType.遲到) / 3 * -1,
                Guardian = x.FirstOrDefault(y => y.StudentId == x.Key).Student.Guardian,
                PhoneNunber = x.FirstOrDefault(y => y.StudentId == x.Key).Student.PhoneNumber,
                Address = x.FirstOrDefault(y => y.StudentId == x.Key).Student.Address
            }).OrderBy(x => x.Deduction).ThenBy(x => x.StudentId).ToList();
            return Ok(new
            {
                code = 1,
                data = record
            });
        }

        [Route("api/mentor/attendance/search")]
        [ResponseType(typeof(Record))]
        public IHttpActionResult GetRecord([FromUri]ViewSearch viewSearch)
        {
            string Token = Request.Headers.Authorization.Parameter;
            int id = JwtAuthUtil.GetId(Token);
            var result = db.Records.Where(x => x.Student.TeacherId == id).AsQueryable();
            if (viewSearch.StudentId.HasValue)
            {
                result = result.Where(x => x.StudentId == viewSearch.StudentId);
            }
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
                Id = x.Id,
                Name = x.Student.Name,
                Date = x.LessonDate,
                LessonOrder = x.LessonOrder,
                Subject = x.Subject,
                RollCallTeacherId = teacher.FirstOrDefault(y => y.Id == x.RollCallTeacherId).Name,
                Attendance = x.Attendance.ToString()
            }).OrderBy(x => x.Date).ToList();
            return Ok(new
            {
                code = 1,
                data = data
            });
        }
    }
}