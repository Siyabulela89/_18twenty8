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
            ViewBag.Option = new SelectList(_context.OptionalBool, "YesNoID", "Description");
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