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

namespace prjAttendance.Controllers
{
    public class AdministrationClassesController : ApiController
    {
        private Model db = new Model();

        [Route("api/administration/records/getclasses")]
        public IHttpActionResult GetClasses()
        {
            var result = db.Classes.Select(x => new
            {
                ClassId = x.Id,
                x.ClassName
            });
            return Ok(new
            {
                code = 1,
                data = result
            });
        }

    }
}