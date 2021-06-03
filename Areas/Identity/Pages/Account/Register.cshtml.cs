using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using _18TWENTY8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace _18TWENTY8.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        public List<SelectListItem> Roles { get; }

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            Roles = new List<SelectListItem>
        {
                
            new SelectListItem {Value = "Little Sister (Mentee)", Text ="Little Sister (Mentee)"},
            new SelectListItem {Value = "Big Sister (Mentor)", Text = "Big Sister (Mentor)"},
        };
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "User Name")]
            public string UserNamedisp { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
           
           

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            [Display(Name = "UserRole")]
            public string UserRole { get; set; }

        }

        public void OnGet( int Id, string returnUrl = null)
        {
            if(Id==1)
            {
                ViewData["RegType"] = "Little Sister Registration";
                ViewData["Role"] = "Little Sister (Mentee)";

            }
            else if (Id == 2)
            {
                ViewData["RegType"] = "Big Sister Registration";
                ViewData["Role"] = "Big Sister (Mentor)";
            }
            else if (Id==3)

            {
                ViewData["RegType"] = "Financial Assistance Programme application";
                ViewData["Role"] = "Bursary Applicant";

            }
            else if (Id == 4)

            {
                ViewData["RegType"] = "Administration Take Up";
                ViewData["Role"] = "Admin";

            }


            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(int id, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, UserNamedisp=Input.UserNamedisp};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, Input.UserRole);
                   
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                     if(Input.UserRole== "Big Sister (Mentor)")
                    {
                        return RedirectToAction("create", "BigSisterDetails", new { email = Input.Email, userId = user.Id, fullnames = Input.UserNamedisp });

                      
                    }
                     else if (Input.UserRole == "Little Sister (Mentee)")
                    {
                     
                        return RedirectToAction("create", "LittleSisterDetails", new { email = Input.Email, userId = user.Id, fullnames = Input.UserNamedisp });

                    }
                    else if (Input.UserRole == "Admin")
                    {
                        return Redirect("~/Administration/AdminLanding");

                    }
                    else if (Input.UserRole == "Bursary Applicant")
                    {
                       
                        return RedirectToAction("Create", "FinancialSupport", new { email = Input.Email, userId = user.Id,fullnames = Input.UserNamedisp });

                    }
                    return RedirectToPage("./Register", new { ReturnUrl = returnUrl, id = id });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
