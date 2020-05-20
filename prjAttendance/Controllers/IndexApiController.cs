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
    public class IndexApiController : ApiController
    {
        private Model db = new Model();

        // GET: api/IndexApi
        [Route("api/Index")]
        public IQueryable<Permission> GetPermission()
        {
            return db.Permission;
        }

        // GET: api/IndexApi/5
        [ResponseType(typeof(Permission))]
        public IHttpActionResult GetPermission(int id)
        {
            Permission permission = db.Permission.Find(id);
            if (permission == null)
            {
                return NotFound();
            }

            return Ok(permission);
        }

        // PUT: api/IndexApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPermission(int id, Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permission.id)
            {
                return BadRequest();
            }

            db.Entry(permission).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
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

        // POST: api/IndexApi
        [ResponseType(typeof(Permission))]
        public IHttpActionResult PostPermission(Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Permission.Add(permission);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = permission.id }, permission);
        }

        // DELETE: api/IndexApi/5
        [ResponseType(typeof(Permission))]
        public IHttpActionResult DeletePermission(int id)
        {
            Permission permission = db.Permission.Find(id);
            if (permission == null)
            {
                return NotFound();
            }

            db.Permission.Remove(permission);
            db.SaveChanges();

            return Ok(permission);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermissionExists(int id)
        {
            return db.Permission.Count(e => e.id == id) > 0;
        }
    }
}