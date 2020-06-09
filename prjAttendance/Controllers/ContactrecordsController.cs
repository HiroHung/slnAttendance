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
    public class ContactrecordsController : ApiController
    {
        private Model db = new Model();

        // GET: api/Contactrecords
        public IQueryable<Contactrecord> GetContactrecords()
        {
            return db.Contactrecords;
        }

        // GET: api/Contactrecords/5
        [ResponseType(typeof(Contactrecord))]
        [Route("api/mentor/contactrecord/search/list")]
        public IHttpActionResult GetContactrecordList([FromUri]ViewSearch viewSearch)
        {
            string Token = Request.Headers.Authorization.Parameter;
            int id = JwtAuthUtil.GetId(Token);
            var result = db.Contactrecords.AsQueryable();
            if (viewSearch.StudentId.HasValue)
            {
                result = result.Where(x => x.StudentId == viewSearch.StudentId);
            }
            if (viewSearch.StartDate.HasValue && viewSearch.EndDate.HasValue)
            {

                viewSearch.EndDate = viewSearch.EndDate.Value.AddDays(1);
                result = result.Where(x => x.ContactDateTime >= viewSearch.StartDate && x.ContactDateTime <= viewSearch.EndDate);
            }

            return Ok(new
            {
                code=1,
                data=result.Where( x=> x.TeacherId == id).Select(x=>new
                {
                    Id=x.Id,
                    Time=x.ContactDateTime,
                    StudentName=db.Students.Where(y=>y.Id==x.StudentId).Select(y=>y.Name).FirstOrDefault(),
                    ContactGuardian=x.ContactGuardian
                })
            });
        }

        [ResponseType(typeof(Contactrecord))]
        [Route("api/mentor/contactrecord/search/details")]
        public IHttpActionResult GetContactrecordDetails(int Id)
        {
            var result = db.Contactrecords.Where(x => x.Id == Id).Select(x=>new
            {
                Time=x.ContactDateTime,
                Teacher=x.Teacher.Name,
                StudentName=db.Students.Where(y=>y.Id==x.StudentId).Select(y=>y.Name).FirstOrDefault(),
                ContactGuardian = x.ContactGuardian,
                Method=x.Method,
                Reason=x.Reason,
                Results=x.Results
            });
            return Ok(new
            {
                code=1,
                result
            });
        }

        // PUT: api/Contactrecords/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContactrecord(int id, Contactrecord contactrecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactrecord.Id)
            {
                return BadRequest();
            }

            db.Entry(contactrecord).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactrecordExists(id))
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

        // POST: api/Contactrecords
        [HttpPost]
        [Route("api/mentor/contactrecord/create")]
        [ResponseType(typeof(Contactrecord))]
        public IHttpActionResult PostContactrecord(Contactrecord contactrecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string Token = Request.Headers.Authorization.Parameter;
            int Id = JwtAuthUtil.GetId(Token);
            contactrecord.TeacherId = Id;
            db.Contactrecords.Add(contactrecord);
            db.SaveChanges();

            return Ok(new
            {
                code=1,
                message ="紀錄完成"
            });
        }

        // DELETE: api/Contactrecords/5
        [ResponseType(typeof(Contactrecord))]
        public IHttpActionResult DeleteContactrecord(int id)
        {
            Contactrecord contactrecord = db.Contactrecords.Find(id);
            if (contactrecord == null)
            {
                return NotFound();
            }

            db.Contactrecords.Remove(contactrecord);
            db.SaveChanges();

            return Ok(contactrecord);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactrecordExists(int id)
        {
            return db.Contactrecords.Count(e => e.Id == id) > 0;
        }
    }
}