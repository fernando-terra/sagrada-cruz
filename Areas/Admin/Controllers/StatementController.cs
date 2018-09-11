using br.com.sagradacruz.DAO;
using br.com.sagradacruz.Models;
using Microsoft.AspNetCore.Mvc;

namespace br.com.sagradacruz.Controllers
{
    public class StatementController : Controller
    {
        StatementDAO dAO = new StatementDAO();

        public IActionResult Index()
        {
            var statements = dAO.GetStatements();
            return View(statements);
        }

        public IActionResult New(StatementViewModel statement)
        {
            return null;
        }

        public IActionResult Flow(bool opinion)
        {
            return null;
        }
    }
}