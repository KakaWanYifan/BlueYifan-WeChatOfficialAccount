using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace API
{
    public class Msg
    {
        public static void RetrurnTextMsg(string CounterID,string OurID, string TextMsg)
        {
            DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long CreateTime = Convert.ToInt64((DateTime.Now - StartTime).TotalSeconds);

            string rnt = "";
            rnt = rnt + "<xml>";
            rnt = rnt + "<ToUserName><![CDATA[" + CounterID + "]]></ToUserName>";
            rnt = rnt + "<FromUserName><![CDATA[" + OurID + "]]></FromUserName>";
            rnt = rnt + "<CreateTime>" + CreateTime.ToString() + "</CreateTime>";
            rnt = rnt + "<MsgType><![CDATA[text]]></MsgType>";
            rnt = rnt + "<Content><![CDATA[" + TextMsg + "]]></Content>";
            rnt = rnt + "</xml>";
            HttpContext.Current.Response.Write(rnt);
        }

        public static void RetrurnImgMsg(string CounterID, string OurID, string media_id)
        {
            DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long CreateTime = Convert.ToInt64((DateTime.Now - StartTime).TotalSeconds);

            string rnt = "";
            rnt = rnt + "<xml>";
            rnt = rnt + "<ToUserName><![CDATA[" + CounterID + "]]></ToUserName>";
            rnt = rnt + "<FromUserName><![CDATA[" + OurID + "]]></FromUserName>";
            rnt = rnt + "<CreateTime>" + CreateTime.ToString() + "</CreateTime>";
            rnt = rnt + "<MsgType><![CDATA[image]]></MsgType>";
            rnt = rnt + "<Image>";
            rnt = rnt + "<MediaId><![CDATA[" + media_id + "]]></MediaId>";
            rnt = rnt + "</Image>";
            rnt = rnt + "</xml>";
            HttpContext.Current.Response.Write(rnt);
        }

        public static void ReturnNewsMsg(string CounterID,string OurID,int ArticleCount,string[] title,string[] description,string[] picurl,string[] url)
        {
            DateTime StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long CreateTime = Convert.ToInt64((DateTime.Now - StartTime).TotalSeconds);
            string rnt = "";
            rnt = rnt + "<xml>";
            rnt = rnt + "<ToUserName><![CDATA[" + CounterID + "]]></ToUserName>";
            rnt = rnt + "<FromUserName><![CDATA[" + OurID + "]]></FromUserName>";
            rnt = rnt + "<CreateTime>" + CreateTime.ToString() + "</CreateTime>";
            rnt = rnt + "<MsgType><![CDATA[news]]></MsgType>";
            rnt = rnt + "<ArticleCount>" + ArticleCount.ToString() + "</ArticleCount>";
            rnt = rnt + "<Articles>";
            for(int i = 0;i<ArticleCount;i++)
            {
                rnt = rnt + "<item>";
                rnt = rnt + "<Title><![CDATA[" + title[i] + "]]></Title>";
                rnt = rnt + "<Description><![CDATA[" + description[i] + "]]></Description>";
                rnt = rnt + "<PicUrl><![CDATA[" + picurl[i] + "]]></PicUrl>";
                rnt = rnt + "<Url><![CDATA[" + url[i] + "]]></Url>";
                rnt = rnt + "</item>";
            }
            rnt = rnt + "</Articles>";
            rnt = rnt + "</xml>";
            HttpContext.Current.Response.Write(rnt);
        }
    }
}
