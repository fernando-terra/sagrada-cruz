using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using br.com.sagradacruz.Models;

namespace br.com.sagradacruz.Areas.Admin.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            var strSession = Utils.ConvertBytesToString(HttpContext.Session.Get("SESSION"));
            var session = JsonConvert.DeserializeObject<ActiveSession>(strSession);

            if (session.Status)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("SESSION");

            return RedirectToAction("Index", "Home");
        }
    }
}