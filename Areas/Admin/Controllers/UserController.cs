using br.com.sagradacruz.Areas.Admin.DAO;
using br.com.sagradacruz.Areas.Admin.Models;
using br.com.sagradacruz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace br.com.sagradacruz.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        UserDAO dAO = new UserDAO();

        public IActionResult Index()
        {
            var strSession = Utils.ConvertBytesToString(HttpContext.Session.Get("SESSION"));
            if (string.IsNullOrEmpty(strSession)) { return RedirectToAction("Index", "Home"); }
            var session = JsonConvert.DeserializeObject<ActiveSession>(strSession);
            if (session.Status) { return View(); }
            else { return RedirectToAction("Index", "Home"); }
        }

        [HttpPost]
        public IActionResult New(User user)
        {
            dAO.CreateUser(user);

            return View();
        }
    }
}