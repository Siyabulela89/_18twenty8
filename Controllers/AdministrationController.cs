using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _18TWENTY8.Models;
using _18TWENTY8.Models.ViewModels;
using _18TWENTY8.Models.ViewModels.BigSister;
using _18TWENTY8.Models.ViewModels.NewFolder;
using _18TWENTY8.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FSTC.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {

        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<AdministrationController> logger;
        private readonly EighteentwentyeightContext _context;
        private readonly IMapper _mapper;
        private readonly SisterService _sisterService;
        public AdministrationController(RoleManager<ApplicationRole> roleManager
            , UserManager<ApplicationUser> userManager
            , ILogger<AdministrationController> logger
            , IConfiguration Config
            , EighteentwentyeightContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
            _sisterService = new SisterService(_context, _mapper);
            _Configuration = Config;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }
        public IConfiguration _Configuration { get; }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {

                ViewBag.ErrorMessage = $"User with id ={id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminLanding()
        {
            var posts = _context.LittleSisterDetail
                 .Where(p => p.ProfileStatusID ==7)
                 .Select(p => new { p.UserID});
            List<object> listfor = new List<object>
            {     _context.LittleSisterDetail.ToList(),
            _context.BigSisterDetail.ToList(),
          
            _context.SisterAssignment.Where(x=> posts.Any(y=> y.UserID==x.LittleSisterID)).ToList(),
               _context.AssignSisterStatus.ToList(),

                    _context.AssignApprove.ToList(),
                          _context.ProfileStatus.ToList()




            };
            return View(listfor.ToList());

        }
        [Authorize(Roles = "Admin")]
        public IActionResult AdminBursaryApplicant(String userID)
        {
            int ApplicationStatusid = _context.FinancialSupport.Where(x => x.UserID == userID).SingleOrDefault().ApplicationStatusID;
            string ApplicationStatusreas = _context.FinancialSupport.Where(x => x.UserID == userID).SingleOrDefault().ApplicationReason;

            var finances = _context.FinancialSupport.FirstOrDefault(x => x.UserID==userID);
            
           
     
            string ApplicationStatus = _context.ApplicationStatus.Where(x => x.ApplicationStatusID == ApplicationStatusid).SingleOrDefault().description;

            var Financeobj = new FinanceApproveview
            {
               finsupport= finances,
               
               Appstatus=ApplicationStatus

            };

            return View(Financeobj);

        }


        
             [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailedBursaryAdminAppliedsave(int BursaryID, string BursaryName, string UserID, int Status)
        {




            var ApplicationBurs = _context.BursaryApplicationCandidate.FirstOrDefault(x => x.UserID == UserID && x.BursaryID==BursaryID);
            ApplicationBurs.Status = Status;
       
            if (ModelState.IsValid)
            {




                _context.Update(ApplicationBurs);
                await _context.SaveChangesAsync();

                return RedirectToAction("DetailedBursaryAdminApplied", new { BursaryID=BursaryID, BursaryName = BursaryName });

            }


            return RedirectToAction("DetailedBursaryAdminApplied", new { BursaryID = BursaryID, BursaryName = BursaryName });

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminBursaryCreate(int Appstatus,string reason, string reasons, string userID)
        {


          

            var finances = _context.FinancialSupport.FirstOrDefault(x => x.UserID == userID);
            finances.ApplicationStatusID = Appstatus;
            if(Appstatus==6)
            {
                finances.ApplicationReason = reason;
            }
            else if(Appstatus==5)
            {
                finances.ApplicationReason = reasons;

            }
            else
            {
                finances.ApplicationReason = "Your profile has been approved, you are now able to apply for the 18twenty8 bursary schemes";
                

            }



            if (ModelState.IsValid)
            {



                
                    _context.Update(finances);
                    await _context.SaveChangesAsync();

                return RedirectToAction("AdminBursaryApplicant", new { userID = userID });

            }

            return View();

        }

        
              public IActionResult DetailedBursaryAdminApplied(int BursaryID,string BursaryName)
        {
            ViewBag.Titleburs = BursaryName;
            List<object> listfor = new List<object>
            {     _context.FinancialSupport.ToList(),
            _context.BursaryApplication.Where(x=> x.BursaryID==BursaryID).ToList(),
            _context.ApplicationStatus.ToList(),
            _context.BursaryStatus.ToList(),
            _context.BursaryApplicationCandidate.ToList()






            };
            return View(listfor.ToList());

        }
        [Authorize(Roles = "Admin")]
        public IActionResult BursaryApplicant()
        {

            List<object> listfor = new List<object>
            {     _context.FinancialSupport.ToList(),
            _context.BursaryApplication.ToList(),
            _context.ApplicationStatus.ToList(),
            _context.BursaryStatus.ToList(),
            _context.BursaryApplicationCandidate.ToList()
            
             




            };
            return View(listfor.ToList());

        }
        

        public IActionResult WelcomeRegistration(string  id, string sms, string con, string act, string number)
        {

            ViewBag.Con = con;
            ViewBag.Act = act;
            ViewBag.Id = id;
            ViewBag.message = sms;
            ViewBag.wel = "Welcome";
            string accountSid = _Configuration.GetSection("TwilioApp").GetValue<string>("ACCOUNT_SID");
            string authToken = _Configuration.GetSection("TwilioApp").GetValue<string>("AuthToken");
            TwilioClient.Init(accountSid, authToken);
            String numbers = "+27" + number.Substring(1);
            var message = MessageResource.Create(
                body: sms,
                from: new Twilio.Types.PhoneNumber("+17605482821"),
                to: new Twilio.Types.PhoneNumber(numbers)
            );


            return View();


        }
        public IActionResult Volunteerlist()
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
            List<object> listfor = new List<object>
            {     _context.Volunteerdetail.ToList(),
            _context.VolunteerAcademic.ToList(),
            _context.CommitteesStorage.ToList(),
            _context.ProgrammesStorage.ToList(),
            _context.Daysofweekstorage.ToList(),
            _context.Committees.ToList(),
            _context.Daysofweek.ToList(),
            _context.Programmes.ToList(),
            _context.Province.ToList(),
            _context.Time.ToList(),
            _context.OptionalBool.ToList()






            };
            return View(listfor.ToList());

        }
        public IActionResult AdminTakeup()
        {

            return View();

        }
        [Authorize(Roles = "Admin")]
        public IActionResult BursaryCapture()
        {
            ViewBag.BursType = new SelectList(_context.BursaryType, "BursaryTypeID", "Description");
            return View();

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> BursaryCapture(BursaryApplication BSA)
        {
            var Bursary = new BursaryApplication()
            {
                Title = BSA.Title,
                Description = BSA.Description,
                QualifyingCriteria = BSA.QualifyingCriteria,
                ApplicationStartDate = BSA.ApplicationStartDate,
                ApplicationEndDate = BSA.ApplicationEndDate,
                BursaryTypeID=BSA.BursaryTypeID,
                externallink=BSA.externallink,
            

                DateCreated = DateTime.Now,

        




            };
            if (ModelState.IsValid)
            {

                _context.Add(Bursary);
                await _context.SaveChangesAsync();

                return RedirectToAction("DetailsBurs");
            }
                return View();

        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        public IActionResult DetailsBurs()
        {
            List<object> listfor = new List<object>
            {     _context.BursaryApplication.Where(x=> x.ApplicationEndDate>=DateTime.Now).ToList(),
                  


            };
            return View(listfor.ToList());
        }
        public async Task<IActionResult> DetailsBB(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bursdet = await _context.BursaryApplication
                .FirstOrDefaultAsync(m => m.BursaryID == id);
            if (bursdet == null)
            {
                return NotFound();
            }

            return View(bursdet);
        }
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> EditUser(String id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id={id} cannot be found";
                return View("NotFound");
            }
            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                UserNamedsp = user.UserNamedisp,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with id={model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.UserNamedisp = model.UserNamedsp;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);

            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                ApplicationRole applicationRole = new ApplicationRole
                {
                    Name = model.RoleName

                };
                IdentityResult result = await roleManager.CreateAsync(applicationRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");

                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.UserID = userId;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            var model = new List<UserRolesViewModel>();
            foreach (var role in roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name


                };
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }
                model.Add(userRolesViewModel);

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            ViewBag.UserID = userId;
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            return RedirectToAction("EditUser", new { Id = userId });
        }
        public async Task<IActionResult> AdminStatusBigsis(string? id)
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

        public async Task<IActionResult> AdminStatusLilsis(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest($"Little Sister ID not provided");
          
            return View(await _sisterService.GetLittleSisterProfile(id));
        }
        [HttpPost]
        public async Task<IActionResult> ActionProfile([FromBody] ActionProfileViewModel model)
        {
            return Ok(await _sisterService.UpdateProfileStatus(model));
        }
        [HttpPost]
        public async Task<IActionResult> ActionProfileSisterAssign([FromBody] SisterAssignmentActionViewModel model)
        {
            return Ok(await _sisterService.UpdateSisterAssignStatus(model));
            //if (model.Role == "Big Sister (Mentor)")
            //{
            //    return RedirectToAction("Details", "BigSisterDetails", new { id = model.BigSisterID });
            //}
            //else
            //{
            //    return RedirectToAction("Details", "LittleSisterDetails", new { id = model.LilSisterID });
            //}
        }
        [HttpPost]
        public async Task<IActionResult> AssignSister([FromBody] AssignSisterViewModel model)
        {
            return Ok(await _sisterService.AssignBigSisterToLittleSister(model));
        }
        [HttpPost]
        public async Task<IActionResult> GetSisterAssignment([FromBody] GetSisterProfileViewModel model)
        {
            return Ok(await _sisterService.GetSisterAssignment(model.UserId));
        }

        [HttpPost]
        public async Task<IActionResult> GetSisterProfile([FromBody] GetSisterProfileViewModel model)
        {
            if (string.IsNullOrEmpty(model.SisterType))
                return BadRequest($"Please provide the sister type.");

            if (model.SisterType.Equals("Big", StringComparison.OrdinalIgnoreCase))
                return Ok(await _sisterService.GetBigSisterProfile(model.UserId));

            if (model.SisterType.Equals("Little", StringComparison.OrdinalIgnoreCase))
                return Ok(await _sisterService.GetLittleSisterProfile(model.UserId));


            return BadRequest($"Please specify the correct sister type. (Options are either Big or Little)");
        }

    }
}