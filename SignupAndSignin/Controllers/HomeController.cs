using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignupAndSignin.Data;
using SignupAndSignin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignupAndSignin.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _Db;

        public HomeController(AppDbContext Db)
        {
            _Db = Db;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ThankYou() {
            ViewBag.Message = "Thank You for Registering";
            return View();
        }

        public IActionResult Add() { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(Users u) {
            try
            {
                if (ModelState.IsValid)
                {
                    _Db.Registrations.Add(u);
                    await _Db.SaveChangesAsync();
                    return RedirectToAction("ThankYou");
                }

                return View();
            }
            catch (Exception e) {
                return View(e);
            }
        }
        public IActionResult ShowUsers() {
            var userList = from u in _Db.Registrations
                           select new Users
                           {
                               id = u.id,
                               name = u.name,
                               email = u.email,
                               password = u.password

                           };
            return View(userList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
