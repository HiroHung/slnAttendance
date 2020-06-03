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

        
        // PUT: api/Timetables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTimetable(int id, Timetable timetable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timetable.Id)
            {
                return BadRequest();
            }

            db.Entry(timetable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimetableExists(id))
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

        // POST: api/Timetables
        [ResponseType(typeof(Timetable))]
        public IHttpActionResult PostTimetable(Timetable timetable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Timetables.Add(timetable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = timetable.Id }, timetable);
        }

        // DELETE: api/Timetables/5
        [ResponseType(typeof(Timetable))]
        public IHttpActionResult DeleteTimetable(int id)
        {
            Timetable timetable = db.Timetables.Find(id);
            if (timetable == null)
            {
                return NotFound();
            }

            db.Timetables.Remove(timetable);
            db.SaveChanges();

            return Ok(timetable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimetableExists(int id)
        {
            return db.Timetables.Count(e => e.Id == id) > 0;
        }
    }
}