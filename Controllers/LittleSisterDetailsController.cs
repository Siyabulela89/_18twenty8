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
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Spire.Doc;
using _18TWENTY8.Models.ViewModels.LittleSister;

namespace _18TWENTY8.Controllers
{
    public class LittleSisterDetailsController : Controller
    {
        private readonly EighteentwentyeightContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _host;
        private readonly IMapper _mapper;
        private readonly SisterService _sisterService;
        public LittleSisterDetailsController(IHostingEnvironment host
            , EighteentwentyeightContext context
            , UserManager<ApplicationUser> userManager
            , IConfiguration Config, IMapper mapper)
        {
            _Configuration = Config;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _sisterService = new SisterService(_context, _mapper);
            _host = host;
        }
        public IConfiguration _Configuration { get; }
        // GET: LittleSisterDetails
        [Authorize(Roles = "Little Sister (Mentee)")]
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
        [Authorize(Roles = "Little Sister (Mentee)")]
        public async Task<IActionResult> Details(string id, string wel)
        {
            ViewBag.Wel = wel;
            if (string.IsNullOrEmpty(id))
                return BadRequest($"Little Sister ID not provided");

            return View(await _sisterService.GetLittleSisterProfile(id));
        }

        public async Task<IActionResult> LittleSisterProfile(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest($"Little Sister ID not provided");

            return View(await _sisterService.GetLittleSisterProfile(id));
        }

