using br.com.sagradacruz.DAO;
using br.com.sagradacruz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace br.com.sagradacruz.Controllers
{
    public class StatementController : Controller
    {
        StatementDAO dAO = new StatementDAO();

        public IActionResult Index(bool flowResult = false)
        {
            ViewBag.flowResult = flowResult;
            var statements = dAO.GetStatements();
            return View(statements);
        }

        public IActionResult New(StatementViewModel statement)
        {
            var strSession = Utils.ConvertBytesToString(HttpContext.Session.Get("SESSION"));
            if (string.IsNullOrEmpty(strSession))
            {
                return RedirectToAction("Index", "Home");
            }
            var session = JsonConvert.DeserializeObject<ActiveSession>(strSession);
            if (session.Status)
            {
                var result = dAO.CreateStatement(statement);
                return Json(result);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Flow(int id, bool approved)
        {
            var result = dAO.ApproveStatement(id, approved);
            return Json(result);
        }
    }
}