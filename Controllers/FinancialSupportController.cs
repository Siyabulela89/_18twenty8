using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _18TWENTY8.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

using _18TWENTY8.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using System.Text.RegularExpressions;
using System.Security.Claims;

using Microsoft.IdentityModel.Protocols;
using AutoMapper;

namespace _18TWENTY8.Controllers
{
    public class FinancialSupportController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHostingEnvironment _host;
        private readonly EighteentwentyeightContext _context;
        public FinancialSupportController(RoleManager<ApplicationRole> roleManager
          , UserManager<ApplicationUser> userManager
          ,IHostingEnvironment host
          , EighteentwentyeightContext context, IConfiguration Config, IMapper mapper)
        {
            _Configuration = Config;
            _context = context;
          
            this.roleManager = roleManager;
            this.userManager = userManager;
            _host = host;

        }
        public IConfiguration _Configuration { get; }
        // GET: FinancialSupport
        public ActionResult Index()
        {
            return View();
        }

        // GET: FinancialSupport/Details/5


        // GET: FinancialSupport/Create
        public ActionResult Create(string email, string userId)
        {
            ViewBag.UserID = userId;
            ViewBag.email = email;

            bool has = _context.FinancialSupport.Any(x => x.UserID == userId);
            if (has == true)
            {
                return RedirectToAction("Details", new { id = userId });
            }
            else

            {
                return View();
            }
        }
        public IActionResult IncorrectCode(int? id)
        {
            ViewBag.ID = id;

            return View();
        }
        // POST: FinancialSupport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FinancialSupport fins, IFormFile CV, IFormFile CID, IFormFile pc, IFormFile POR,IFormFile QA, IFormFile QA2, IFormFile CVV)
        {
            string path_Root1 = _host.WebRootPath;

            string unq1;
            String unq2;
            var filePath = Path.GetTempFileName();
            var FinancialS = new FinancialSupport()
            {
                Name = fins.Name,
                Surname = fins.Surname,
                Nickname = fins.Nickname,
                IdNr_Passport = fins.IdNr_Passport,
                DOB = fins.DOB,
                Email = fins.Email,
                CellphoneNr = fins.CellphoneNr,

                ApplicationStatusID = 1,
        
                DateCreated = DateTime.Now,
         
                UserID = fins.UserID




            };
            unq2 = "CV" + Guid.NewGuid() + CV.FileName;
            string pathtofile = path_Root1 + "\\Uploads\\CV\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofile))

            {
                CV.CopyTo(fs);
                fs.Flush();
            }
            FinancialS.CVurl = unq2;


            unq2 = "CID" + Guid.NewGuid() + CID.FileName;
            string pathtofiled = path_Root1 + "\\Uploads\\CertifiedID\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofiled))

            {
                CID.CopyTo(fs);
                fs.Flush();
            }

            FinancialS.CertifiedID = unq2;
            unq2 = "pc" + Guid.NewGuid() + pc.FileName;
            string pathtofileds = path_Root1 + "\\Uploads\\ProimagesLilsis\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofileds))

            {
                pc.CopyTo(fs);
                fs.Flush();
            }

            FinancialS.Imgurl = unq2;
            unq2 = "Vid" + Guid.NewGuid() + CVV.FileName;
            string pathtofiledss = path_Root1 + "\\Uploads\\CVVideo\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofiledss))

            {
                pc.CopyTo(fs);
                fs.Flush();
            }

            FinancialS.VideoURl = unq2;
            unq2 = "Fee" + Guid.NewGuid() + QA2.FileName;
            string pathtofiledsfs = path_Root1 + "\\Uploads\\FeeStatements\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofiledsfs))

            {
                pc.CopyTo(fs);
                fs.Flush();
            }

            FinancialS.LatestStatementfees = unq2;
            unq2 = "POR" + Guid.NewGuid() + POR.FileName;
            string pathtofiledsfss = path_Root1 + "\\Uploads\\POR\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofiledsfss))

            {
                pc.CopyTo(fs);
                fs.Flush();
            }

            FinancialS.Proofofregoistrationurl = unq2;
            if (ModelState.IsValid)
            {
             
                _context.Add(FinancialS);
                await _context.SaveChangesAsync();
                int ids = FinancialS.FinancialSupportID;
                FinancialS.VerifCode = "18t" + FinancialS.Surname.Substring(1, 2) + ids;
                FinancialS.verifiedRegistration = "No";
                _context.Update(FinancialS);
                await _context.SaveChangesAsync();
                String number = "+27" + FinancialS.CellphoneNr.Substring(1);
                string accountSid = _Configuration.GetSection("TwilioApp").GetValue<string>("ACCOUNT_SID");
                string authToken = _Configuration.GetSection("TwilioApp").GetValue<string>("AuthToken");

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Thank you on your application for the financial assistance program, your verification code is " + FinancialS.VerifCode + " please enter the code on the verification page to complete your registration",
                    from: new Twilio.Types.PhoneNumber("+17605482821"),
                    to: new Twilio.Types.PhoneNumber(number)
                );




                return RedirectToAction("Verifytwof", new { id = FinancialS.FinancialSupportID });




            }
            try
                {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Details(string id)
        {


            List<object> listfor = new List<object>
            {  _context.FinancialSupport.Where(x=> x.UserID==id).ToList(),
                    _context.ApplicationStatus.ToList()


            };
            return View(listfor.ToList());

        }
        public async Task<IActionResult> Verifytwof(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var financialS = await _context.FinancialSupport.FindAsync(id);
            if (financialS == null)
            {
                return NotFound();
            }
            return View(financialS);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verifytwof(int id, FinancialSupport financialSupport)
        {



            if (id != financialSupport.FinancialSupportID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (financialSupport.VerifCodeComp == financialSupport.VerifCode)
                {
                    financialSupport.verifiedRegistration = "Yes";
                    financialSupport.ApplicationStatusID = 3;
                    financialSupport.ApplicationReason = "Your profile is going through a review and vetting process with our administrators";


                    try
                    {
                        _context.Update(financialSupport);
                       
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!financialSupportExists(financialSupport.FinancialSupportID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Details", new { id = financialSupport.UserID });
                }
                else
                {
                    ViewBag.Status = "Incorrect Code please try again";

                    return RedirectToAction("IncorrectCode", new { id = id });

                }
            }
            return View(financialSupport);
        }

        private bool financialSupportExists(int financialSupportID)
        {
            throw new NotImplementedException();
        }

        // GET: FinancialSupport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FinancialSupport/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FinancialSupport fs)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialSupport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FinancialSupport/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FinancialSupport fs)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}