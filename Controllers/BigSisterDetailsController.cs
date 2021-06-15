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
using System.Drawing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

using _18TWENTY8.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using System.Text.RegularExpressions;
using System.Security.Claims;

using Microsoft.IdentityModel.Protocols;
using AutoMapper;
using _18TWENTY8.Models.ViewModels.BigSister;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Spire.Doc;

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
      
        [Authorize(Roles = "Big Sister (Mentor)")]
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
                    bigSisterDetail.profilestatusreason = "We are reviewing your profile.";


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
        [Authorize(Roles = "Big Sister (Mentor)")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.BigSisterDetail.ToListAsync());
        }



        [Authorize(Roles = "Big Sister (Mentor)")]// GET: BigSisterDetails/Details/5
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

            var assignments = new List<BigSisterAssignmentViewModel>();
            if (has == true)
            {
                // int sistID = _context.SisterAssignment.Where(x => x.BigSisterID == id).SingleOrDefault().AssignSisterStatusID;
                var bigSisterAssignments = _context.SisterAssignment.Where(s => s.BigSisterID == id).ToList();
                var sisterAssign = bigSisterAssignments.FirstOrDefault(); //_context.SisterAssignment.FirstOrDefault(s => s.BigSisterID == id);
                int sistID = sisterAssign.AssignSisterStatusID;
                ViewBag.Sistatus = _context.AssignSisterStatus.Where(x => x.AssignSisterStatusID == sistID).SingleOrDefault().description;
                ViewBag.AssignSisterStatusID = sisterAssign.SisAssID;
                ViewBag.AssignedSisterStatus = "Pending Approval";
                //var littleSister = _context.LittleSisterDetail.FirstOrDefault(l => l.UserID == sisterAssign.LittleSisterID);
                //ViewBag.AssignedLittleSister = littleSister.Name + " " + littleSister.Surname;
                //ViewBag.AssignedLittleSisterId = littleSister.UserID;

   
                foreach (var assign in bigSisterAssignments)
                {
                    var littleSisterDetail = _context.LittleSisterDetail.FirstOrDefault(l => l.UserID == assign.LittleSisterID);
        
                    var littleSisterFullname = littleSisterDetail.Name + " " + littleSisterDetail.Surname;
                    var bigAssign = new BigSisterAssignmentViewModel
                    {
                        AssignSisterStatusID = assign.SisAssID,
                        AssignBigApproveID = assign.BigApproveID,
                        AssignLittleApproveID=assign.LittleApproveID,
                        AssignedLittleSisterId =  assign.LittleSisterID,
                        AssignedLittleSister = littleSisterFullname

                    };

                    assignments.Add(bigAssign);
                   
                }


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
                               _context.AdditionalSupportStorageBig.Where(x=> x.UserID==ids).ToList(),
                               assignments




            };
            return View(listfor.ToList());
        }

        public async Task<IActionResult> BigSisterProfile(string? id)
        {
            if (id == null)
                return NotFound();

            bool has = _context.SisterAssignment.Any(x => x.BigSisterID == id);


            var bigSisterDetail = await _context.BigSisterDetail.FirstOrDefaultAsync(m => m.UserID == id);
            ViewBag.EvMentor = _context.OptionalBool.FirstOrDefault(x => x.YesNoID == bigSisterDetail.EverbeenamentorQ).Description;
            ViewBag.ProStatus = _context.ProfileStatus.FirstOrDefault(x => x.ProfileStatusID == bigSisterDetail.ProfileStatusID).Description;
            ViewBag.Conv = _context.OptionalBool.FirstOrDefault(x => x.YesNoID == bigSisterDetail.ArrestedConvictedQ).Description;



            ViewBag.data = _context.InformationofStorageBig.Where(x => x.UserID == bigSisterDetail.BigSisterDetailID).ToList();


            if (has == true)
            {
                var sisterAssignment = _context.SisterAssignment.FirstOrDefault(x => x.BigSisterID == id);
                int sistID = sisterAssignment.AssignSisterStatusID; //_context.SisterAssignment.Where(x => x.BigSisterID == id).SingleOrDefault().AssignSisterStatusID;
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
            int ids = _context.BigSisterDetail.FirstOrDefault(x => x.UserID == id).BigSisterDetailID;
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

        [Authorize(Roles = "Big Sister (Mentor)")]
        // GET: BigSisterDetails/Create
        public IActionResult Create(string email,string userId, string fullnames)

        {

            ViewBag.UserID = userId;
            ViewBag.email = email;
            ViewBag.fullname = fullnames;
            ViewBag.Errorid = "";
            ViewBag.Errorqa = "";
            ViewBag.Errorcv = "";
            ViewBag.Errorpc = "";


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
                    Addsupbig = supdoc,
                    UserID=userId
                };
           

            return View(bg);
            }
        }
        

        // POST: BigSisterDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Big Sister (Mentor)")]
        public async Task<IActionResult> Create(BigSisterDetail bgs, IFormFile CV, IFormFile CID, IFormFile pc, IEnumerable<IFormFile> QA)
        {
            long filelentghCID = CID.Length/1000000;
            long filelentghpc = pc.Length / 1000000;
            long filelentghCV = CV.Length / 1000000;

            ViewBag.UserID = bgs.UserID;
            ViewBag.email = bgs.email;
            ViewBag.fullname = bgs.Name;

            // get first 6 digits as a valid date
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

            var id_date = bgs.IDPassport.Substring(4, 2);
            var id_month = bgs.IDPassport.Substring(2, 2);
            var id_year = bgs.IDPassport.Substring(0, 2);

            var fullDate = id_year+ id_month + id_date;
            CultureInfo provider = CultureInfo.InvariantCulture;

             DateTime DateOB = DateTime.ParseExact(fullDate, "yyMMdd", provider);
    
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
                DateofBirth = DateOB,
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
                HobbiesOther=bgs.HobbiesOther,
                   PostalCode = bgs.PostalCode,
                   UserID = bgs.UserID,
                     Infosbig = listint,
                Addsupbig = supdoc
            




            };

            var extension = Path.GetExtension(pc.FileName).ToLower();
            if (extension == ".png" || extension == ".jpg" || extension == ".gif" || extension == ".jpeg" || extension == ".gif")
            {
                ViewBag.Errorpc = "incorrect image format, please note that we only accept (png, jpg, gif, and jpeg image file formats)";
                return View(Bigsister);
            }
                if (filelentghpc > 10)

            {

                ViewBag.Errorpc = "uploaded file is above the required size of 10mb";
                return View(Bigsister);

            }
            else

            {

            }
            if (filelentghCV > 10)

            {

                ViewBag.Errorcv = "uploaded file is above the required size of 10mb";
                return View(Bigsister);

            }
            else

            {

            }

            foreach (var formFile in QA)
            {
                if (formFile.Length > 0)
                {
                    var extensionf = Path.GetExtension(formFile.FileName).ToLower();
                    if (extensionf == ".png" || extensionf == ".jpg" || extensionf == ".gif" || extensionf == ".jpeg" || extensionf == ".pdf" || extensionf==".docx" || extensionf==".doc")
                    {


                    }
                    else if((formFile.Length/1000000)>5)

                    {
                        ViewBag.Errorqa = "One or more uploaded files are above the required size of 5mb";
                        return View(Bigsister);

                    }
                    else

                    {
                        ViewBag.Errorqa = "File format for qualifications is incorrect, we only accept qualifications in (pdf, word, png, jpg, jpeg) format";
                        return View(Bigsister);

                    }
                }

            }
                    var extensionx = Path.GetExtension(CV.FileName).ToLower();
            if (extensionx == ".png" || extensionx == ".jpg" || extensionx == ".gif" || extensionx == ".jpeg" || extensionx==".pdf")
            {

                unq2 = "CV" + Guid.NewGuid() + CV.FileName;
                string pathtofile = path_Root1 + "\\Uploads\\CV\\" + unq2;
                using (FileStream fs = System.IO.File.Create(pathtofile))

            {
                CV.CopyTo(fs);
                fs.Flush();
            }
                Bigsister.CVUrl = unq2;
            }
            else if (extension==".doc" || extension==".docx")
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
                Bigsister.CVUrl = newname;

            }
            else

            {
                ViewBag.Errorcv = "File format for the CV is incorrect, we only accept CVs in (pdf, word, png, jpg, jpeg) format";
                return View(Bigsister);

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
                Bigsister.CertifiedID = unq2;
            }
            else if (extensionid == ".doc" || extensionid == ".docx")
            {
                unq2 = "CV" + Guid.NewGuid() + CV.FileName;
                string conv = path_Root1 + "\\Uploads\\Conv\\" + unq2;
                string newname = "ID" + Guid.NewGuid() + ".pdf";
                string pathtofiled = path_Root1 + "\\Uploads\\CertifiedID\\" + newname;


                using (FileStream fs = System.IO.File.Create(conv))

                {
                    CV.CopyTo(fs);
                    fs.Flush();
                }
                Document doc = new Document();
                doc.LoadFromFile(conv);
                doc.SaveToFile(pathtofiled, FileFormat.PDF);
                Bigsister.CertifiedID = newname;

            }
            else

            {
                ViewBag.Errorid = "File format for the Certified ID is incorrect, we only accept Certified IDs in (pdf, word, png, jpg, jpeg) format";
                return View(Bigsister);

            }



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
                Bigsister.VerifCode = "18t" + Bigsister.Name.Substring(1,3) +unq2.Substring(1,3)+ ids;
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
                    await _context.SaveChangesAsync();

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
                            _context.BigSisterAcademic.Add(new BigSisterAcademic()
                            {
                                BigSisterUserID = Bigsister.BigSisterDetailID,
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
                            _context.BigSisterAcademic.Add(new BigSisterAcademic()
                            {
                                BigSisterUserID = Bigsister.BigSisterDetailID,
                                DateCreated = DateTime.Now,
                                QualificationDocname = formFile.FileName,
                                Qualificationurl = newname


                            }); ;
                            await _context.SaveChangesAsync();

                        }
                      
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
                Addsupbig = supdoc,
             
            };
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
