using System;
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
        public IActionResult Index()
        {
            string accountSid = _Configuration.GetSection("TwilioApp").GetValue<string>("ACCOUNT_SID");
            string authToken = _Configuration.GetSection("TwilioApp").GetValue<string>("AuthToken");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Thank you on your application for the Little Sister program, your verification code is " + "test" + " please enter the code on the verification page to complete your registration",
                from: new Twilio.Types.PhoneNumber("+17605482821"),
                to: new Twilio.Types.PhoneNumber("+27762161569"));

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
