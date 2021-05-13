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
    public class BigSisterDetailsController : Controller
    {
        private readonly EighteentwentyeightContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _host;
   
        public BigSisterDetailsController(IHostingEnvironment host, EighteentwentyeightContext context, UserManager<ApplicationUser> userManager, IConfiguration Config)
        {
            _Configuration = Config;
               _userManager = userManager;
            _context = context;
            _host = host;
        }
        public IConfiguration _Configuration { get; }
        public IActionResult IncorrectCode(int? id)
        {
            ViewBag.ID = id;

            return View();
        }
        public async Task<IActionResult> Verifytwof(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var bigSisterDetail = await _context.BigSisterDetail.FindAsync(id);
            if (bigSisterDetail == null)
            {
                return NotFound();
            }
            return View(bigSisterDetail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verifytwof(int id, BigSisterDetail bigSisterDetail)
        {
            


            if (id != bigSisterDetail.BigSisterDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(bigSisterDetail.VerifCodeComp==bigSisterDetail.VerifCode)
                {
                    bigSisterDetail.verifiedRegistration = "Yes";
                    bigSisterDetail.ProfileStatusID = 4;
                    bigSisterDetail.profilestatusreason = "Your profile is going through a review and vetting process with our administrators";


                    try
                {
                    _context.Update(bigSisterDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BigSisterDetailExists(bigSisterDetail.BigSisterDetailID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                    return RedirectToAction("Details", new { id = bigSisterDetail.UserID });
                }
                else
                {
                    ViewBag.Status = "Incorrect Code please try again";

                    return RedirectToAction("IncorrectCode", new { id = id });

                }
            }
            return View(bigSisterDetail);
        }

        // GET: BigSisterDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.BigSisterDetail.ToListAsync());
        }

        // GET: BigSisterDetails/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
                return NotFound();

            bool has = _context.SisterAssignment.Any(x => x.BigSisterID == id);


            var bigSisterDetail = await _context.BigSisterDetail.FirstOrDefaultAsync(m => m.UserID == id);
            ViewBag.EvMentor = _context.OptionalBool.Where(x => x.YesNoID == bigSisterDetail.EverbeenamentorQ).SingleOrDefault().Description;
            ViewBag.ProStatus = _context.ProfileStatus.Where(x => x.ProfileStatusID == bigSisterDetail.ProfileStatusID).SingleOrDefault().Description;
            ViewBag.Conv = _context.OptionalBool.Where(x => x.YesNoID == bigSisterDetail.ArrestedConvictedQ).SingleOrDefault().Description;



            ViewBag.data = _context.InformationofStorageBig.Where(x => x.UserID == bigSisterDetail.BigSisterDetailID).ToList();


            if (has == true)
            {
                // int sistID = _context.SisterAssignment.Where(x => x.BigSisterID == id).SingleOrDefault().AssignSisterStatusID;
                var sisterAssign = _context.SisterAssignment.FirstOrDefault(s => s.BigSisterID == id);
                int sistID = sisterAssign.AssignSisterStatusID;
                ViewBag.Sistatus = _context.AssignSisterStatus.Where(x => x.AssignSisterStatusID == sistID).SingleOrDefault().description;
                ViewBag.AssignSisterStatusID = sisterAssign.SisAssID;
                ViewBag.AssignedSisterStatus = "Pending Approval";
                var littleSister = _context.LittleSisterDetail.FirstOrDefault(l => l.UserID == sisterAssign.LittleSisterID);
                ViewBag.AssignedLittleSister = littleSister.Name + " " + littleSister.Surname;
                ViewBag.AssignedLittleSisterId = littleSister.UserID;
            }
            else
            {
                ViewBag.Sistatus = "Pending Approval";
            }
            if (bigSisterDetail == null)
            {
                return NotFound();
            }
            int ids = _context.BigSisterDetail.Where(x => x.UserID == id).SingleOrDefault().BigSisterDetailID;
            List<object> listfor = new List<object>
            {
            _context.BigSisterDetail.Where(x=>x.UserID==id).ToList(),

               _context.InformationInterest.ToList(),
                 _context.InformationofStorageBig.Where(x=> x.UserID==ids).ToList(),

                    _context.InteractionLevel.ToList(),
                          _context.OptionalBool.ToList(),
                              _context.BigSisterAcademic.Where(x=> x.BigSisterAcademicID==ids).ToList(),
                               _context.Province.ToList(),
                                   _context.AdditionalSupportBig.ToList(),
                               _context.AdditionalSupportStorageBig.Where(x=> x.UserID==ids).ToList()




            };
            return View(listfor.ToList());
        }

        public async Task<IActionResult> BigSisterProfile(string? id)
        {
            if (id == null)
                return NotFound();

            bool has = _context.SisterAssignment.Any(x => x.BigSisterID == id);


            var bigSisterDetail = await _context.BigSisterDetail.FirstOrDefaultAsync(m => m.UserID == id);
            ViewBag.EvMentor = _context.OptionalBool.Where(x => x.YesNoID == bigSisterDetail.EverbeenamentorQ).SingleOrDefault().Description;
            ViewBag.ProStatus = _context.ProfileStatus.Where(x => x.ProfileStatusID == bigSisterDetail.ProfileStatusID).SingleOrDefault().Description;
            ViewBag.Conv = _context.OptionalBool.Where(x => x.YesNoID == bigSisterDetail.ArrestedConvictedQ).SingleOrDefault().Description;



            ViewBag.data = _context.InformationofStorageBig.Where(x => x.UserID == bigSisterDetail.BigSisterDetailID).ToList();


            if (has == true)
            {
                int sistID = _context.SisterAssignment.Where(x => x.BigSisterID == id).SingleOrDefault().AssignSisterStatusID;
                ViewBag.Sistatus = _context.AssignSisterStatus.Where(x => x.AssignSisterStatusID == sistID).SingleOrDefault().description;
            }
            else
            {
                ViewBag.Sistatus = "Pending Approval";

            }
            if (bigSisterDetail == null)
            {
                return NotFound();
            }
            int ids = _context.BigSisterDetail.Where(x => x.UserID == id).SingleOrDefault().BigSisterDetailID;
            List<object> listfor = new List<object>
            {
            _context.BigSisterDetail.Where(x=>x.UserID==id).ToList(),

               _context.InformationInterest.ToList(),
                 _context.InformationofStorageBig.Where(x=> x.UserID==ids).ToList(),

                    _context.InteractionLevel.ToList(),
                          _context.OptionalBool.ToList(),
                              _context.BigSisterAcademic.Where(x=> x.BigSisterAcademicID==ids).ToList(),
                               _context.Province.ToList(),
                                   _context.AdditionalSupportBig.ToList(),
                               _context.AdditionalSupportStorageBig.Where(x=> x.UserID==ids).ToList()




            };
            return View(listfor.ToList());
        }

        // GET: BigSisterDetails/Create
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

            bool has = _context.BigSisterDetail.Any(x => x.UserID == userId);
            if (has == true)
            {
                return RedirectToAction("Details", new { id = userId});
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

                var bg = new BigSisterDetail()
                {
                    Infosbig = listint,
                    Addsupbig = supdoc
                };
           

            return View(bg);
            }
        }
        

        // POST: BigSisterDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BigSisterDetail bgs, IFormFile CV, IFormFile CID, IFormFile pc, IEnumerable<IFormFile> QA)
        {
            string path_Root1 = _host.WebRootPath;
 
            string unq1;
            String unq2;
            var filePath = Path.GetTempFileName();


            var infoint = bgs.Infosbig.Where(x => x.Selected).Select(y => y.Value);
            var adsupp = bgs.Addsupbig.Where(x => x.Selected).Select(y => y.Value);
            var Bigsister = new BigSisterDetail()
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
                howdidyouhearabout = bgs.howdidyouhearabout,
                BackgroundQ = bgs.BackgroundQ,
                EverbeenamentorQ = bgs.EverbeenamentorQ,
                DetailsOnEverbeenMentorQ = bgs.DetailsOnEverbeenMentorQ,
                ArrestedConvictedQ = bgs.ArrestedConvictedQ,
                DetailsArrestedConvictedQ = bgs.DetailsArrestedConvictedQ,
                Province = bgs.Province,
                Interactionlevelmeet = bgs.Interactionlevelmeet,
                InteractionlevelDigCom = bgs.InteractionlevelDigCom,
                prefferedstudentdetails = bgs.prefferedstudentdetails,
                youngerselfQ = bgs.youngerselfQ,
                Expectationsonlittlesister = bgs.Expectationsonlittlesister,
                ConfirmMentordurationQ = bgs.ConfirmMentordurationQ,
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


            unq2 = "CV"  + Guid.NewGuid() + CV.FileName;
            string pathtofile = path_Root1 + "\\Uploads\\CV\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofile))

            {
                CV.CopyTo(fs);
                fs.Flush();
            }
            Bigsister.CVUrl = unq2;


            unq2 = "CID" + Guid.NewGuid() + CID.FileName;
            string pathtofiled = path_Root1 + "\\Uploads\\CertifiedID\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofiled))

            {
                CID.CopyTo(fs);
                fs.Flush();
            }

            Bigsister.CertifiedID = unq2;

            unq2 = "pc"+Guid.NewGuid() +pc.FileName;
            string pathtofileds = path_Root1 + "\\Uploads\\ProimagesBigsis\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofileds))

            {
                pc.CopyTo(fs);
                fs.Flush();
            }

            Bigsister.Imageurl = unq2;
            if (ModelState.IsValid)
            {
                Bigsister.ProfileStatusID = 1;
                _context.Add(Bigsister);
                await _context.SaveChangesAsync();
                int ids = Bigsister.BigSisterDetailID;
                Bigsister.VerifCode = "18t" + Bigsister.Surname.Substring(1,2) + ids;
                Bigsister.verifiedRegistration = "No";
                _context.Update(Bigsister);
                await _context.SaveChangesAsync();
                String number = "+27" + Bigsister.Phonenumber.Substring(1);
                string accountSid = _Configuration.GetSection("TwilioApp").GetValue<string>("ACCOUNT_SID");
                string authToken = _Configuration.GetSection("TwilioApp").GetValue<string>("AuthToken");

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "Thank you on your application for the Big Sister program, your verification code is " + Bigsister.VerifCode + " please enter the code on the verification page to complete your registration",
                    from: new Twilio.Types.PhoneNumber("+17605482821"),
                    to: new Twilio.Types.PhoneNumber(number)
                );

                foreach (var id in infoint)
                {
                    _context.InformationofStorageBig.Add(new InformationofStorageBig()
                    {
                        InformationofInterestID = int.Parse(id),
                        DateCreated = DateTime.Now,
                        UserID = ids


                    });

                }

                foreach (var id in adsupp)
                {
                    _context.AdditionalSupportStorageBig.Add(new AdditionalSupportStorageBig()
                    {
                        AdditionalSupportBigID = int.Parse(id),
                        DateCreated = DateTime.Now,
                        UserID = ids


                    });
                    await _context.SaveChangesAsync();
                  
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
                        _context.BigSisterAcademic.Add(new BigSisterAcademic()
                        {
                            BigSisterUserID = Bigsister.BigSisterDetailID,
                            DateCreated = DateTime.Now,
                            QualificationDocname = formFile.FileName,
                            Qualificationurl = unq1


                        }); ;
                        await _context.SaveChangesAsync();
                      
                    }
                   
                }
                return RedirectToAction("Verifytwof", new { id = Bigsister.BigSisterDetailID });




            }
            ViewBag.Option = new SelectList(_context.OptionalBool, "YesNoID", "Description");
            ViewBag.Intlevel = new SelectList(_context.InteractionLevel, "InteractionLevelID", "Description");
            return View(bgs);
        }

        // GET: BigSisterDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bigSisterDetail = await _context.BigSisterDetail.FindAsync(id);
            if (bigSisterDetail == null)
            {
                return NotFound();
            }
            return View(bigSisterDetail);
        }

        // POST: BigSisterDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BigSisterDetailID,Name,Surname,Nickname,IDPassport,DateofBirth,email,Phonenumber,physicaladdress,AlternateContact,howdidyouhearabout,BackgroundQ,EverbeenamentorQ,DetailsOnEverbeenMentorQ,ArrestedConvictedQ,DetailsArrestedConvictedQ,InformationofInterest,Interactionlevelmeet,InteractionlevelDigCom,prefferedstudentdetails,youngerselfQ,AdditionalSupport,Expectationsonlittlesister,ConfirmMentordurationQ,DateCreated")] BigSisterDetail bigSisterDetail)
        {
            if (id != bigSisterDetail.BigSisterDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bigSisterDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BigSisterDetailExists(bigSisterDetail.BigSisterDetailID))
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
            return View(bigSisterDetail);
        }

        // GET: BigSisterDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bigSisterDetail = await _context.BigSisterDetail
                .FirstOrDefaultAsync(m => m.BigSisterDetailID == id);
            if (bigSisterDetail == null)
            {
                return NotFound();
            }

            return View(bigSisterDetail);
        }

        // POST: BigSisterDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bigSisterDetail = await _context.BigSisterDetail.FindAsync(id);
            _context.BigSisterDetail.Remove(bigSisterDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BigSisterDetailExists(int id)
        {
            return _context.BigSisterDetail.Any(e => e.BigSisterDetailID == id);
        }
    }
}
