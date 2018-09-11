using br.com.sagradacruz.DAO;
using br.com.sagradacruz.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace br.com.sagradacruz.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var statementDAO = new StatementDAO();
            var statements = new List<StatementViewModel>();

            try
            {                
                statements = statementDAO.GetStatements(true);
            }
            catch(Exception)
            {
                statements = new List<StatementViewModel>();
            }

            return View(statements);
        }
    }
}