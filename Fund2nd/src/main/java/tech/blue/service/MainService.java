package tech.blue.service;

import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;
import org.springframework.stereotype.Service;

import java.util.HashMap;
import java.util.Map;

import org.jsoup.*;

/**
 * Created by Wan_Yifan on 2017/7/12.
 */
@Service
public class MainService
{
	private final String STOCKURL = "http://app.finance.ifeng.com/data/fund/zqmx.php?symbol=";
	private final String BONDURL = "http://app.finance.ifeng.com/data/fund/zhmx.php?symbol=";
	private final String CALLURL = "http://app.finance.ifeng.com/data/fund/mrmx.php?symbol=";
	private final String PUTURL = "http://app.finance.ifeng.com/data/fund/mcmx.php?symbol=";
	private final String INDURL = "http://app.finance.ifeng.com/data/fund/hymx.php?symbol=";
	private final String DISURL = "http://app.finance.ifeng.com/data/fund/zcfp.php?symbol=";

	public String GetFund(String FundId,String FundName)
	{
		String HTML = "";
		Map<String,Object> StockMap = GetHTML("stock",FundId);
		Map<String,Object> BondMap = GetHTML("bond",FundId);
		Map<String,Object> CallMap = GetHTML("call",FundId);
		Map<String,Object> PutMap = GetHTML("put",FundId);
		Map<String,Object> IduMap = GetHTML("idu",FundId);
		Map<String,Object> DisMap = GetHTML("dis",FundId);
		String Stock = "<p><b>" + FundName + "(" + FundId + ")重仓股明细" + "</b></p>\n"
				+ "<table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\"> "
				+ StockMap.get("table").toString()
				+ "</table>";
		String Bond =  "<p><b>" + FundName + "(" + FundId + ")债券明细" + "</b></p>\n"
				+ "<table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\"> "
				+ BondMap.get("table").toString()
				+ "</table>";
		String Call =  "<p><b>" + FundName + "(" + FundId + ")买入明细" + "</b></p>\n"
				+ "<table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\"> "
				+ CallMap.get("table").toString()
				+ "</table>";
		String Put =  "<p><b>" +  FundName + "(" + FundId + ")卖出明细" + "</b></p>\n"
				+ "<table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\"> "
				+ PutMap.get("table").toString()
				+ "</table>";
		String Idu =  "<p><b>" + FundName + "(" + FundId + ")行业明细" + "</b></p>\n"
				+ "<table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\"> "
				+ IduMap.get("table").toString()
				+ "</table>";
		String Dis =  "<p><b>" + FundName + "(" + FundId + ")资产分配" + "</b></p>\n"
				+ "<table width=\"100%\" border=\"1\" cellspacing=\"0\" cellpadding=\"0\"> "
				+ DisMap.get("table").toString()
				+ "</table>";
		HTML = "<!doctype html>" +
				"<html>" +
				"<head>" +
				"<title>" + FundName + "</title>" +
				"<style type=\"text/css\">body{ font-size:24px;}</style>" +
				"</head>" +
				"<body>" +
				"投资组合数据依次为：</br>" +
				"1、重仓股明细</br>" +
				"2、债券明细</br>" +
				"3、买入明细</br>" +
				"4、卖出明细</br>" +
				"5、行业明细</br>" +
				"6、资产分配</br>" +
				Stock + Bond + Call + Put + Idu + Dis +
				"<br/>" +
				"</body>" +
				"</html>";
		return  HTML;
	}

	//GetStock
	private Map<String,Object> GetHTML (String Type, String FundId)
	{
		String url = "";
		Map<String,Object> rnt = new HashMap<>();
		switch (Type.toLowerCase())
		{
			case "stock":
				url = STOCKURL + FundId;
				break;
			case "bond":
				url = BONDURL + FundId;
				break;
			case "call":
				url = CALLURL + FundId;
				break;
			case "put":
				url = PUTURL + FundId;
				break;
			case "idu":
				url = INDURL + FundId;
				break;
			case "dis":
				url = DISURL + FundId;
				break;
		}
		try {
			Document doc = Jsoup.connect(url).get();
			Element head = doc.head();
			Elements metas = head.select("meta");
			for (Element meta : metas)
			{
				if ("description".equalsIgnoreCase(meta.attr("name")))
				{
					rnt.put("des",meta.attr("content"));
				}
			}
			Elements Tables = doc.getElementsByTag("table");
			Element Table = Tables.get(0);
			Elements elements = Table.select("a[href]");
			for(Element el:elements)
			{
				el.removeAttr("href");
			}
			rnt.put("table",Table.html());
		}
		catch (Exception e)
		{

		}
		return  rnt;
	}
}
