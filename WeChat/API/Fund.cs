using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace API
{
    public class Fund
    {
        public string FundNet(string str)
        {
            string com = "select * from blue.fundnet where code like '%" + str + "%' or name like '%" + str + "%'" + "order by time desc,code";
            MySQL msql = new MySQL("blue");
            DataTable dt = msql.S(com);
            string rnt = "";
            string notice = "点击基金代码和基金名称即可查看该基金的投资组合数据";
            int nLength = System.Text.Encoding.UTF8.GetBytes(notice).Length;
            if (dt.Rows.Count == 0)
            {
                rnt = "抱歉，未查询到基金信息。请检查关键词。\n（仅支持开放式基金，专户产品等不可查。）";
                return rnt;
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    rnt = rnt + "<a href=\"http://www.blueyifan.tech:9000/Fund2nd/fund/" + dr["code"].ToString() + "/" + dr["name"].ToString() + "\">" + "基金代码：" + dr["code"].ToString() + "</a>\n";
                    rnt = rnt + "<a href=\"http://www.blueyifan.tech:9000/Fund2nd/fund/" + dr["code"].ToString() + "/" + dr["name"].ToString() + "\">" + "基金名称：" + dr["name"].ToString() + "</a>\n";
                    rnt = rnt + "最新净值：" + dr["newnet"].ToString() + "\n";
                    rnt = rnt + "累计净值：" + dr["totalnet"].ToString() + "\n";
                    rnt = rnt + "日增长值：" + dr["dayincrease"].ToString() + "\n";
                    rnt = rnt + "日增长率：" + dr["daygrowrate"].ToString() + "\n";
                    rnt = rnt + "周增长率：" + dr["weekgrowrate"].ToString() + "\n";
                    rnt = rnt + "月增长率：" + dr["monthgrowrate"].ToString() + "\n";
                    rnt = rnt + "最新净值时间：" + dr["time"].ToString() + "\n";
                    rnt = rnt + "\n";
                    if (System.Text.Encoding.UTF8.GetBytes(rnt).Length >= 2048-nLength)
                    {
                        rnt = "抱歉，当前无法返回信息。因为腾讯的限制，所返回的消息不能超过2048个字节，请优化关键词。";
                        return rnt;
                    }
                }
                rnt = rnt + notice;
                return rnt;
            }
        }
    }
}
