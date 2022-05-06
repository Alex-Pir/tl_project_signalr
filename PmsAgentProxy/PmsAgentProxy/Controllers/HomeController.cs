﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using PmsAgentProxy.Clients;
using PmsAgentProxy.Util;

namespace PmsAgentProxy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProxy _proxy;
        
        public HomeController(IProxy proxy)
        {
            _proxy = proxy;
        }
        
        [HttpPost]
        public async Task<XmlActionResult> SendData(string parameter)
        {
            try
            {
                var response = await _proxy.SendRequest(parameter);
                return new XmlActionResult(response);
            }
            catch (Exception ex)
            {
                //TODO добавить логирование
            }

            return new XmlActionResult("");
        }
    }
}