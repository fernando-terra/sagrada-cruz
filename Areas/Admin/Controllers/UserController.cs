using br.com.sagradacruz.Areas.Admin.DAO;
using br.com.sagradacruz.Areas.Admin.Models;
using br.com.sagradacruz.Helpers;
using br.com.sagradacruz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using static br.com.sagradacruz.Helpers.Enum;

namespace br.com.sagradacruz.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        UserDAO dAO = new UserDAO();

        [HttpGet]
        public IActionResult Index()
        {
            var strSession = Utils.ConvertBytesToString(HttpContext.Session.Get("SESSION"));
            if (string.IsNullOrEmpty(strSession))
            {
                return RedirectToAction("Index", "Home");
            }
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

        [HttpGet]
        public IActionResult List()
        {
            var strSession = Utils.ConvertBytesToString(HttpContext.Session.Get("SESSION"));
            if (string.IsNullOrEmpty(strSession))
            {
                return RedirectToAction("Index", "Home");
            }
            var session = JsonConvert.DeserializeObject<ActiveSession>(strSession);
            if (session.Status)
            {
                var users = dAO.GetUser();
                return View(users);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }            
        }

        public IActionResult Edit(int id)
        {
            var strSession = Utils.ConvertBytesToString(HttpContext.Session.Get("SESSION"));
            if (string.IsNullOrEmpty(strSession))
            {
                return RedirectToAction("Index", "Home");
            }
            var session = JsonConvert.DeserializeObject<ActiveSession>(strSession);
            if (session.Status)
            {
                var user = dAO.GetUser(id).First();
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }            
        }

        public IActionResult New()
        {
            var strSession = Utils.ConvertBytesToString(HttpContext.Session.Get("SESSION"));
            if (string.IsNullOrEmpty(strSession))
            {
                return RedirectToAction("Index", "Home");
            }
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

        public IActionResult Save(User user)
        {
            var strSession = Utils.ConvertBytesToString(HttpContext.Session.Get("SESSION"));
            if (string.IsNullOrEmpty(strSession))
            {
                return RedirectToAction("Index", "Home");
            }
            var session = JsonConvert.DeserializeObject<ActiveSession>(strSession);
            if (session.Status)
            {
                if(user.Id > 0)
                {
                    dAO.EditUser(user);
                    return RedirectToAction("List", "User", new { @area = "Admin" });
                }
                else
                {
                    dAO.CreateUser(user);
                    return RedirectToAction("List", "User", new { @area = "Admin" });
                }                
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Delete(int id)
        {
            dAO.RemoveUser(id);

            return RedirectToAction("List", "User", new { @area = "Admin" });
        }
    }
}