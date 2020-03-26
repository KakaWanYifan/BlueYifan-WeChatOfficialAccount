using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Net.Security;
using System.Web;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Converters;
namespace Main
{
    /// <summary>
    /// 处理菜单相关的类
    /// </summary>
    public partial class Menu
    {

        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="menuEntity">菜单实体</param>
        /// <param name="accessToken">accessToken</param>
        /// <returns>错误信息实体</returns>
        public static ErrorEntity Create(MenuEntity menuEntity,string accessToken)
        {
            var url = string.Format(" https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", accessToken);
            return PostResult<ErrorEntity>(menuEntity, url);
        }

        /// <summary>
        /// 查询自定义菜单
        /// </summary>
        public static MenuEntity Query(string accessToken)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", accessToken);
            var jobj = GetResult<JObject>(url);
            var menu = jobj["menu"].ToString();

            return JsonConvert.DeserializeObject<MenuEntity>(menu);
        }

        public static T GetResult<T>(string url)
        {
            var retdata = HttpGet(url);
            return JsonConvert.DeserializeObject<T>(retdata);
        }
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";//设置请求的方法
            request.Accept = "*/*";//设置Accept标头的值
            string responseStr = "";
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())//获取响应
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    responseStr = reader.ReadToEnd();
                }
            }
            return responseStr;
        }

                /// <summary>
        /// 发起post请求，并获取请求返回值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="obj">数据实体</param>
        /// <param name="url">接口地址</param>
        public static T PostResult<T>(object obj, string url)
        {
            //序列化设置
            var setting = new JsonSerializerSettings();


            //解决枚举类型序列化时，被转换成数字的问题
            setting.Converters.Add(new StringEnumConverter());
            var retdata = HttpPost(url, JsonConvert.SerializeObject(obj, setting));
            return JsonConvert.DeserializeObject<T>(retdata);
        }

        public static string HttpPost(string url, string data, string certpath = "", string certpwd = "")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //当请求为https时，验证服务器证书
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((a, b, c, d) =>
            {
                if (d == SslPolicyErrors.None)
                    return true;
                return false;
            });
            if (!string.IsNullOrEmpty(certpath) && !string.IsNullOrEmpty(certpwd))
            {
                X509Certificate2 cer = new X509Certificate2(certpath, certpwd,
                    X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet);
                request.ClientCertificates.Add(cer);
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;
            string responseStr = "";
            using (StreamWriter requestStream = new StreamWriter(request.GetRequestStream()))
            {
                requestStream.Write(data);//将请求的数据写入到请求流中
            }
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        responseStr = reader.ReadToEnd();//获取响应
                        WriteTxt("/debug.txt", responseStr);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return responseStr;
        }

        public static void WriteTxt(string path, string txt)
        {
            using (FileStream fs = new FileStream(HttpContext.Current.Request.MapPath(path), FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(txt);
                    sw.Flush();
                }
            }
        }
    }
}
