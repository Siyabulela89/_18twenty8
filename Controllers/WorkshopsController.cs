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

namespace _18TWENTY8.Controllers
{
    public class WorkshopsController : Controller
    {
        private readonly EighteentwentyeightContext _context;
        private readonly IHostingEnvironment _host;
        public WorkshopsController(IHostingEnvironment host, EighteentwentyeightContext context)
        {
            _context = context;
            _host = host;
        }

        // GET: Workshops
        public async Task<IActionResult> Index()
        {
            List<object> listfor = new List<object>
            {     
                    _context.Workshops.ToList(),
                    _context.WorkshopsupportDocType.ToList()

            };
            return View(listfor.ToList());
        }
        public IActionResult LeadershipCamp()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Bam()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult FinancialAssistance()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        // GET: Workshops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshops = await _context.Workshops
                .FirstOrDefaultAsync(m => m.WorkshopID == id);
            if (workshops == null)
            {
                return NotFound();
            }

            return View(workshops);
        }

        // GET: Workshops/Create
        public IActionResult Create()
        {
            List<WorkshopsupportDocType> workshoptype = new List<WorkshopsupportDocType>();
            workshoptype = _context.WorkshopsupportDocType.ToList();
            ViewData["wstype"] = workshoptype;
            return View();
        }

        // POST: Workshops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkshopID,WorkshopTitle,WorkshopContent,fileurl,WorkshopFileTypeID,DateCreated")] Workshops workshops, IFormFile Doc)
        {

            workshops.DateCreated = DateTime.Now;
            string path_Root;
            string uniquen;
            path_Root = _host.WebRootPath;
            if (workshops.WorkshopFileTypeID==1)
            {
                uniquen = "PressPub" + Guid.NewGuid() + Doc.FileName;
                string pathtofile = path_Root + "\\Uploads\\Workshops\\" + uniquen;
                using (FileStream fs = System.IO.File.Create(pathtofile))
                {
                    Doc.CopyTo(fs);
                    fs.Flush();
                }
               

            }
            else if (workshops.WorkshopFileTypeID==2)
            {
                uniquen= "https://www.youtube.com/embed/" + workshops.fileurl;

            }
            else

            {
                uniquen = "Nomedia.png";

            }
            workshops.fileurl = uniquen;
            if (ModelState.IsValid)
            {
                _context.Add(workshops);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workshops);
        }

        // GET: Workshops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshops = await _context.Workshops.FindAsync(id);
            if (workshops == null)
            {
                return NotFound();
            }
            return View(workshops);
        }

        // POST: Workshops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkshopID,WorkshopTitle,WorkshopContent,fileurl,Filetype,DateCreated")] Workshops workshops)
        {
            if (id != workshops.WorkshopID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workshops);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkshopsExists(workshops.WorkshopID))
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
            return View(workshops);
        }

        // GET: Workshops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshops = await _context.Workshops
                .FirstOrDefaultAsync(m => m.WorkshopID == id);
            if (workshops == null)
            {
                return NotFound();
            }

            return View(workshops);
        }

        // POST: Workshops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workshops = await _context.Workshops.FindAsync(id);
            _context.Workshops.Remove(workshops);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkshopsExists(int id)
        {
            return _context.Workshops.Any(e => e.WorkshopID == id);
        }

        public IActionResult Bigsisternetwork()
        {
           
            return View();
        }

        
    }
}
