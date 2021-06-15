﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _18TWENTY8.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Net;

namespace _18TWENTY8.Controllers
{
    public class HomeController : Controller
    {
        private readonly EighteentwentyeightContext _context;
      
        public HomeController(EighteentwentyeightContext context, IConfiguration Config)
        {
            _Configuration = Config;
            _context = context;
           
        }
        public IConfiguration _Configuration { get; }
        [BindProperty]
        public EMessage Messages { get; set; }
        public IActionResult Index()
        {
           

        List<object> listfor = new List<object>
            {  _context.Workshops.OrderBy(x=> x.fileorder).ToList(),
                    _context.WorkshopsupportDocType.ToList(),
                    _context.Graduates.Where(x=> x.order==3 || x.order==8 || x.order==9 || x.order==1).ToList()


            };
            return View(listfor.ToList());
        
        }
        public IActionResult Directors()
        {
           
            return View();

        }
        public IActionResult Countdown()
        {

            return View();

        }
        public IActionResult Thankyoucontact()
        {

            return View();

        }
        public IActionResult StatusofWomen()
        {

            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(EMessage ems)
        {
            var body = "<p><strong>Email From:<strong/> {0} {1}</p><p><strong>Message:<strong/></p><p>{2}</p> <p><strong>Contact number:<strong/> </p><p>{3}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress("siyabulela@sinqe.co.za"));  // replace with valid value 
            message.From = new MailAddress("web@18twenty8.org");  // replace with valid value
            message.Subject = "Web Enquiry " + ems.Name;
            message.Body = string.Format(body, ems.From, ems.Fromemail, ems.content, ems.Contact );
            message.IsBodyHtml = true;


            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "Web@18twenty8.org",  // replace with valid value
                    Password = "Web4321!"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.18twenty8.org";
                smtp.Port = 587;
                smtp.EnableSsl = false;
                await smtp.SendMailAsync(message);

            }
            return RedirectToAction("Thankyoucontact");
        }


        public IActionResult RecognitionLetters()
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
