using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace API
{
    public class BaseServices
    {
        public static void ValidUrl(string Token)
        {
            HttpContext context = HttpContext.Current;
            string signature = context.Request.QueryString["signature"];
            string timestamp = context.Request.QueryString["timestamp"];
            string nonce = context.Request.QueryString["nonce"];
            string echostr = context.Request.QueryString["echostr"];
            string[] arr = new string[] { Token, timestamp, nonce };
            Array.Sort(arr);
            string str = string.Join("", arr);
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "sha1").ToLower();
            //这个是在.NET 4.5 中已经弃用的API，有时间换别的。
            if (signature == sign)
            {
                context.Response.Write(echostr);
            }
        }
    }
}
