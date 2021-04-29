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

namespace _18TWENTY8.Views
{
    public class PressPublicitiesController : Controller
    {
        private readonly EighteentwentyeightContext _context;
        private readonly IHostingEnvironment _host;
        public PressPublicitiesController(IHostingEnvironment host, EighteentwentyeightContext context)
        {
            _context = context;
            _host = host;
        }

        // GET: PressPublicities
        public async Task<IActionResult> Index()
        {

            List<object> listfor = new List<object>
            {     _context.PressPubType.ToList(),
                    _context.PressPublicity.OrderByDescending(x=> x.DateCreated).ToList()


            };
            return View(listfor.ToList());
           
        }

        // GET: PressPublicities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pressPublicity = await _context.PressPublicity
                .FirstOrDefaultAsync(m => m.PressPubID == id);
            if (pressPublicity == null)
            {
                return NotFound();
            }

            return View(pressPublicity);
        }

        // GET: PressPublicities/Create
        public IActionResult Create()
        {
            List<PressPubType> listpub = new List<PressPubType>();
            listpub = _context.PressPubType.ToList();
            ViewData["PubType"] = listpub;
            return View();
        }

        // POST: PressPublicities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PressPubID,PressPubTypeID,Title,Description,Pathurl,SiteUrl,DateCreated")] PressPublicity pressPublicity, IFormFile Doc)
        {
            pressPublicity.DateCreated = DateTime.Now;
            string path_Root;
            string uniquen;
            string siteurl;
            if (pressPublicity.PressPubTypeID == 2)
            {
                path_Root = _host.WebRootPath;
                siteurl = "https://www.youtube.com/embed/" + pressPublicity.SiteUrl;
            }
            else if (pressPublicity.PressPubTypeID == 3)
            {
                path_Root = _host.WebRootPath;
                siteurl = pressPublicity.SiteUrl; ;

            }
            else if (pressPublicity.PressPubTypeID == 4)

            {
                path_Root = _host.WebRootPath;
                siteurl = pressPublicity.SiteUrl;

            }
            else if (pressPublicity.PressPubTypeID == 7)

            {
                path_Root = _host.WebRootPath;
                siteurl = "https://player.vimeo.com/video/" + pressPublicity.SiteUrl + "?color=ffffff&title=0&byline=0&portrait=0&badge=0";

            }
         
            else

            {
                siteurl = "nourl";

            }

            if ((Doc == null || Doc.Length == 0))
            {
                pressPublicity.Pathurl = "Nodoc";

            }
            else
            {
           
              
                if(pressPublicity.PressPubTypeID == 5)

                {
                    path_Root = _host.WebRootPath;
                    uniquen = "PressPub" + Guid.NewGuid() + Doc.FileName;

                }
                else if (pressPublicity.PressPubTypeID == 6)
                {
                    path_Root = _host.WebRootPath;
                    uniquen = "PressPub" + Guid.NewGuid() + Doc.FileName;
                }
                else

                {
                    path_Root = _host.WebRootPath;
                    uniquen =  Doc.FileName;

                }
                string pathtofile = path_Root + "\\Uploads\\PressContent\\" + uniquen;
                using (FileStream fs = System.IO.File.Create(pathtofile))
                {
                    Doc.CopyTo(fs);
                    fs.Flush();
                }
                pressPublicity.Pathurl = uniquen;
            }
              if (pressPublicity.PressPubTypeID == 1)

            {
                path_Root = _host.WebRootPath;
                siteurl = pressPublicity.SiteUrl;

                uniquen = "Theone.png";
                pressPublicity.Pathurl = uniquen;

            }
            pressPublicity.SiteUrl = siteurl;
            if (ModelState.IsValid)
            {
                _context.Add(pressPublicity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pressPublicity);
        }

        // GET: PressPublicities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pressPublicity = await _context.PressPublicity.FindAsync(id);
            if (pressPublicity == null)
            {
                return NotFound();
            }
            return View(pressPublicity);
        }

        // POST: PressPublicities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PressPubID,PressPubTypeID,Title,Description,Pathurl,SiteUrl,DateCreated")] PressPublicity pressPublicity)
        {
            pressPublicity.DateCreated = DateTime.Now;
            if (id != pressPublicity.PressPubID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pressPublicity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PressPublicityExists(pressPublicity.PressPubID))
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
            return View(pressPublicity);
        }

        // GET: PressPublicities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pressPublicity = await _context.PressPublicity
                .FirstOrDefaultAsync(m => m.PressPubID == id);
            if (pressPublicity == null)
            {
                return NotFound();
            }

            return View(pressPublicity);
        }

        // POST: PressPublicities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pressPublicity = await _context.PressPublicity.FindAsync(id);
            _context.PressPublicity.Remove(pressPublicity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PressPublicityExists(int id)
        {
            return _context.PressPublicity.Any(e => e.PressPubID == id);
        }
    }
}
