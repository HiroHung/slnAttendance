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
using Microsoft.Ajax.Utilities;
using prjAttendance.Models;
using prjAttendance.Security;

namespace prjAttendance.Controllers
{
    public class TeachRecordsController : ApiController
    {
        private Model db = new Model();
        // GET: api/Records
        public IQueryable<Record> GetRecords()
        {
            return db.Records;
        }

        // GET: api/Records/5
        [Route("api/teach/records")]
        [ResponseType(typeof(Record))]
        public IHttpActionResult GetRecord(double LessonOrder, int ClassId)
        {
            var record = db.Records.Where(x => x.LessonDate == DateTime.Today && x.LessonOrder == LessonOrder && x.ClassId == ClassId).ToList();
            int id = JwtAuthUtil.GetId(Request.Headers.Authorization.Parameter);
            int week = Utility.GetWeek();
            if (record.Count == 0)
            {
                var result = db.Students.Where(x => x.ClasssId == ClassId).Select(x => new
                {
                    StudentId = x.Id,
                    學號 = x.StudentNumber,
                    姓名 = x.Name
                });
                return Ok(new
                {
                    code = 1,
                    method = "post",
                    data = result,
                });
            }
            else
            {
                var result = db.Records
                    .Where(x => x.LessonDate == DateTime.Today && x.LessonOrder == LessonOrder && x.ClassId == ClassId)
                    .OrderBy(x => x.StudentId)
                    .Select(x => new
                    {
                        x.Id,
                        x.StudentId,
                        學號 = x.Student.StudentNumber,
                        姓名 = x.Student.Name,
                        x.Attendance
                    });
                return Ok(new
                {
                    code = 1,
                    method = "put",
                    data = result
                });
            }
        }

        // PUT: api/Records/5
        [HttpPut]
        [Route("api/teach/records/update")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecord(ViewRecord viewRecord)
        {
            foreach (var infoitem in viewRecord.StudentInfo)
            {
                Record record = db.Records.Single(x => x.Id == infoitem.Id);
                record.RollCallTime = DateTime.Now;
                record.Attendance = (AttendanceType)infoitem.Attendance;
                db.SaveChanges();
            }
            return Ok(new
            {
                code = 1,
                message = "修改成功"
            });
        }

        // POST: api/Records
        [Route("api/teach/records/create")]
        [ResponseType(typeof(Record))]
        public IHttpActionResult PostRecord(ViewRecord viewRecord)
        {
            int id = JwtAuthUtil.GetId(Request.Headers.Authorization.Parameter);
            int week = Utility.GetWeek();
            Record record = new Record();
            foreach (var infoitem in viewRecord.StudentInfo)
            {
                record.ClassId = viewRecord.ClassId;
                record.LessonOrder = viewRecord.LessonOrder;
                record.RollCallTime = DateTime.Now;
                record.LessonDate = DateTime.Today;
                record.RollCallTeacherId = id;
                record.Week = (WeekType)week;
                record.StudentId = infoitem.StudentId;
                record.Attendance = (AttendanceType)infoitem.Attendance;
                db.Records.Add(record);
                db.SaveChanges();
            }
            //return CreatedAtRoute("DefaultApi", new { id = record.Id }, record);
            return Ok(new
            {
                code = 1,
                message = "點名成功"
            });
        }

        // DELETE: api/Records/5
        [ResponseType(typeof(Record))]
        public IHttpActionResult DeleteRecord(int id)
        {
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return NotFound();
            }

            db.Records.Remove(record);
            db.SaveChanges();

            return Ok(record);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RecordExists(int id)
        {
            return db.Records.Count(e => e.Id == id) > 0;
        }
    }
}