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
    public class RecognitionLettersController : Controller
    {
        private readonly EighteentwentyeightContext _context;

        private readonly IHostingEnvironment _host;
        public RecognitionLettersController(IHostingEnvironment host, EighteentwentyeightContext context)
        {
            _context = context;
            _host = host;
        }

        // GET: RecognitionLetters
        public async Task<IActionResult> Index()
        {
            return View(await _context.RecognitionLetters.OrderBy(x=>x.order).ToListAsync());
        }

        // GET: RecognitionLetters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recognitionLetters = await _context.RecognitionLetters
                .FirstOrDefaultAsync(m => m.RecognitionLetterID == id);
            if (recognitionLetters == null)
            {
                return NotFound();
            }

            return View(recognitionLetters);
        }

        // GET: RecognitionLetters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RecognitionLetters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecognitionLetterID,RecognitionLetterName,DateCreated")] RecognitionLetters recognitionLetters, IFormFile Doc1)
        {


            recognitionLetters.DateCreated = DateTime.Now;
            string path_Root = _host.WebRootPath;
            string unq;

            unq = "Rec" + Guid.NewGuid() + Doc1.FileName;
            string pathtofile = path_Root + "\\Uploads\\Recognition\\" + unq;
            using (FileStream fs = System.IO.File.Create(pathtofile))

            {
                Doc1.CopyTo(fs);
                fs.Flush();
            }

            recognitionLetters.recogurl = unq;

            if (ModelState.IsValid)
            {
           
                _context.Add(recognitionLetters);
                await _context.SaveChangesAsync();
                recognitionLetters.popupmodalid = "LargeModal" + recognitionLetters.RecognitionLetterID;
                _context.Update(recognitionLetters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recognitionLetters);
        }

        // GET: RecognitionLetters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recognitionLetters = await _context.RecognitionLetters.FindAsync(id);
            if (recognitionLetters == null)
            {
                return NotFound();
            }
            return View(recognitionLetters);
        }

        // POST: RecognitionLetters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecognitionLetterID,RecognitionLetterName,DateCreated")] RecognitionLetters recognitionLetters)
        {
            if (id != recognitionLetters.RecognitionLetterID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recognitionLetters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecognitionLettersExists(recognitionLetters.RecognitionLetterID))
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
            return View(recognitionLetters);
        }

        // GET: RecognitionLetters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recognitionLetters = await _context.RecognitionLetters
                .FirstOrDefaultAsync(m => m.RecognitionLetterID == id);
            if (recognitionLetters == null)
            {
                return NotFound();
            }

            return View(recognitionLetters);
        }

        // POST: RecognitionLetters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recognitionLetters = await _context.RecognitionLetters.FindAsync(id);
            _context.RecognitionLetters.Remove(recognitionLetters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecognitionLettersExists(int id)
        {
            return _context.RecognitionLetters.Any(e => e.RecognitionLetterID == id);
        }
    }
}
