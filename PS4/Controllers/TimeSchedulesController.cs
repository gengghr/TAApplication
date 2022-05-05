using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PS4.Data;
using PS4.Models;

namespace PS4
{
    public class Schedule
    {
        public Schedule(string w, int sh, int eh, int sm, int em, bool open)
        {
            weekd = w;
            start_hour = sh;
            end_hour = eh;
            start_min = sm;
            end_min = em;
            isOpen = open;
        }
        public string weekd { get; set; }
        public int start_hour { get; set; }
        public int end_hour { get; set; }
        public int start_min { get; set; }
        public int end_min { get; set; }
        public bool isOpen { get; set; }
    }
    public class TimeSchedulesController : Controller
    {
        private readonly TAAPPContext _context;

        public TimeSchedulesController(TAAPPContext context)
        {
            _context = context;
        }

        // GET: TimeSchedules
        public async Task<IActionResult> Index()
        {
            var tAAPPContext = _context.TimeSchedule.Include(t => t.Applicant).Include(t => t.Course);
            return View(await tAAPPContext.ToListAsync());
        }

        // GET: TimeSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSchedule = await _context.TimeSchedule
                .Include(t => t.Applicant)
                .Include(t => t.Course)
                .FirstOrDefaultAsync(m => m.timeScheduleID == id);
            if (timeSchedule == null)
            {
                return NotFound();
            }

            return View(timeSchedule);
        }

        // GET: TimeSchedules/Create
        public IActionResult Create()
        {
            ViewData["ApplicantID"] = new SelectList(_context.Applicant, "ID", "CurrentProgram");
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Department");
            return View();
        }

        // POST: TimeSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("timeScheduleID,courseID,startTime,endTime,isOpen,CourseID,ApplicantID")] TimeSchedule timeSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantID"] = new SelectList(_context.Applicant, "ID", "CurrentProgram", timeSchedule.ApplicantID);
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Department", timeSchedule.CourseID);
            return View(timeSchedule);
        }

        // GET: TimeSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSchedule = await _context.TimeSchedule.FindAsync(id);
            if (timeSchedule == null)
            {
                return NotFound();
            }
            ViewData["ApplicantID"] = new SelectList(_context.Applicant, "ID", "CurrentProgram", timeSchedule.ApplicantID);
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Department", timeSchedule.CourseID);
            return View(timeSchedule);
        }

        // POST: TimeSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("timeScheduleID,courseID,startTime,endTime,isOpen,CourseID,ApplicantID")] TimeSchedule timeSchedule)
        {
            if (id != timeSchedule.timeScheduleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeScheduleExists(timeSchedule.timeScheduleID))
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
            ViewData["ApplicantID"] = new SelectList(_context.Applicant, "ID", "CurrentProgram", timeSchedule.ApplicantID);
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "Department", timeSchedule.CourseID);
            return View(timeSchedule);
        }

        // GET: TimeSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSchedule = await _context.TimeSchedule
                .Include(t => t.Applicant)
                .Include(t => t.Course)
                .FirstOrDefaultAsync(m => m.timeScheduleID == id);
            if (timeSchedule == null)
            {
                return NotFound();
            }

            return View(timeSchedule);
        }

        // POST: TimeSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeSchedule = await _context.TimeSchedule.FindAsync(id);
            _context.TimeSchedule.Remove(timeSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeScheduleExists(int id)
        {
            return _context.TimeSchedule.Any(e => e.timeScheduleID == id);
        }
        //give the start time applicantID set IsOpen
        public async Task<IActionResult> SetSchedule(int id )
        {
            var ts = _context.TimeSchedule.ToList();

            int col = id % 5;
            int row = id / 5;
            int day = 0;
            int year = 2021;
            int month = 12;
            //int base_hour = 8;
            int hour = 8 + row / 4;
            int min = 15 * (row % 4);
            if(col ==0 )
            {
                day = 6;
            }
            else if(col == 1)
            {
                day = 7;
            }
            else if (col == 2)
            {
                day = 1;
            }
            else if (col == 3)
            {
                day = 2;
            }
            else if (col == 4)
            {
                day = 3;
            }
            var ed_hour = hour;
            var ed_min = min+15;
            if(ed_min >= 60)
            {
                ed_min -= 60;//+15 -60
                ed_hour++;
            }

            DateTime st_time = new DateTime(year,month,day,hour,min,0);
            DateTime ed_time = new DateTime(year, month, day, ed_hour, ed_min, 0);
            var result = ts.FirstOrDefault(m => m.startTime == st_time && m.ApplicantID==id);
            if(result == null || result.isOpen == false)
            {
                if (result == null)
                {
                    _context.TimeSchedule.Add(new TimeSchedule { startTime = st_time, ApplicantID = 1, endTime = ed_time, isOpen = true });
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    result.isOpen = true;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                result.isOpen = false;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        //return all TimeSchedule that isOpen is true
        public List<Schedule> GetSchedule()
        {
            
            List<Schedule> result = new List<Schedule>();
            var ts = _context.TimeSchedule.ToList();
            foreach(var t in ts)
            {
                if(t.isOpen==true)
                {
                    string weekday = t.startTime.DayOfWeek.ToString();
                    int sh = t.startTime.Hour;
                    int sm = t.startTime.Minute;
                    int eh = t.endTime.Hour;
                    int em = t.endTime.Minute;

                    Schedule tmp = new Schedule(weekday,sh,eh,sm,em,t.isOpen);
                    result.Add(tmp);
                }
            }
            return result;
        }
    }
}
