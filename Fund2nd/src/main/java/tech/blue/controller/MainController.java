package tech.blue.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;
import tech.blue.service.MainService;


/**
 * Created by Wan_Yifan on 2017/7/12.
 */
@Controller
public class MainController
{
	@Autowired
	private MainService ms;

	@RequestMapping(value = "/fund/{id}/{name}", method = RequestMethod.GET)
	public @ResponseBody String ShowFund(@PathVariable("id") String FundId,@PathVariable("name") String FundName)
	{
		String HTML = ms.GetFund(FundId,FundName);
		return HTML;
	}


}
