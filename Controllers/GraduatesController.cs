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

namespace _18TWENTY8.Models
{
    public class GraduatesController : Controller
    {
        private readonly EighteentwentyeightContext _context;
      
        private readonly IHostingEnvironment _host;
       public GraduatesController(IHostingEnvironment host, EighteentwentyeightContext context)
        {
            _context = context;
            _host = host;
        }


        // GET: Graduates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Graduates.ToListAsync());
        }

        // GET: Graduates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graduates = await _context.Graduates
                .FirstOrDefaultAsync(m => m.GraduateID == id);
            if (graduates == null)
            {
                return NotFound();
            }

            return View(graduates);
        }

        // GET: Graduates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Graduates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GraduateID,GraduateName,GraduatenameandQual,GraduateStoryname,pdfstoryurl,Videourl,imageurl,DateCreated")] Graduates graduates, IFormFile Doc1, IFormFile Doc2)
        {




            graduates.DateCreated = DateTime.Now;
            string path_Root1 = _host.WebRootPath;
            String path_Root2 = _host.WebRootPath; 
            string unq1;
            String unq2;

            unq1 = "story" + Guid.NewGuid() + Doc1.FileName;
            string pathtofile = path_Root1 + "\\Uploads\\GraduateStories\\" + unq1;
            using (FileStream fs = System.IO.File.Create(pathtofile))

            {
                Doc1.CopyTo(fs);
                fs.Flush();
            }

            unq2 = "story3" + Guid.NewGuid() + Doc2.FileName;
            string pathtofile1 = path_Root2 + "\\Uploads\\GraduateStories\\" + unq2;
            using (FileStream fs1 = System.IO.File.Create(pathtofile1))
            {
                Doc2.CopyTo(fs1);
                fs1.Flush();
            }

            graduates.imageurl = unq2;
            graduates.pdfstoryurl = unq1;
            graduates.Videourl = "noneyet";
          

            if (ModelState.IsValid)
            {
                _context.Add(graduates);
                await _context.SaveChangesAsync();
                graduates.Modalpopupid = "LargeModal" + graduates.GraduateID;
                _context.Update(graduates);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            
            
            }
            return View(graduates);
        }

        // GET: Graduates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graduates = await _context.Graduates.FindAsync(id);
            if (graduates == null)
            {
                return NotFound();
            }
            return View(graduates);
        }

        // POST: Graduates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GraduateID,GraduateName,GraduatenameandQual,GraduateStoryname,pdfstoryurl,Videourl,imageurl,DateCreated")] Graduates graduates)
        {
            if (id != graduates.GraduateID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(graduates);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GraduatesExists(graduates.GraduateID))
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
            return View(graduates);
        }

        // GET: Graduates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var graduates = await _context.Graduates
                .FirstOrDefaultAsync(m => m.GraduateID == id);
            if (graduates == null)
            {
                return NotFound();
            }

            return View(graduates);
        }

        // POST: Graduates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var graduates = await _context.Graduates.FindAsync(id);
            _context.Graduates.Remove(graduates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GraduatesExists(int id)
        {
            return _context.Graduates.Any(e => e.GraduateID == id);
        }
    }
}
