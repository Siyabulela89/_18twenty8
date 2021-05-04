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

namespace _18TWENTY8.Controllers
{
    public class FinancialSupportController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<FinancialSupportController> logger;
        private readonly EighteentwentyeightContext _context;
        public FinancialSupportController(RoleManager<ApplicationRole> roleManager
          , UserManager<ApplicationUser> userManager
          , ILogger<FinancialSupportController> logger
          , EighteentwentyeightContext context, IMapper mapper)
        {
            _context = context;
          
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.logger = logger;
        }
        // GET: FinancialSupport
        public ActionResult Index()
        {
            return View();
        }

        // GET: FinancialSupport/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FinancialSupport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinancialSupport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FinancialSupport fs)
        {
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

        // GET: FinancialSupport/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FinancialSupport/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FinancialSupport fs)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FinancialSupport/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FinancialSupport/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FinancialSupport fs)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}