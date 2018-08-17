using Microsoft.AspNetCore.Mvc;
using br.com.sagradacruz.DAO;
using br.com.sagradacruz.Models;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json;
using System.Text;

namespace br.com.sagradacruz.Controllers
{
    public class LoginController : Controller
    {
        private Utils _utils = new Utils();

        [HttpGet]
        public ActionResult Index()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Access(LoginViewModel model)
        {
            var dAO = new LoginDAO();
            var session = new ActiveSession();

            if (ModelState.IsValid)
            {
                if (dAO.Login(model))
                {
                    session.Created = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    session.Name = model.User;
                    session.Status = true;
                    session.User = model.User;

                    HttpContext.Session.Set("SESSION", Utils.ConvertToByteArray(JsonConvert.SerializeObject(session)));
                    
                    return RedirectToAction("Index", "Manager", new { area = "Admin" });
                }
                else
                {
                    return View("Index", new LoginViewModel());
                }                
            }
            else
            {
                return View("Index", new LoginViewModel());
            }
        }
    }
}