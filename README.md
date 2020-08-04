> 公众号已关闭，不再维护。

# BlueYifan-WeChatOfficialAccount
【早期的，代码很烂】微信公众号“BlueYifan”的源代码

该项目大概从2016年10月开始，断断续续的进行维护开发。之前都在我自己搭建的SVN上，现在移到Git上。

有一说一，代码很烂。

就像玩具一样

![](http://wanyifan.cn/github/BlueYifan-WeChatOfficialAccount.png)

## Fund2nd
Java写的，web应用，用户访问后，请求凤凰网的基金投资组合数据，并返回。

## InvalidButUseful
C#写的，一些小工具，主要是读取CSV、JSON、TXT的数据，插入到MySQL。

## RefreshData
C#写的，刷新数据的程序，请求基金净值数据，然后插入到MySQL。在2020年，年初，又修改了程序，新增一个请求疫情行程数据，然后上传到腾讯云开发数据库。

## SONG
C#写的，生成宋词的程序，没什么意思，其实就是讲一些宋词高频词汇，按照词牌格式，随机进行组合

## StockData
C#写的，请求股票的行情数据，并根据余弦函数求这些股票与沪深300成分股的相似度。非要说有什么特点的话，就是为了保证行情数据不丢失，在拉取到实时行情后，立马写文件到本地，然后另一个线程去读文件，再存入数据库。

## WeChat
C#写的，微信公众号的后台程序

## Widget/MarsPic
C#写的，请求NASA的火星照片的API，然后把结果插入数据库。NASA的火星车好奇号，几乎每天都会都会拍摄火星地表的照片，然后传回地球，NASA会选择一些公开，并提供API接口。