        // GET: LittleSisterDetails/Create
        [Authorize(Roles = "Little Sister (Mentee)")]
        public IActionResult Create(string email, string userId, string fullnames)
        {
            ViewBag.UserID = userId;
            ViewBag.email = email;
            ViewBag.fullname = fullnames;
            ViewBag.Errorid = "";
            ViewBag.Errorqa = "";
            ViewBag.Errorcv = "";
            ViewBag.Errorpc = "";

            bool hasl = _context.Loggedinbefore.Any(x => x.userId == userId);
            bool has = _context.LittleSisterDetail.Any(x => x.UserID == userId);
            if (has == true && hasl == true)
            {
                string wel = "Welcome back";
                return RedirectToAction("Details", new { id = userId, wel = wel });
            }
            else if (has == true)
            {
                string numb = _context.LittleSisterDetail.SingleOrDefault().Phonenumber;
                string sms = "Welcome " + fullnames + " and thank you for your completion of the 18twenty8 Little Sister (Mentee) registration. your profile will go through evaluation, and we will revert back to you as soon as possible upon successful approval";
                string returnurlcon = "LittleSisterDetails";
                string returnurlact = "Details";
                var Loggedin = new Loggedinbefore()
                {
                    userId = userId
                };
                _context.Add(Loggedin);
                _context.SaveChangesAsync();
                return RedirectToAction("Administration", "Welcome", new { id = userId, sms = sms, con = returnurlcon, act = returnurlact, number = numb });
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

                ViewBag.Option = new SelectList(_context.OptionalBool.OrderByDescending(x => x.YesNoID), "YesNoID", "Description");
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

        [Authorize(Roles = "Little Sister (Mentee)")]
        public async Task<IActionResult> Create(LittleSisterDetail bgs, IFormFile CV, IFormFile CID, IFormFile pc, IEnumerable<IFormFile> QA)
        {

            long filelentghCID = CID.Length / 1000000;
            long filelentghpc = pc.Length / 1000000;
            long filelentghCV = CV.Length / 1000000;
            ViewBag.UserID = bgs.UserID;
            ViewBag.email = bgs.email;
            ViewBag.fullname = bgs.Name;

            var id_date = bgs.IDPassport.Substring(4, 2);
            var id_month = bgs.IDPassport.Substring(2, 2);
            var id_year = bgs.IDPassport.Substring(0, 2);
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

            ViewBag.Option = new SelectList(_context.OptionalBool.OrderByDescending(x => x.YesNoID), "YesNoID", "Description");
            ViewBag.Intlevel = new SelectList(_context.InteractionLevel, "InteractionLevelID", "Description");
            ViewBag.Province = new SelectList(_context.Province, "ProvinceID", "Provincename");

            var fullDate = id_year + id_month + id_date;
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime DateOB = DateTime.ParseExact(fullDate, "yyMMdd", provider);
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
                DateofBirth = DateOB,
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
                otherhobbies = bgs.otherhobbies,
                PostalCode = bgs.PostalCode,
                UserID = bgs.UserID,

                Infosbig = listint




            };


            var extension = Path.GetExtension(pc.FileName).ToLower();
            if (extension == ".png" || extension == ".jpg" || extension == ".gif" || extension == ".jpeg" || extension == ".gif")
            {

            }
            else if (filelentghpc > 3)
            {
                ViewBag.Errorpc = "file too big";
                return View(LittleSister);

            }
            else
            {
                ViewBag.Errorpc = "incorrect image format, please note that we only accept (png, jpg, gif, and jpeg image file formats)";
                return View(LittleSister);

            }
            if (filelentghCID > 2)

            {

                ViewBag.Errorid = "uploaded file is above the required size of 2mb";
                return View(LittleSister);

            }
            else

            {

            }
            if (filelentghCV > 2)

            {

                ViewBag.Errorcv = "uploaded file is above the required size of 2mb";
                return View(LittleSister);

            }
            else

            {

            }

            foreach (var formFile in QA)
            {
                if (formFile.Length > 0)
                {
                    var extensionf = Path.GetExtension(formFile.FileName).ToLower();
                    if (extensionf == ".png" || extensionf == ".jpg" || extensionf == ".gif" || extensionf == ".jpeg" || extensionf == ".pdf" || extensionf == ".docx" || extensionf == ".doc")
                    {


                    }
                    else if ((formFile.Length / 1000000) > 2)

                    {
                        ViewBag.Errorqa = "One or more uploaded files are above the required size of 2mb";
                        return View(LittleSister);

                    }
                    else

                    {
                        ViewBag.Errorqa = "File format for qualifications is incorrect, we only accept qualifications in (pdf, word, png, jpg, jpeg) format";
                        return View(LittleSister);

                    }
                }

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
                LittleSister.CVurl = unq2;
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
                LittleSister.CVurl = newname;

            }
            else

            {
                ViewBag.Errorcv = "File format for the CV is incorrect, we only accept CVs in (pdf, word, png, jpg, jpeg) format";
                return View(LittleSister);

            }


            var extensionid = Path.GetExtension(CID.FileName).ToLower();
            if (extensionid == ".png" || extensionid == ".jpg" || extensionid == ".gif" || extensionid == ".jpeg" || extensionid == ".pdf")
            {
                unq2 = "CID" + Guid.NewGuid() + CID.FileName;
                string pathtofiled = path_Root1 + "\\Uploads\\CertifiedID\\" + unq2;
                using (FileStream fs = System.IO.File.Create(pathtofiled))

                {
                    CID.CopyTo(fs);
                    fs.Flush();
                }
                LittleSister.CertifiedID = unq2;
            }
            else if (extensionid == ".doc" || extensionid == ".docx")
            {
                unq2 = "CV" + Guid.NewGuid() + CID.FileName;
                string conv = path_Root1 + "\\Uploads\\Conv\\" + unq2;
                string newname = "ID" + Guid.NewGuid() + ".pdf";
                string pathtofiled = path_Root1 + "\\Uploads\\CertifiedID\\" + newname;


                using (FileStream fs = System.IO.File.Create(conv))

                {
                    CID.CopyTo(fs);
                    fs.Flush();
                }
                Document doc = new Document();
                doc.LoadFromFile(conv);
                doc.SaveToFile(pathtofiled, FileFormat.PDF);
                LittleSister.CertifiedID = newname;

            }
            else

            {
                ViewBag.Errorid = "File format for the Certified ID is incorrect, we only accept Certified IDs in (pdf, word, png, jpg, jpeg) format";
                return View(LittleSister);

            }



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
                LittleSister.VerifCode = "18t" + LittleSister.Name.Substring(1, 2) + ids;
                LittleSister.verifiedRegistration = "No";
                _context.Update(LittleSister);
                await _context.SaveChangesAsync();
                String number = "+27" + LittleSister.Phonenumber.Substring(1);
                string accountSid = _Configuration.GetSection("TwilioApp").GetValue<string>("ACCOUNT_SID");
                string authToken = _Configuration.GetSection("TwilioApp").GetValue<string>("AuthToken");

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: "The verification code to complete your 18twenty8 Little Sister registration is " + LittleSister.VerifCode,
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
                        var extensionf = Path.GetExtension(formFile.FileName).ToLower();
                        if (extensionf == ".png" || extensionf == ".jpg" || extensionf == ".gif" || extensionf == ".jpeg" || extensionf == ".pdf" || extensionf == ".docx" || extensionf == ".doc")
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
                        else

                        {
                            unq1 = "Qual" + Guid.NewGuid() + formFile.FileName;
                            string conv = path_Root1 + "\\Uploads\\Conv\\" + unq1;
                            string newname = "Qual" + Guid.NewGuid() + ".pdf";
                            string pathtofiled = path_Root1 + "\\Uploads\\Qualifications\\" + newname;


                            using (FileStream fs = System.IO.File.Create(conv))

                            {
                                formFile.CopyTo(fs);
                                fs.Flush();
                            }
                            Document doc = new Document();
                            doc.LoadFromFile(conv);
                            doc.SaveToFile(pathtofiled, FileFormat.PDF);
                            _context.LittleSisterAcademic.Add(new LittleSisterAcademic()
                            {
                                LittleSisterUserID = LittleSister.LittleSisterDetailID,
                                DateCreated = DateTime.Now,
                                QualificationDocname = formFile.FileName,
                                Qualificationurl = newname


                            }); ;
                            await _context.SaveChangesAsync();

                        }

                    }

                }
                return RedirectToAction("Verifytwof", new { id = LittleSister.LittleSisterDetailID });




            }
            ViewBag.Option = new SelectList(_context.OptionalBool.OrderByDescending(x => x.YesNoID), "YesNoID", "Description");
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
                    LittleSisterDetail.profilestatusreason = "We are reviewing your profile.";


