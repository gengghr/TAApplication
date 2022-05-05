using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PS4.Data;
using PS4.Models;

namespace PS4.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly TAAPPContext _context;

        public ApplicationsController(TAAPPContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        // GET: Applications
        public async Task<IActionResult> List()
        {
            return View(await _context.Applicant.ToListAsync());
        }
        // [Authorize(Roles = "Administrator")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Applicant.ToListAsync());
        }

        public async Task<IActionResult> HW1_Application()
        {
            return View(await _context.Applicant.ToListAsync());
        }
        public async Task<IActionResult> HW2_Application()
        {
            return View(await _context.Applicant.ToListAsync());
        }
        public async Task<IActionResult> HW2_Edit()
        {
            return View(await _context.Applicant.ToListAsync());
        }



        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var applicant = await _context.Applicant
                 .Include(s => s.Application)
                   .ThenInclude(e => e.Course)
                     .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.ID == id);

            //var applicant = await _context.Applicant
            //    .FirstOrDefaultAsync(m => m.ID == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }
        [AllowAnonymous]
        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("uID,LastName,FirstName,Phone,Address,CurrentDegree,CurrentProgram,GPA,WorkHours,Statement,EnglishFluency,Semesters,LinkedIn,Resume,CreationDate")] Applicant applicant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(applicant);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(applicant);
        }


        // GET: Applications/Edit/5
        [Authorize(Roles = "Applicant, Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }


        [Authorize(Roles = "Applicant, Administrator")]
        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditPost(int id, [Bind("ID,uID,LastName,FirstName,Phone,Address,CurrentDegree,CurrentProgram,GPA,WorkHours,Statement,EnglishFluency,Semesters,LinkedIn,Resume,CreationDate")] Applicant applicant)
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentToUpdate = await _context.Applicant.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Applicant>(
                studentToUpdate,
                "",
                s => s.FirstName, s => s.LastName, s => s.uID, s => s.Phone, s => s.Address, s => s.CurrentDegree, s => s.CurrentProgram, s => s.GPA,
               s => s.WorkHours, s => s.Statement, s => s.EnglishFluency, s => s.Semesters, s => s.LinkedIn, s => s.Resume))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(studentToUpdate);
            //if (id != applicant.ID)
            //{
            //    return NotFound();
            //}


            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(applicant);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ApplicantExists(applicant.ID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(applicant);
        }

        [Authorize(Roles = "Administrator")]
        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant
                .FirstOrDefaultAsync(m => m.ID == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }
        [Authorize(Roles = "Administrator")]
        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicant = await _context.Applicant.FindAsync(id);
            _context.Applicant.Remove(applicant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicant.Any(e => e.ID == id);
        }
    }
}
