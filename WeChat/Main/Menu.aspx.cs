using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Main
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //BlueYifan
            string accessToken = AccessTokenBox.GetTokenValue("XXX", "XXX");
            var child1 = new List<BaseMenu>();
            var child2 = new List<BaseMenu>();
            var child3 = new List<BaseMenu>();
            var basebtn = new List<BaseMenu>();
            //child1.Add(new BaseMenu
            //{
            //    url = "https://mp.weixin.qq.com/mp/homepage?__biz=MzIwODYwOTU3OA==&hid=5&sn=68e23e35ea40b57874630d6b277fc35d",
            //    name = "数学分析 - 习题解答",
            //    type = MenuType.view
            //});
            //child1.Add(new BaseMenu
            //{
            //    url = "https://mp.weixin.qq.com/mp/homepage?__biz=MzIwODYwOTU3OA==&hid=8&sn=df42f8418381be747fa655d75918f741",
            //    name = "高等代数 - 习题解答",
            //    type = MenuType.view
            //});
            //child1.Add(new BaseMenu
            //{
            //    url = "https://mp.weixin.qq.com/mp/homepage?__biz=MzIwODYwOTU3OA==&hid=6&sn=989b0faf398ee1ebdbe7e5303a142fe6",
            //    name = "金融计量学 - 学习笔记",
            //    type = MenuType.view
            //});
            //child1.Add(new BaseMenu
            //{
            //    url = "https://mp.weixin.qq.com/mp/homepage?__biz=MzIwODYwOTU3OA==&hid=7&sn=b24035f1686b12c5bc505c778e1047f9",
            //    name = "统计学习方法 - 习题解答",
            //    type = MenuType.view
            //});
            child1.Add(new BaseMenu
            {
                key = "数学的笔记与练习",
                name = "数学的笔记与练习",
                type = MenuType.click
            });
            child1.Add(new BaseMenu
            {
                key = "量化分析沪深300成份股",
                name = "量化分析沪深300成份股",
                type = MenuType.click
            });
            //child2.Add(new BaseMenu
            //{
            //    key = "查基金",
            //    name = "查基金",
            //    type = MenuType.click
            //});
            child2.Add(new BaseMenu
            {
                key = "测海拔高度",
                name = "测海拔高度",
                type = MenuType.click
            });
            child2.Add(new BaseMenu
            {
                key = "蓝色机器人",
                name = "蓝色机器人",
                type = MenuType.click
            });
            //child2.Add(new BaseMenu
            //{
            //    key = "查快递状态",
            //    name = "查快递状态",
            //    type = MenuType.click
            //});
            //child2.Add(new BaseMenu
            //{
            //    key = "色觉辨认助手",
            //    name = "色觉辨认助手",
            //    type = MenuType.pic_photo_or_album
            //});
            child2.Add(new BaseMenu
            {
                url = "https://mp.weixin.qq.com/s/Sy2m02ACaJ3rlGYVqLL_FQ",
                name = "CaptureToText",
                type = MenuType.view
            });
            child2.Add(new BaseMenu
            {
                url = "https://mp.weixin.qq.com/s?__biz=MzIwODYwOTU3OA==&mid=2247484308&idx=1&sn=70dd872ef51f79a8e8c1bd13f427bf4e&chksm=9701cbd1a07642c7b2a37cb2cb6a5df3adc1ef93a5d6ddfa75bb7a0d50391963aa8da66f701c&scene=38#wechat_redirect",
                name = "相同的行程查询",
                type = MenuType.view
            });
            //child3.Add(new BaseMenu
            //{
            //    url = "https://gitiden.gitee.io/qrc/",
            //    name = "加个微信",
            //    type = MenuType.view
            //});
            //child3.Add(new BaseMenu
            //{
            //    key = "探索火星",
            //    name = "探索火星",
            //    type = MenuType.click
            //});
            //child3.Add(new BaseMenu
            //{
            //    url = "https://mp.weixin.qq.com/mp/homepage?__biz=MzIwODYwOTU3OA==&hid=9&sn=6b865c4a6093bb0ca1bb869c682ac600",
            //    name = "归档",
            //    type = MenuType.view
            //});
            child3.Add(new BaseMenu
            {
                url = "https://github.com/KakaWanYifan",
                name = "我的 Github",
                type = MenuType.view
            });
            child3.Add(new BaseMenu
            {
                url = "http://mp.weixin.qq.com/s/UNmtIDG8S6PgQn9fQMqJTw",
                name = "卡卡金融计算器",
                type = MenuType.view
            });
            child3.Add(new BaseMenu
            {
                url = "https://mp.weixin.qq.com/s/qLTQA1g5rHMP9htF_bFYKw",
                name = "卡卡无线电刷题",
                type = MenuType.view
            });
            //child3.Add(new BaseMenu
            //{
            //    url = "https://mp.weixin.qq.com/s/RSU-9tUKVB6gvcFWKw_wcw",
            //    name = "卡卡豆瓣高分电影",
            //    type = MenuType.view
            //});
            //child3.Add(new BaseMenu
            //{
            //    url = "https://mp.weixin.qq.com/s/TtarqsahyA4ORzWaIiJu5A",
            //    name = "百度人工智能大赛",
            //    type = MenuType.view
            //});
            //child3.Add(new BaseMenu
            //{
            //    url = "https://mp.weixin.qq.com/s/jh3lEqdCM1NRNLeqhGddaw",
            //    name = "因缘幸会  遂得所图",
            //    type = MenuType.view
            //});
            basebtn.Add(new BaseMenu
            {
                name = "数学",
                sub_button = child1
            });
            basebtn.Add(new BaseMenu
            {
                name = "便捷",
                sub_button = child2
            });
            basebtn.Add(new BaseMenu
            {
                name = "更多",
                sub_button = child3
            });
            var ret = Menu.Create(new MenuEntity { button = basebtn }, accessToken);
            Response.Write(ret.ErrDescription);
        }
    }
}