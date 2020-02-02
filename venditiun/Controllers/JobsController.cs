using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using venditiun.Models;
using venditum.Data;

namespace venditiun.Controllers
{
    public class JobsController : Controller
    {
        private readonly VenditiunDbContext _context;

        public JobsController(VenditiunDbContext context)
        {
            _context = context;
        }

        [Route("/Project/{projetctid}/Task/{taskId}/Job/{id}",
            Name = "jobdetails")]
        public async Task<IActionResult> JobDetails()
        {
            var venditiunDbContext = _context.Jobs.Include(j => j.CreatedByUser).Include(j => j.Status).Include(j => j.Task).Include(j => j.UpdatedByUser).Include(j => j.User);
            return View(await venditiunDbContext.ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.CreatedByUser)
                .Include(j => j.Status)
                .Include(j => j.Task)
                .Include(j => j.UpdatedByUser)
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id");
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Id");
            ViewData["UpdatedBy"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Tasks, "Id", "Id");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskId,UserId,Decription,BeginDate,EndDate,StatusId,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", job.CreatedBy);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", job.StatusId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Id", job.TaskId);
            ViewData["UpdatedBy"] = new SelectList(_context.Users, "Id", "Id", job.UpdatedBy);
            ViewData["UserId"] = new SelectList(_context.Tasks, "Id", "Id", job.UserId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", job.CreatedBy);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", job.StatusId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Id", job.TaskId);
            ViewData["UpdatedBy"] = new SelectList(_context.Users, "Id", "Id", job.UpdatedBy);
            ViewData["UserId"] = new SelectList(_context.Tasks, "Id", "Id", job.UserId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaskId,UserId,Decription,BeginDate,EndDate,StatusId,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
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
            ViewData["CreatedBy"] = new SelectList(_context.Users, "Id", "Id", job.CreatedBy);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Id", job.StatusId);
            ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Id", job.TaskId);
            ViewData["UpdatedBy"] = new SelectList(_context.Users, "Id", "Id", job.UpdatedBy);
            ViewData["UserId"] = new SelectList(_context.Tasks, "Id", "Id", job.UserId);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.CreatedByUser)
                .Include(j => j.Status)
                .Include(j => j.Task)
                .Include(j => j.UpdatedByUser)
                .Include(j => j.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
