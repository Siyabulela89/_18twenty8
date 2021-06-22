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
using _18TWENTY8.Models.ViewModels.BigSister;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace _18TWENTY8.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly EighteentwentyeightContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _host;

        public VolunteerController(IHostingEnvironment host, EighteentwentyeightContext context, UserManager<ApplicationUser> userManager, IConfiguration Config)
        {
            _Configuration = Config;
            _userManager = userManager;
            _context = context;
            _host = host;
        }
        public IConfiguration _Configuration { get; }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Successfullapplication()
        {
            return View();
        }

        public IActionResult Create()
        {
            var Commitees = _context.Committees.Select(x => new SelectListItem()
            {
                Text = x.Description,
                Value = x.CommitteeTypeID.ToString()
            }).ToList();
            var programmes = _context.Programmes.Select(x => new SelectListItem()
            {
                Text = x.Description,
                Value = x.ProgrammeID.ToString()
            }).ToList();
            var daysofweek = _context.Daysofweek.Select(x => new SelectListItem()
            {
                Text = x.Description,
                Value = x.DayID.ToString()
            }).ToList();
            ViewBag.Option = new SelectList(_context.OptionalBool.OrderByDescending(x=> x.YesNoID), "YesNoID", "Description");
            ViewBag.ToContact = new SelectList(_context.Time, "TimeID", "Description");
            ViewBag.Province = new SelectList(_context.Province, "ProvinceID", "Provincename");
            var vd = new Volunteerdetail()
            {
                Commitees = Commitees,
                programmelist = programmes,
                Daysofweek = daysofweek,

            };

            return View(vd);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
   
        public async Task<IActionResult> Create(Volunteerdetail vs, IFormFile CV, IFormFile CID, IFormFile pc, IEnumerable<IFormFile> QA)
        {

    

            string path_Root1 = _host.WebRootPath;

            string unq1;
            String unq2;
            var filePath = Path.GetTempFileName();

 
            var prog = vs.programmelist.Where(x => x.Selected).Select(y => y.Value);
            var daysweek = vs.Daysofweek.Where(x => x.Selected).Select(y => y.Value);

            vs.DateCreated = DateTime.Now;
            unq2 = "CID" + Guid.NewGuid() + CID.FileName;
            string pathtofiled = path_Root1 + "\\Uploads\\CertifiedID\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofiled))

            {
                CID.CopyTo(fs);
                fs.Flush();
            }

            vs.IDurl = unq2;


            unq2 = "CV" + Guid.NewGuid() + CV.FileName;
            string pathtofile = path_Root1 + "\\Uploads\\CV\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofile))

            {
                CV.CopyTo(fs);
                fs.Flush();
            }
            vs.CVurl = unq2;

            var vdd = new Volunteerdetail()
            {Fullnames=vs.Fullnames,
            DOB=vs.DOB,
            email=vs.email,
            cellnumber=vs.cellnumber,
            Postalcode=vs.Postalcode,
            Postaladdressline1=vs.Postaladdressline1,
            Postaladdressline2=vs.Postaladdressline2,
            Province=vs.Province,
            home_businesscontact=vs.home_businesscontact,
            timetocall=vs.timetocall,
            Occupation=vs.Occupation,
            Employer=vs.Employer,
            Hoursweekforprogramme=vs.Hoursweekforprogramme,
            Otherintprogrammes=vs.Otherintprogrammes,
            previousexperienceinotherorgasvolunteer=vs.previousexperienceinotherorgasvolunteer,
            describewhyyouofferedtovolunteer=vs.describewhyyouofferedtovolunteer,
            goaltoachieveinvolunteer=vs.goaltoachieveinvolunteer,
            Othercommittees=vs.Othercommittees,
            describehobbies=vs.describehobbies,
            indemnity=vs.indemnity,
            CVurl=vs.CVurl,
            IDurl=vs.IDurl,
            DateCreated=DateTime.Now

            };
            unq2 = "pc" + Guid.NewGuid() + pc.FileName;
            string pathtofileds = path_Root1 + "\\Uploads\\VoluImg\\" + unq2;
            using (FileStream fs = System.IO.File.Create(pathtofileds))

            {
                pc.CopyTo(fs);
                fs.Flush();
            }

            vdd.imgurl = unq2;
            if (ModelState.IsValid)
            {
                _context.Add(vdd);
                await _context.SaveChangesAsync();

                int id = vdd.VolunteerID;

              

                foreach (var ids in prog)
                {
                    _context.ProgrammesStorage.Add(new ProgrammesStorage()
                    {
                        ProgrammeID = int.Parse(ids),
                        VolunteerID = id



                    });
                    await _context.SaveChangesAsync();
                }
                foreach (var ids in daysweek)
                {
                    _context.Daysofweekstorage.Add(new Daysofweekstorage()
                    {
                        DayID = int.Parse(ids),
                        VolunteerID = id



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
                        _context.VolunteerAcademic.Add(new VolunteerAcademic()
                        {
                            VolunteerID = id,
                            QualificationDocname = formFile.FileName,
                            Qualificationurl=unq1,
                            order=1,
                            DateCreated = DateTime.Now,



                        }); ;
                    }
                    await _context.SaveChangesAsync();

                }
                return RedirectToAction("Successfullapplication");
            }



                var Commitees = _context.Committees.Select(x => new SelectListItem()
            {
                Text = x.Description,
                Value = x.CommitteeTypeID.ToString()
            }).ToList();
            var programmes = _context.Programmes.Select(x => new SelectListItem()
            {
                Text = x.Description,
                Value = x.ProgrammeID.ToString()
            }).ToList();
            var daysofweek = _context.Daysofweek.Select(x => new SelectListItem()
            {
                Text = x.Description,
                Value = x.DayID.ToString()
            }).ToList();
            ViewBag.Option = new SelectList(_context.OptionalBool.OrderByDescending(x=> x.YesNoID), "YesNoID", "Description");
            ViewBag.ToContact = new SelectList(_context.Time, "TimeID", "Description");
            ViewBag.Province = new SelectList(_context.Province, "ProvinceID", "Provincename");
            var vd = new Volunteerdetail()
            {
                Commitees = Commitees,
                programmelist = programmes,
                Daysofweek = daysofweek,

            };
            return View(vd);
        }
    }
}