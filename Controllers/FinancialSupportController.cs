using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _18TWENTY8.Controllers
{
    public class FinancialSupportController : Controller
    {
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
        public ActionResult Create(IFormCollection collection)
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
        public ActionResult Edit(int id, IFormCollection collection)
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
        public ActionResult Delete(int id, IFormCollection collection)
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