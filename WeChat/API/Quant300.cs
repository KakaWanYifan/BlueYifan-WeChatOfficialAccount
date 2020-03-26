using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace API
{
    public class Quant300
    {
        public string Rnt(string str)
        {
            string Msg = "";
            string Notice = "";
            str = str.ToLower();
            MySQL msql = new MySQL("stock");
            string com = "select code,name from stock.300 where code like '%" + str + "%' or name like '%" + str + "%'";
            DataTable dt = msql.S(com);
            if (dt.Rows.Count == 0)
            {
                Msg = "请检查关键词\n请回复沪深300成份股代码或名称，如000001或平安银行。\n支持模糊查询\n<a href=\"http://www.csindex.com.cn/zh-CN/indices/index-detail/000300\">点击查看关于沪深300指数</a>";
            }
            else
            {
                foreach(DataRow dr in dt.Rows)
                {
                    Msg = Msg + "<a href=\"http://www.blueyifan.tech:300/" + dr[0] + ".html\">点击查看 " + " " + dr[0].ToString().Substring(2, 6) + " " + dr[1] + "</a>\n";
                }
                Msg = Msg + Notice;
                if (System.Text.Encoding.UTF8.GetBytes(Msg).Length >= 2048)
                {
                    Msg = "抱歉，当前无法返回信息。因为腾讯的限制，所返回的消息不能超过2048个字节，请优化关键词。";
                }
            }
            return Msg;
        }
    }
}
