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
    public class TeachersApiController : ApiController
    {
        private Model db = new Model();

        [HttpPost]
        [Route("api/login")]
        [JwtAuthFilter]
        public HttpResponseMessage Post(ViewLogin viewLogin)
        {
            if (ModelState.IsValid)
            {
                if (viewLogin.Permission == "04")
                {
                    Student student = ValidateStudentUser(viewLogin.Account, viewLogin.Password);
                    if (student != null)
                    {
                        JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
                        string jwtToken = jwtAuthUtil.GenerateToken(student.Id, student.Permission);
                        return Request.CreateResponse(HttpStatusCode.OK, new
                        {
                            code = 1,
                            token = jwtToken,
                            message = "登入成功"
                        });
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        code = 5567,
                        message = "帳密錯誤或身分不符"
                    });
                }
                Teacher teacher = ValidateTeacherUser(viewLogin.Account, viewLogin.Password, viewLogin.Permission);
                if (teacher != null)
                {
                    JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
                    string jwtToken = jwtAuthUtil.GenerateToken(teacher.Id, teacher.Permission.ToString());
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        code = 1,
                        token = jwtToken,
                        message = "登入成功"
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, new
                {
                    code = 5567,
                    message = "帳密錯誤或身分不符"
                });

            }
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                code = 5567,
                message = "登入失敗"
            });
        }

        private Teacher ValidateTeacherUser(string account, string password, string permission)
        {
            Teacher teacher = db.Teachers.SingleOrDefault(o => o.Email == account);
            if (teacher == null)
            {
                return null;
            }
            if (teacher.Permission.IndexOf(permission) == -1)
            {
                return null;
            }
            return password == teacher.IDcardNumber ? teacher : null;
        }

        private Student ValidateStudentUser(string account, string password)
        {
            Student student = db.Students.SingleOrDefault(o => o.StudentNumber == account);
            if (student == null)
            {
                return null;
            }
            return password == student.IDcardNumber ? student : null;
        }
    }
}