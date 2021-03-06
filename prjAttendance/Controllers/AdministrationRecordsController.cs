﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using prjAttendance.Migrations;
using prjAttendance.Models;

namespace prjAttendance.Controllers
{
    public class AdministrationRecordsController : ApiController
    {
        private Model db = new Model();

        [Route("api/administration/record/search/list")]
        [HttpGet]
        public IHttpActionResult GetAdministrationRecordsList([FromUri] ViewSearch viewSearch)
        {
            var result = db.Records.AsQueryable();
            if (viewSearch.ClassId.HasValue)
            {
                result = result.Where(x => x.ClassId == viewSearch.ClassId);
            }
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
            return Ok(new
            {
                code = 1,
                data = result.Select(x => new
                {
                    Id = x.Id,
                    Class = x.Student.Class.ClassName,
                    Name = x.Student.Name,
                    Date = x.LessonDate,
                    LessonOrder = x.LessonOrder,
                    Attendance = x.Attendance.ToString()
                }).OrderBy(x => x.Date).ThenBy(x => x.LessonOrder).ToList()
            });
        }


        [Route("api/administration/record/search/details")]
        [HttpGet]
        public IHttpActionResult GetAdministrationRecordsDetails(int Id)
        {
            ICollection<Teacher> rollcallteacher = db.Teachers.ToList();
            var record = db.Records.Where(x => x.Id == Id).ToList();
            var result = record.Select(x => new
            {
                x.Id,
                x.LessonDate,
                x.LessonOrder,
                x.Subject,
                RollCallTeacher = rollcallteacher.FirstOrDefault(y => y.Id == x.RollCallTeacherId).Name,
                Class = x.Student.Class.ClassName,
                StudentName = x.Student.Name,
                x.Attendance
            });
            return Ok(new
            {
                code = 1,
                data = result
            });
        }

        [Route("api/administration/record/search/update")]
        [HttpPut]
        public IHttpActionResult PutAdministrationRecords(int Id, int Attendance)
        {
            Record record = db.Records.FirstOrDefault(x => x.Id == Id);
            record.RollCallTime = DateTime.Now;
            record.Attendance = (AttendanceType)Attendance;
            db.SaveChanges();
            return Ok(new
            {
                code = 1,
                message = "修改成功"
            });
        }

        [Route("api/administration/record/notification")]
        [HttpGet]
        public IHttpActionResult GetAdministrationNotification([FromUri] ViewSearch viewSearch)
        {
            if (viewSearch.StartDate.HasValue && viewSearch.EndDate.HasValue)
            {
                //因為LessonDate設定為Datetime.today，所以不須再加一天，如下行處理
                //Search.End
                //= viewSearch.EndDate.Value.AddDays(1);
                var groups = db.Records.Where(x => x.Attendance == AttendanceType.曠課 && x.LessonDate >= viewSearch.StartDate && x.LessonDate <= viewSearch.EndDate).GroupBy(x => x.StudentId);
                var record = groups.Select(x => new
                {
                    StudentId = x.Key,
                    Class = x.FirstOrDefault().Student.Class.ClassName,
                    StudentNumber = x.FirstOrDefault().Student.StudentNumber,
                    Name = x.FirstOrDefault().Student.Name,
                    Address = x.FirstOrDefault().Student.Address,
                    Guardian = x.FirstOrDefault().Student.Guardian,
                    Times = x.Count(),
                    result = x.Select(y => new
                    {
                        LessonDate = y.LessonDate,
                        LessonOrder = y.LessonOrder,
                        Subject = y.Subject
                    })
                });
                return Ok(new
                {
                    code = 1,
                    data = record
                });
            }

            return Ok(new
            {
                code = 5588,
                message = "請選擇起訖時間"
            });
        }
    }
}
