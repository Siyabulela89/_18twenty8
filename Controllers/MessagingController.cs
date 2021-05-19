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

namespace _18TWENTY8.Controllers
{
    public class MessagingController : Controller
    {
        private readonly EighteentwentyeightContext _context;
        private UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _host;

        private readonly IMapper _mapper;
        private readonly SisterService _sisterService;
        public MessagingController(IHostingEnvironment host, EighteentwentyeightContext context, UserManager<ApplicationUser> userManager, IConfiguration Config, IMapper mapper)
        {
            _Configuration = Config;
            _userManager = userManager;
            _context = context;

            this._mapper = mapper;
            _sisterService = new SisterService(_context, _mapper);
            _host = host;
        }
        public IConfiguration _Configuration { get; }
        public IActionResult Index(string SendID, string RecID, string RoleID)
        {
            ViewBag.Role = RoleID;

            string Main;
            string Sec;

            if (RoleID == "Big Sister (Mentor)")
            {
                Main = SendID;
                Sec = RecID;
            }
            else 
            {
                Main = RecID;
                Sec = SendID;
            }
            ViewBag.Main = Main;
            ViewBag.Sec = Sec;
            ViewBag.sender = SendID;
            ViewBag.receiver = RecID;

            var messages = _context.Messaging.Where(x => (x.SendUserId == SendID & x.ReceiveUserID == RecID)|| (x.SendUserId == RecID & x.ReceiveUserID == SendID)).OrderByDescending(x=> x.DateSent).ToList();
            var lilSisPro = _context.LittleSisterDetail.FirstOrDefault(x => x.UserID == Sec);
            var bigSisPro = _context.BigSisterDetail.FirstOrDefault(x => x.UserID == Main);

            var messageObject = new SisterMessagingViewModel
            {
                LittleSisterProfile = lilSisPro,
                BigSisterProfile = bigSisPro,
                Messages = messages,
                SendUserId=SendID,
                ReceiveUserID=RecID,
                RoleID = RoleID
            };
            //object listfor = new object
            //{    _context.Messaging.Where(x => x.SendUserId == SendID & x.ReceiveUserID == RecID).ToList(),
            //_context.LittleSisterDetail.FirstOrDefault(x => x.UserID == Sec),
            //     _context.BigSisterDetail.FirstOrDefault(x => x.UserID == Main)

            //};
            //List<object> listfor = new List<object>
            //{     _context.Messaging.Where(x=> x.SendUserId==SendID & x.ReceiveUserID==RecID).ToList(),
            //        _context.LittleSisterDetail.Where(x=> x.UserID==Sec).ToList(),
            //               _context.BigSisterDetail.Where(x=> x.UserID==Main).ToList()



            //};
            return View(messageObject);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SisterCreateMessageViewModel ms)
        {

            var Messaging = new Messaging()
            {
                ReceiveUserID = ms.ReceiveUserID,
                SendUserId = ms.SendUserId,
                Message = ms.Message,
                MsRead = 0,
                EmptyMessage = "",
                DateSent = DateTime.Now,






            };

            if (ModelState.IsValid)
            {

                _context.Add(Messaging);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", new { SendID = ms.SendUserId, RecID = ms.ReceiveUserID, RoleID = ms.RoleID });




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
    }
}