                    try
                    {
                        _context.Update(LittleSisterDetail);
                        _context.SisterAssignment.Add(new SisterAssignment()
                        {
                            BigApproveID = 4,
                            LittleApproveID = 4,
                            LittleSisterID = _userManager.GetUserId(User),

                            DateCreated = DateTime.Now,
                            AssignSisterStatusID = 1



                        });

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
                    return RedirectToAction("Create", new { email = LittleSisterDetail.email, userId = LittleSisterDetail.UserID, fullnames = LittleSisterDetail.Name });
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
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Option = new SelectList(_context.OptionalBool.OrderByDescending(x => x.YesNoID), "YesNoID", "Description");
            ViewBag.Intlevel = new SelectList(_context.InteractionLevel, "InteractionLevelID", "Description");
            ViewBag.Province = new SelectList(_context.Province, "ProvinceID", "Provincename");

            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var profile = await _sisterService.GetLittleSisterProfile(id);
            var littleSisterDetail = await _context.LittleSisterDetail.FindAsync(profile.Profile.LittleSisterDetailID);
            if (littleSisterDetail == null)
                return NotFound();

            return View(littleSisterDetail);
        }

        // POST: LittleSisterDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(string id, [Bind("LittleSisterDetailID,UserID,Name,Surname,Nickname,IDPassport,DateofBirth,email,Phonenumber,physicaladdress,AlternateContact,howdidyouhearaboutQ,CurrentStudyQ,BackgroundQ,EverbeenamenteeQ,EmergencyContactNameone,EmergencyContactNumberone,EmergencyContactNametwo,EmergencyContactNumbertwo,DetailsOnEverbeenMenteeQ,ArrestedConvictedQ,DetailsArrestedConvictedQ,InformationofInterest,Interactionlevelmeet,InteractionlevelDigCom,prefferedMenteedetails,CVurl,Imageurl,Expectationsonlittlesister,ConfirmMenteedurationQ,DateCreated")] LittleSisterDetail littleSisterDetail)
        public async Task<IActionResult> Edit(string id, UpdateLittleSisterModel model)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            model.UserID = id;
            var littleSisterDetail = await _sisterService.UpdateLittleSisterProfile(model);
            if (!string.IsNullOrEmpty(littleSisterDetail.UserID))
                return RedirectToAction("Details", new { id = littleSisterDetail.UserID });

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
        [Authorize(Roles = "Little Sister (Mentee)")]
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
