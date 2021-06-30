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
using Microsoft.AspNetCore.Authorization;
using _18TWENTY8.Models.ViewModels.BursaryApplicant;
using System.Globalization;
using Spire.Doc;

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
        //[Authorize(Roles = "Bursary Applicant")]
        
             public ActionResult AvailableBursaries(string UserID)
        {






            List<object> listfor = new List<object>
            {     _context.BursaryApplication.Where(x=> x.ApplicationEndDate> DateTime.Now).ToList(),
            _context.BursaryApplicationCandidate.Where(x=> x.UserID==UserID).ToList()



            };

            ViewBag.UserID = UserID;
            return View(listfor.ToList());

        }

        [HttpPost]
        public async Task<IActionResult> CreateBursaryApp(string UserID, int BursID)
        {
            var BursaryApp = new BursaryApplicationCandidate
            {
                BursaryID = BursID,
                UserID = UserID,
                Status = 1,
                ReceiveUserID = UserID,
                DateApplied = DateTime.Now

            };
            if (ModelState.IsValid)
            {

                _context.Add(BursaryApp);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = UserID });


            }
            return RedirectToAction("AvailableBursaries", new { id = UserID });


        }
        public ActionResult Create(string email, string userId, string fullnames)
        {
            ViewBag.UserID = userId;
            ViewBag.email = email;
            ViewBag.fullname = fullnames;
            ViewBag.Errorid = "";
            ViewBag.Errorqa = "";
            ViewBag.Errorqa2 = "";
            ViewBag.Errorcv = "";
            ViewBag.Errorcvv = "";
            ViewBag.Errorpc = "";
            ViewBag.Errorpor = "";
            bool hasl = _context.Loggedinbefore.Any(x => x.userId == userId);
            bool has = _context.FinancialSupport.Any(x => x.UserID == userId);
            if (has == true && hasl == true)
            {
                string wel = "Welcome back";
                return RedirectToAction("Details", new { id = userId, wel = wel });
            }
            else if (has == true)
            {
                string numb = _context.FinancialSupport.Where(x=> x.UserID==userId).SingleOrDefault().CellphoneNr;
                string sms = "Welcome " + fullnames + " and thank you for your completion of the 18twenty8 Financial Application Support registration.";
                string returnurlcon = "FinancialSupport";
                string returnurlact = "Details";
                var Loggedin = new Loggedinbefore()
                {
                    userId = userId
                };
                _context.Add(Loggedin);
                _context.SaveChangesAsync();
                return RedirectToAction("WelcomeRegistration","Administration",  new { id = userId, sms = sms, con = returnurlcon, act = returnurlact, number= numb });
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
        [Authorize(Roles = "Bursary Applicant")]
        public async Task<IActionResult> Create(FinancialSupport fins, IFormFile CV, IFormFile CID, IFormFile pc, IFormFile POR,IFormFile QA, IFormFile QA2, IFormFile CVV)
        {

            Decimal filelentghCID = (CID.Length / 1000000);
            Decimal filelentghpc = pc.Length / 1000000;
            Decimal filelentghCV = CV.Length / 1000000;
            Decimal filelentghCVV = CVV.Length / 1000000;
            Decimal filelentghPOR = POR.Length / 1000000;
            Decimal filelentghQA = QA.Length / 1000000;
            Decimal filelentghQA2 = QA2.Length / 1000000;

            var id_date = fins.IdNr_Passport.Substring(4, 2);
            var id_month = fins.IdNr_Passport.Substring(2, 2);
            var id_year = fins.IdNr_Passport.Substring(0, 2);

            var fullDate = id_year + id_month + id_date;
            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime DateOB = DateTime.ParseExact(fullDate, "yyMMdd", provider);
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
                DOB = DateOB,
                Email = fins.Email,
                CellphoneNr = fins.CellphoneNr,

                ApplicationStatusID = 1,
        
                DateCreated = DateTime.Now,
         
                UserID = fins.UserID




            };

            var extension = Path.GetExtension(pc.FileName).ToLower();
            if (extension == ".png" || extension == ".jpg" || extension == ".gif" || extension == ".jpeg" || extension == ".gif")
            {
             
            }
            else
            {
                ViewBag.Errorpc = "incorrect image format, please note that we only accept (png, jpg, gif, and jpeg image file formats)";
                return View(FinancialS);
            }
            if (filelentghpc > 10)

            {

                ViewBag.Errorpc = "uploaded file is above the required size of 10mb";
                return View(FinancialS);

            }
            else

            {

            }
            if (filelentghCV > 10)

            {

                ViewBag.Errorcv = "uploaded file is above the required size of 10mb";
                return View(FinancialS);

            }
            else

            {

            }



            var extensionx = Path.GetExtension(CV.FileName).ToLower();
            if (extensionx == ".png" || extensionx == ".jpg" || extensionx == ".gif" || extensionx == ".jpeg" || extensionx == ".pdf")
            {

                unq2 = "CV" + Guid.NewGuid() + CV.FileName;
                string pathtofile = path_Root1 + "\\Uploads\\CV\\" + unq2;
                using (FileStream fs = System.IO.File.Create(pathtofile))

                {
                    CV.CopyTo(fs);
                    fs.Flush();
                }
                FinancialS.CVurl = unq2;
            }
            else if (extensionx == ".doc" || extensionx == ".docx")
            {
                unq2 = "CV" + Guid.NewGuid() + CV.FileName;
                string conv = path_Root1 + "\\Uploads\\Conv\\" + unq2;
                string newname = "CV" + Guid.NewGuid() + ".pdf";
                string pathtofile = path_Root1 + "\\Uploads\\CV\\" + newname;


                using (FileStream fs = System.IO.File.Create(conv))

                {
                    CV.CopyTo(fs);
                    fs.Flush();
                }
                Document doc = new Document();
                doc.LoadFromFile(conv);
                doc.SaveToFile(pathtofile, FileFormat.PDF);
                FinancialS.CVurl = newname;

            }
            else

            {
                ViewBag.Errorcv = "File format for the CV is incorrect, we only accept CVs in (pdf, word, png, jpg, jpeg) format";
                return View(FinancialS);

            }

            var extensionid = Path.GetExtension(CID.FileName).ToLower();
            if (extensionid == ".png" || extensionid == ".jpg" || extensionid == ".gif" || extensionid == ".jpeg" || extensionid == ".pdf")
            {
                unq2 = "CID" + Guid.NewGuid() + CID.FileName;
                string pathtofiledc = path_Root1 + "\\Uploads\\CertifiedID\\" + unq2;
                using (FileStream fs = System.IO.File.Create(pathtofiledc))

                {
                    CID.CopyTo(fs);
                    fs.Flush();
                }
                FinancialS.CertifiedID = unq2;
            }
            else if (extensionid == ".doc" || extensionid == ".docx")
            {
                unq2 = "ID" + Guid.NewGuid() + CID.FileName;
                string conv = path_Root1 + "\\Uploads\\Conv\\" + unq2;
                string newname = "ID" + Guid.NewGuid() + ".pdf";
                string pathtofiledc = path_Root1 + "\\Uploads\\CertifiedID\\" + newname;


                using (FileStream fs = System.IO.File.Create(conv))

                {
                   CID.CopyTo(fs);
                    fs.Flush();
                }
                Document doc = new Document();
                doc.LoadFromFile(conv);
                doc.SaveToFile(pathtofiledc, FileFormat.PDF);
                FinancialS.CertifiedID = newname;

            }
            else

            {
                ViewBag.Errorid = "File format for the Certified ID is incorrect, we only accept Certified IDs in (pdf, word, png, jpg, jpeg) format";
                return View(FinancialS);

            }


            var extensionids = Path.GetExtension(QA.FileName).ToLower();
            if (extensionids == ".png" || extensionids == ".jpg" || extensionids == ".gif" || extensionids == ".jpeg" || extensionids == ".pdf")
            {
                unq2 = "QAS" + Guid.NewGuid() + QA.FileName;
                string pathtofiledc = path_Root1 + "\\Uploads\\Qualifications\\" + unq2;
                using (FileStream fs = System.IO.File.Create(pathtofiledc))

                {
                    QA.CopyTo(fs);
                    fs.Flush();
                }
                FinancialS.Academictranscript = unq2;
            }
            else if (extensionids == ".doc" || extensionids == ".docx")
            {
                unq2 = "QAS" + Guid.NewGuid() + QA.FileName;
                string conv = path_Root1 + "\\Uploads\\Conv\\" + unq2;
                string newname = "QAS" + Guid.NewGuid() + ".pdf";
                string pathtofiledc = path_Root1 + "\\Uploads\\Qualifications\\" + newname;


                using (FileStream fs = System.IO.File.Create(conv))

                {
                    QA.CopyTo(fs);
                    fs.Flush();
                }
                Document doc = new Document();
                doc.LoadFromFile(conv);
                doc.SaveToFile(pathtofiledc, FileFormat.PDF);
                FinancialS.Academictranscript = newname;

            }
            else

            {
                ViewBag.Errorqa = "File format for the Qualifications is incorrect, we only accept Qualifications in (pdf, word, png, jpg, jpeg) format";
                return View(FinancialS);

            }

            var extensionidsq = Path.GetExtension(QA2.FileName).ToLower();
            if (extensionidsq == ".png" || extensionidsq == ".jpg" || extensionidsq == ".gif" || extensionidsq == ".jpeg" || extensionidsq == ".pdf")
            {
                unq2 = "QAS" + Guid.NewGuid() + QA2.FileName;
                string pathtofiledc = path_Root1 + "\\Uploads\\FeeStatements\\" + unq2;
                using (FileStream fs = System.IO.File.Create(pathtofiledc))

                {
                    QA2.CopyTo(fs);
                    fs.Flush();
                }
                FinancialS.LatestStatementfees = unq2;
            }
            else if (extensionidsq == ".doc" || extensionidsq == ".docx")
            {
                unq2 = "QAS" + Guid.NewGuid() + QA2.FileName;
                string conv = path_Root1 + "\\Uploads\\Conv\\" + unq2;
                string newname = "QAS" + Guid.NewGuid() + ".pdf";
                string pathtofiledc = path_Root1 + "\\Uploads\\FeeStatements\\" + newname;


                using (FileStream fs = System.IO.File.Create(conv))

                {
                    QA2.CopyTo(fs);
                    fs.Flush();
                }
                Document doc = new Document();
                doc.LoadFromFile(conv);
                doc.SaveToFile(pathtofiledc, FileFormat.PDF);
                FinancialS.LatestStatementfees = newname;

            }
            else

            {
                ViewBag.Errorqa2 = "File format for the fee statements is incorrect, we only accept files in (pdf, word, png, jpg, jpeg) format";
                return View(FinancialS);

            }

            var extensionidsqp = Path.GetExtension(POR.FileName).ToLower();
            if (extensionidsqp == ".png" || extensionidsqp == ".jpg" || extensionidsqp == ".gif" || extensionidsqp == ".jpeg" || extensionidsqp == ".pdf")
            {
                unq2 = "POR" + Guid.NewGuid() + POR.FileName;
                string pathtofiledc = path_Root1 + "\\Uploads\\POR\\" + unq2;
                using (FileStream fs = System.IO.File.Create(pathtofiledc))

                {
                    POR.CopyTo(fs);
                    fs.Flush();
                }
                FinancialS.Proofofregoistrationurl = unq2;
            }
            else if (extensionidsq == ".doc" || extensionidsq == ".docx")
            {
                unq2 = "POR" + Guid.NewGuid() + POR.FileName;
                string conv = path_Root1 + "\\Uploads\\Conv\\" + unq2;
                string newname = "POR" + Guid.NewGuid() + ".pdf";
                string pathtofiledc = path_Root1 + "\\Uploads\\POR\\" + newname;


                using (FileStream fs = System.IO.File.Create(conv))

                {
                    POR.CopyTo(fs);
                    fs.Flush();
                }
                Document doc = new Document();
                doc.LoadFromFile(conv);
                doc.SaveToFile(pathtofiledc, FileFormat.PDF);

                FinancialS.Proofofregoistrationurl  = newname;

            }
            else

            {
                ViewBag.Errorpor = "File format for proof of registration is incorrect, we only accept files in (pdf, word, png, jpg, jpeg) format";
                return View(FinancialS);

            }







   
            unq2 = "pc" + Guid.NewGuid() + pc.FileName;
            string pathtofileds = path_Root1 + "\\Uploads\\ProimagesLilsis\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofileds))

            {
                pc.CopyTo(fs);
                fs.Flush();
            }

            FinancialS.Imgurl = unq2;
            unq2 = CVV.FileName;


            if (CVV.Length > 0)
            {

                var extensionv = Path.GetExtension(CVV.FileName).ToLower();
                if ((extensionv == ".mpeg-4" || extensionv == ".mov" || extensionv == ".mp4" || extensionv == ".3gp"))
                {

                   
                        var filePaths = Path.Combine((path_Root1 + "\\Uploads\\CVVideo\\"), unq2);
                   

                    using (var stream = System.IO.File.Create(filePaths))
                    {
                        await CVV.CopyToAsync(stream);
                    }
                    FinancialS.VideoURl = unq2;
                }
                else

                {
                    ViewBag.Errorcvv = "The uploaded video is in the incorrect format please note we only accept mp4 and mpeg-4 video formats";
                    return View(FinancialS);

                }
            }

        
         

            if (ModelState.IsValid)
            {
             
                _context.Add(FinancialS);
                await _context.SaveChangesAsync();
                int ids = FinancialS.FinancialSupportID;
                FinancialS.VerifCode = "18t" + FinancialS.Name.Substring(1, 2) + ids;
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
        //[Authorize(Roles = "Bursary Applicant")]
        public IActionResult Details(string id, string wel)

        {
            ViewBag.Wel = wel;
         var finsupport  = _context.FinancialSupport.FirstOrDefault(x => x.UserID == id);
           
          var Appstatusprofile = _context.ApplicationStatus.FirstOrDefault(x => x.ApplicationStatusID == finsupport.ApplicationStatusID);
            var AppliedBurs = _context.BursaryApplicationCandidate.Where(x => x.UserID == id).ToList();

            //var BursarystatusaPP = _context.BursaryStatus.Where(x => x.BursaryStatusID == AppliedBurs.Result.);
            var Bursarylist = _context.BursaryApplication.Where(x => AppliedBurs.Any(y => y.BursaryID == x.BursaryID)).ToList();
            var BursaryStatusAppl = _context.BursaryStatus.ToList();

            var ViewmodelFinancesupport = new BursaryApplicantviewm
            {
                BursCand = AppliedBurs,
                ApplicationStatusProfile = Appstatusprofile,
                AppstatusBursary = BursaryStatusAppl,
                FinancialsS = finsupport,
                Bursarylist=Bursarylist

            };

            //List<object> listfor = new List<object>
            //{  

                //};
            return View(ViewmodelFinancesupport);

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
        //[Authorize(Roles = "Bursary Applicant")]
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
                    financialSupport.ApplicationStatusID = 4;
                    financialSupport.ApplicationReason = "Approved Profile";


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
                    return RedirectToAction("Create", new { email = financialSupport.Email, userId = financialSupport.UserID, fullnames = financialSupport.Name });
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