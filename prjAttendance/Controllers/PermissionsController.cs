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

    }
}