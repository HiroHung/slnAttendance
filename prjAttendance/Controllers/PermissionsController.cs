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
    public class PermissionsController : ApiController
    {
        private Model db = new Model();

        //取得首頁按鈕
        [Route("api/index/button")]
        // GET: api/Permissions
        public IHttpActionResult GetPermission()
        {
            var result = db.Permission.ToList();
            return Ok(new
            {
                code=1,
                data=result
            });
        }
        //取得登入使用者權限
        [Route("api/login/permissions")]
        public IHttpActionResult GetLoginPermission()
        {
            string Token = Request.Headers.Authorization.Parameter;
            JwtAuthUtil jwtAuthUtil =new JwtAuthUtil();
            var result = jwtAuthUtil.GetPermission(Token);
            return Ok(new
            {
                code=1,
                data=result
            });
        }

        // GET: api/Permissions/5
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

        // PUT: api/Permissions/5
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

        // POST: api/Permissions
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

        // DELETE: api/Permissions/5
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