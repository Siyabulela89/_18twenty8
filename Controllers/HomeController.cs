﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _18TWENTY8.Models;

namespace _18TWENTY8.Controllers
{
    public class HomeController : Controller
    {
        private readonly EighteentwentyeightContext _context;
      
        public HomeController(EighteentwentyeightContext context)
        {
            _context = context;
           
        }
        public IActionResult Index()
        {
            List<object> listfor = new List<object>
            {  _context.Workshops.ToList(),
                    _context.WorkshopsupportDocType.ToList()


            };
            return View(listfor.ToList());
        
        }
        public IActionResult Directors()
        {
           
            return View();

        }
        public IActionResult StatusofWomen()
        {

            return View();

        }

        public IActionResult VolunteerWork()
        {

            return View();

        }
        public IActionResult Graduates()
        {

            return View();

        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Donate()
        {
            return View();
        }
        public IActionResult SuccessfullDonation()
        {
            return View();
        }
        public IActionResult UnsuccesfullDonation()
        {
            return View();
        }
        public IActionResult BBBEE()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}