using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Jose;

namespace prjAttendance.Security
{
    public class JwtAuthUtil
    {
        public string GenerateToken(int id, string name, string account)
        {
            string secret = "myJwtAuthDemo";//加解密的key,如果不一樣會無法成功解密
            Dictionary<string, Object> claim = new Dictionary<string, Object>();//payload 需透過token傳遞的資料
            claim.Add("Id", id);
            claim.Add("Name", name);
            claim.Add("Account", account);
            claim.Add("Inittime", DateTime.Now.ToString());
            claim.Add("Exp", DateTime.Now.AddSeconds(Convert.ToInt32("86400")).ToString());//Token 時效設定100秒
            var payload = claim;
            var token = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(secret), JwsAlgorithm.HS512);//產生token
            return token;
        }

        public string Tid(string Token)
        {
            string secret = "myJwtAuthDemo";//加解密的key,如果不一樣會無法成功解密
            var jwtObject = Jose.JWT.Decode<Dictionary<string, Object>>(
                Token,
                Encoding.UTF8.GetBytes(secret),
                JwsAlgorithm.HS512);
            string id = jwtObject["Id"].ToString();
            return id;
        }
    }
}