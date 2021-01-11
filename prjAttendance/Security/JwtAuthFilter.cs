using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Jose;

namespace prjAttendance.Security
{
    public class JwtAuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string secret = "ILoveRocketCoding";//加解密的key,如果不一樣會無法成功解密
            var request = actionContext.Request;
            if (!WithoutVerifyToken(request.RequestUri.ToString()))
            {
                if (request.Headers.Authorization==null || request.Headers.Authorization.Scheme != "Bearer")
                {
                    var errorMessage = new HttpResponseMessage()
                    {
                        ReasonPhrase = "Lost Token",
                        Content = new StringContent(" code = 5566"),
                    };
                    throw new HttpResponseException(errorMessage);
                    //actionContext.Request.CreateResponse(HttpStatusCode.OK, new
                    //{
                    //    code = 5566,
                    //    message = "Lost Token"
                    //});
                }
                else
                {
                    try
                    {
                        var jwtObject = Jose.JWT.Decode<Dictionary<string, Object>>(
                            request.Headers.Authorization.Parameter,
                            Encoding.UTF8.GetBytes(secret),
                            JwsAlgorithm.HS512);
                        if (IsTokenExpired(jwtObject["Exp"].ToString()))
                        {
                            var errorMessage = new HttpResponseMessage()
                            {
                                ReasonPhrase = "Token Expired",
                                Content = new StringContent(" code = 5566"),
                            };
                            throw new HttpResponseException(errorMessage);

                            //actionContext.Request.CreateResponse(HttpStatusCode.OK, new
                            //{
                            //    code = 5566,
                            //    message = "Token Expired"
                            //});
                        }
                    }
                    catch (Exception e)
                    {
                        var errorMessage = new HttpResponseMessage()
                        {
                            ReasonPhrase = "Lost Token",
                            Content = new StringContent($"code = 5566,發生錯誤：{e}"),
                        };
                        throw new HttpResponseException(errorMessage);
                    }
                }
            }
            base.OnActionExecuting(actionContext);
        }

        //Login不需要驗證因為還沒有token
        public bool WithoutVerifyToken(string requestUri)
        {
            if (requestUri.EndsWith("/login") || requestUri.EndsWith("/index/button"))
                return true;
            return false;
        }

        //驗證token時效
        public bool IsTokenExpired(string dateTime)
        {
            return Convert.ToDateTime(dateTime) < DateTime.Now;
        }
    }
}