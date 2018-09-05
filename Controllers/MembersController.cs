using br.com.sagradacruz.DAO;
using br.com.sagradacruz.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace br.com.sagradacruz.Controllers
{
    public class MembersController : Controller
    {
        public IActionResult Index()
        {
            var dAO = new MembersDAO();
            var members = new List<Member>();

            try
            {
                members = dAO.GetMembers();
            }
            catch(Exception ex)
            {
                members = new List<Member>();
            }

            return View(members);
        }
    }
}