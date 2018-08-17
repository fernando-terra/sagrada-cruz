using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using br.com.sagradacruz.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace br.com.sagradacruz.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {           
            return View();
        }
    }
}