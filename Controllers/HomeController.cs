using br.com.sagradacruz.DAO;
using Microsoft.AspNetCore.Mvc;

namespace br.com.sagradacruz.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var statementDAO = new StatementDAO();
            var statements = statementDAO.GetStatements();

            return View(statements);
        }
    }
}