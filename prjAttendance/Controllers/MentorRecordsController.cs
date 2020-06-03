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

        // GET: api/MentorRecords
        [ResponseType(typeof(Record))]
        [Route("api/mentor/todayrecords")]
        public IHttpActionResult GetRecords()
        {
            DateTime Sdate = DateTime.Today;
            DateTime Edate = DateTime.Today.AddDays(1);
            string Token = Request.Headers.Authorization.Parameter;
            int id = JwtAuthUtil.GetId(Token);
            var todaydatas = db.Records.Where(x => x.LessonDate >= Sdate && x.LessonDate <= Edate && x.Student.TeacherId == id);
            var gropBydatas = todaydatas.GroupBy(x => x.LessonOrder);
            var records =gropBydatas.Select(x => new
            {
                result = new
                {
                    LessonOrder=x.Key,
                    遲到=x.Where(y=>y.Attendance == AttendanceType.遲到).Select(y=>new
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
                code=1,
                data=records
            });
        }

        // GET: api/MentorRecords/5
        [ResponseType(typeof(Record))]
        public IHttpActionResult GetRecord(int id)
        {
            Record record = db.Records.Find(id);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(record);
        }

        // PUT: api/MentorRecords/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRecord(int id, Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != record.Id)
            {
                return BadRequest();
            }

            db.Entry(record).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MentorRecords
        [ResponseType(typeof(Record))]
        public IHttpActionResult PostRecord(Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Records.Add(record);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = record.Id }, record);
        }

        // DELETE: api/MentorRecords/5
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