using Microsoft.AspNetCore.Mvc;
using br.com.sagradacruz.DAO;
using br.com.sagradacruz.Models;

namespace br.com.sagradacruz.Controllers
{
    public class LoginController : Controller
    {      
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
            if (ModelState.IsValid)
            {
                if (dAO.Login(model))
                {
                    return View("Index", model); //RedirectToAction("Index", "Post", new { area = "Admin" });
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