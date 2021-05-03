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
using _18TWENTY8.Models.ViewModels.BigSister;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using _18TWENTY8.Models.ViewModels;
using System.Text.RegularExpressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using _18TWENTY8.Services;
using AutoMapper;

namespace _18TWENTY8.Controllers
{
    public class LittleSisterDetailsController : Controller
    {
        private readonly EighteentwentyeightContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _host;
    
        private readonly IMapper _mapper;
        private readonly SisterService _sisterService;
        public LittleSisterDetailsController(IHostingEnvironment host, EighteentwentyeightContext context, UserManager<ApplicationUser> userManager, IConfiguration Config, IMapper mapper)
        {
            _Configuration = Config;
            _userManager = userManager;
            _context = context;

            this._mapper = mapper;
            _sisterService = new SisterService(_context, _mapper);
            _host = host;
        }
        public IConfiguration _Configuration { get; }
        // GET: LittleSisterDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.LittleSisterDetail.ToListAsync());
        }
        public IActionResult IncorrectCode(int? id)
        {
            ViewBag.ID = id;

            return View();
        }
        // GET: LittleSisterDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest($"Little Sister ID not provided");

            return View(await _sisterService.GetLittleSisterProfile(id));
        }

        // GET: LittleSisterDetails/Create
        public IActionResult Create()
        {
            string userId;
            if (User.Identity.IsAuthenticated)
            {
                userId = _userManager.GetUserId(User);
            }
            else

            {
                userId = "NOT FOUND";

            }

            bool has = _context.LittleSisterDetail.Any(x => x.UserID == userId);
            if (has == true)
            {
                return RedirectToAction("Details", new { id = userId });
            }
            else

            {

                var listint = _context.InformationInterest.Select(x => new SelectListItem()
                {
                    Text = x.Description,
                    Value = x.InformationofInterestID.ToString()
                }).ToList();
                var supdoc = _context.AdditionalSupportBig.Select(x => new SelectListItem()
                {
                    Text = x.Description,
                    Value = x.AdditionalSupportBigID.ToString()
                }).ToList();
                List<InteractionLevel> listtime = new List<InteractionLevel>();
                listtime = _context.InteractionLevel.ToList();

                ViewBag.Option = new SelectList(_context.OptionalBool, "YesNoID", "Description");
                ViewBag.Intlevel = new SelectList(_context.InteractionLevel, "InteractionLevelID", "Description");
                ViewBag.Province = new SelectList(_context.Province, "ProvinceID", "Provincename");

                var bg = new LittleSisterDetail()
                {
                    Infosbig = listint,
                 
                };


                return View(bg);
            }

        }

        // POST: LittleSisterDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LittleSisterDetail bgs, IFormFile CV, IFormFile CID, IFormFile pc, IEnumerable<IFormFile> QA)
        {

            string path_Root1 = _host.WebRootPath;

            string unq1;
            String unq2;
            var filePath = Path.GetTempFileName();


            var infoint = bgs.Infosbig.Where(x => x.Selected).Select(y => y.Value);

            var LittleSister = new LittleSisterDetail()
            {
                Name = bgs.Name,
                Surname = bgs.Surname,
                Nickname = bgs.Nickname,
                IDPassport = bgs.IDPassport,
                DateofBirth = bgs.DateofBirth,
                email = bgs.email,
                Phonenumber = bgs.Phonenumber,
                Interactionlevelmeetother = bgs.Interactionlevelmeetother,
                InteractionlevelDigComother = bgs.InteractionlevelDigComother,
                howdidyouhearaboutQ = bgs.howdidyouhearaboutQ,
                BackgroundQ = bgs.BackgroundQ,
                CurrentStudyQ = bgs.CurrentStudyQ,
                EverbeenamenteeQ = bgs.EverbeenamenteeQ,
                DetailsOnEverbeenMenteeQ = bgs.DetailsOnEverbeenMenteeQ,
                ArrestedConvictedQ = bgs.ArrestedConvictedQ,
                DetailsArrestedConvictedQ = bgs.DetailsArrestedConvictedQ,
                Province = bgs.Province,
                Interactionlevelmeet = bgs.Interactionlevelmeet,
                InteractionlevelDigCom = bgs.InteractionlevelDigCom,
                prefferedMenteedetails = bgs.prefferedMenteedetails,

                Expectationsonlittlesister = bgs.Expectationsonlittlesister,
                ConfirmMenteedurationQ = bgs.ConfirmMenteedurationQ,
                DateCreated = DateTime.Now,
                AddressStreet = bgs.AddressStreet,
                AddressStreetlinetwo = bgs.AddressStreetlinetwo,
                EmergencyContactNameone = bgs.EmergencyContactNameone,
                EmergencyContactNametwo = bgs.EmergencyContactNametwo,
                EmergencyContactNumberone = bgs.EmergencyContactNumberone,
                EmergencyContactNumbertwo = bgs.EmergencyContactNumbertwo,
                PostalCode = bgs.PostalCode,
                UserID = _userManager.GetUserId(User)




            };


            unq2 = "CV" + Guid.NewGuid() + CV.FileName;
            string pathtofile = path_Root1 + "\\Uploads\\CV\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofile))

            {
                CV.CopyTo(fs);
                fs.Flush();
            }
            LittleSister.CVurl = unq2;


            unq2 = "CID" + Guid.NewGuid() + CID.FileName;
            string pathtofiled = path_Root1 + "\\Uploads\\CertifiedID\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofiled))

            {
                CID.CopyTo(fs);
                fs.Flush();
            }

            LittleSister.CertifiedID = unq2;

            unq2 = "pc" + Guid.NewGuid() + pc.FileName;
            string pathtofileds = path_Root1 + "\\Uploads\\ProimagesLilsis\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofileds))

            {
                pc.CopyTo(fs);
                fs.Flush();
            }

            LittleSister.Imageurl = unq2;
            if (ModelState.IsValid)
            {
                LittleSister.ProfileStatusID = 3;
                _context.Add(LittleSister);
                await _context.SaveChangesAsync();
                int ids = LittleSister.LittleSisterDetailID;
                LittleSister.VerifCode = "18t" + LittleSister.Surname.Substring(1, 2) + ids;
                LittleSister.verifiedRegistration = "No";
                _context.Update(LittleSister);
                await _context.SaveChangesAsync();
                String number = "+27" + LittleSister.Phonenumber.Substring(1);
                string accountSid = _Configuration.GetSection("TwilioApp").GetValue<string>("ACCOUNT_SID");
                string authToken = _Configuration.GetSection("TwilioApp").GetValue<string>("AuthToken");

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Thank you on your application for the Little Sister program, your verification code is " + LittleSister.VerifCode + " please enter the code on the verification page to complete your registration",
                    from: new Twilio.Types.PhoneNumber("+17605482821"),
                    to: new Twilio.Types.PhoneNumber(number)
                );

                foreach (var id in infoint)
                {
                    _context.InformationofStorageLittle.Add(new InformationofStorageLittle()
                    {
                        InformationofInterestID = int.Parse(id),
                        DateCreated = DateTime.Now,
                        UserID = ids


                    });

                }

              
                foreach (var formFile in QA)
                {
                    if (formFile.Length > 0)
                    {
                        unq1 = "Qual" + Guid.NewGuid() + formFile.FileName;
                        string pathtofiles = path_Root1 + "\\Uploads\\Qualifications\\" + unq1;
                        using (FileStream fs = System.IO.File.Create(pathtofiles))

                        {
                            formFile.CopyTo(fs);
                            fs.Flush();
                        }
                        _context.LittleSisterAcademic.Add(new LittleSisterAcademic()
                        {
                            LittleSisterUserID = LittleSister.LittleSisterDetailID,
                            DateCreated = DateTime.Now,
                            QualificationDocname = formFile.FileName,
                            Qualificationurl = unq1


                        }); ;
                        await _context.SaveChangesAsync();

                    }

                }
                return RedirectToAction("Verifytwof", new { id = LittleSister.LittleSisterDetailID });




            }
            ViewBag.Option = new SelectList(_context.OptionalBool, "YesNoID", "Description");
            ViewBag.Intlevel = new SelectList(_context.InteractionLevel, "InteractionLevelID", "Description");
            return View(bgs);
        }
        public async Task<IActionResult> Verifytwof(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var littleSisterDetail = await _context.LittleSisterDetail.FindAsync(id);
            if (littleSisterDetail == null)
            {
                return NotFound();
            }
            return View(littleSisterDetail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verifytwof(int id, LittleSisterDetail LittleSisterDetail)
        {



            if (id != LittleSisterDetail.LittleSisterDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (LittleSisterDetail.VerifCodeComp == LittleSisterDetail.VerifCode)
                {
                    LittleSisterDetail.verifiedRegistration = "Yes";
                    LittleSisterDetail.ProfileStatusID = 5;
                    LittleSisterDetail.profilestatusreason = "Your profile is going through a review and vetting process with our administrators";


                    try
                    {
                        _context.Update(LittleSisterDetail);
                        _context.SisterAssignment.Add(new SisterAssignment()
                        {
                            BigApproveID = 4,
                            LittleApproveID = 4,
                            LittleSisterID = _userManager.GetUserId(User),
                         
                            DateCreated = DateTime.Now,
                            AssignSisterStatusID=1



                        }) ;
            
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!LittleSisterDetailExists(LittleSisterDetail.LittleSisterDetailID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Details", new { id = LittleSisterDetail.UserID });
                }
                else
                {
                    ViewBag.Status = "Incorrect Code please try again";

                    return RedirectToAction("IncorrectCode", new { id = id });

                }
            }
            return View(LittleSisterDetail);
        }
        // GET: LittleSisterDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var littleSisterDetail = await _context.LittleSisterDetail.FindAsync(id);
            if (littleSisterDetail == null)
            {
                return NotFound();
            }
            return View(littleSisterDetail);
        }

        // POST: LittleSisterDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LittleSisterDetailID,UserID,Name,Surname,Nickname,IDPassport,DateofBirth,email,Phonenumber,physicaladdress,AlternateContact,howdidyouhearaboutQ,CurrentStudyQ,BackgroundQ,EverbeenamenteeQ,EmergencyContactNameone,EmergencyContactNumberone,EmergencyContactNametwo,EmergencyContactNumbertwo,DetailsOnEverbeenMenteeQ,ArrestedConvictedQ,DetailsArrestedConvictedQ,InformationofInterest,Interactionlevelmeet,InteractionlevelDigCom,prefferedMenteedetails,CVurl,Imageurl,Expectationsonlittlesister,ConfirmMenteedurationQ,DateCreated")] LittleSisterDetail littleSisterDetail)
        {
            if (id != littleSisterDetail.LittleSisterDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(littleSisterDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LittleSisterDetailExists(littleSisterDetail.LittleSisterDetailID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(littleSisterDetail);
        }

        // GET: LittleSisterDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var littleSisterDetail = await _context.LittleSisterDetail
                .FirstOrDefaultAsync(m => m.LittleSisterDetailID == id);
            if (littleSisterDetail == null)
            {
                return NotFound();
            }

            return View(littleSisterDetail);
        }

        // POST: LittleSisterDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var littleSisterDetail = await _context.LittleSisterDetail.FindAsync(id);
            _context.LittleSisterDetail.Remove(littleSisterDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LittleSisterDetailExists(int id)
        {
            return _context.LittleSisterDetail.Any(e => e.LittleSisterDetailID == id);
        }
    }
}
