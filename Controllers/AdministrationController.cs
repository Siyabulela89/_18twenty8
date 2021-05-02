using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _18TWENTY8.Models;
using _18TWENTY8.Models.ViewModels;
using _18TWENTY8.Models.ViewModels.BigSister;
using _18TWENTY8.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
            , EighteentwentyeightContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
            _sisterService = new SisterService(_context, _mapper);
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }

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
        public IActionResult AdminLanding()
        {

            List<object> listfor = new List<object>
            {     _context.LittleSisterDetail.ToList(),
            _context.BigSisterDetail.ToList(),
            _context.SisterAssignment.ToList(),
               _context.AssignSisterStatus.ToList(),

                    _context.AssignApprove.ToList(),
                          _context.ProfileStatus.ToList()




            };
            return View(listfor.ToList());

        }
        public IActionResult AdminTakeup()
        {

            return View();

        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
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
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActionProfile([FromBody] ActionProfileViewModel model)
        {
            return Ok(await _sisterService.UpdateProfileStatus(model));
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