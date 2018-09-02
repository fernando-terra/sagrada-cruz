using br.com.sagradacruz.DAO;
using Microsoft.AspNetCore.Mvc;

namespace br.com.sagradacruz.Controllers
{
    public class MembersController : Controller
    {
        public IActionResult Index()
        {
            var dAO = new MembersDAO();
            var members = dAO.GetMembers();

            return View(members);
        }
    }
}