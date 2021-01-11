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

        [ResponseType(typeof(Contactrecord))]
        [Route("api/mentor/contactrecord/search/list")]
        public IHttpActionResult GetContactrecordList([FromUri]ViewSearch viewSearch)
        {
            string Token = Request.Headers.Authorization.Parameter;
            int id = JwtAuthUtil.GetId(Token);
            var result = db.Contactrecords.Where(x => x.TeacherId == id).AsQueryable();
            if (viewSearch.StudentId.HasValue)
            {
                result = result.Where(x => x.StudentId == viewSearch.StudentId);
            }
            if (viewSearch.StartDate.HasValue && viewSearch.EndDate.HasValue)
            {

                viewSearch.EndDate = viewSearch.EndDate.Value.AddDays(1);
                result = result.Where(x => x.ContactDateTime >= viewSearch.StartDate && x.ContactDateTime <= viewSearch.EndDate);
            }
       
            var students = db.Students.AsQueryable();
            var data = result.Select(x => new
            {
                Id = x.Id,
                Time = x.ContactDateTime,
                StudentName = students.FirstOrDefault(y => y.Id == x.StudentId).Name,
                ContactGuardian = x.ContactGuardian
            });
            return Ok(new
            {
                code=1,
                data=data
            });
        }

        [ResponseType(typeof(Contactrecord))]
        [Route("api/mentor/contactrecord/search/details")]
        public IHttpActionResult GetContactrecordDetails(int Id)
        {
            var students = db.Students.ToList();
            var contactrecord = db.Contactrecords.Where(x => x.Id == Id).ToList();
            var result = contactrecord.Select(x=>new
            {
                Time=x.ContactDateTime,
                Teacher=x.Teacher.Name,
                StudentName= students.FirstOrDefault(y=>y.Id==x.StudentId).Name,
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

        [ResponseType(typeof(Contactrecord))]
        [Route("api/mentor/contactrecord/print")]
        public IHttpActionResult GetContactrecordPrint([FromUri]ViewSearch viewSearch)
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
            var students = db.Students.AsQueryable();
            var data = result.Select(x => new
            {
                Id = x.Id,
                Time = x.ContactDateTime,
                StudentName = students.FirstOrDefault(y => y.Id == x.StudentId).Name,
                ContactGuardian = x.ContactGuardian,
                Teacher = x.Teacher.Name,
                Method = x.Method,
                Reason = x.Reason,
                Results = x.Results
            });
            return Ok(new
            {
                code = 1,
                data =data
            });
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

    }
}