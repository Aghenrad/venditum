using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
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

        [Route("/Project/{projectid}/Task/{taskid}/Job/{id}",
             Name = "jobdetails")]
        public async Task<IActionResult> JobDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .FirstOrDefaultAsync(m => m.Id == id);

            if (job == null)
            {
                return NotFound();
            }

            job.Status = await _context.Statuses
                .Where(s => s.Id == job.StatusId)
                .FirstOrDefaultAsync();
            job.Task = await _context.Tasks
                .Where(p => p.Id == job.TaskId)
                .FirstOrDefaultAsync();
            job.CreatedByUser = await _context.Users
                .Where(u => u.Id == job.CreatedBy)
                .FirstOrDefaultAsync();
            job.UpdatedByUser = await _context.Users
                .Where(u => u.Id == job.UpdatedBy)
                .FirstOrDefaultAsync();

            return View(job);
        }

        [Route("/Project/{projectid}/Task/{taskid}/Job/Create",
            Name = "jobcreate")]
        public IActionResult JobCreate(int taskid)
        {
            return View(new Job() { Task = _context.Tasks.Where(t => t.Id == taskid).FirstOrDefault()});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Project/{projectid}/Task/{taskid}/Job/Create",
            Name = "jobcreate")]
        public async Task<IActionResult> JobCreate([Bind("Id,ProjectId,Name,Decription,Status,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Models.Job job)
        {
            if (ModelState.IsValid)
            {
                //TODO add logged user id
                job.CreatedBy = 1;
                job.UpdatedBy = 1;

                job.StatusId = 1;
                job.CreatedDate = DateTime.Now;
                job.UpdatedDate = DateTime.Now;

                _context.Add(job);
                await _context.SaveChangesAsync();

                return RedirectToAction("JobDetails", new RouteValueDictionary(
                    new { controller = "Jobs", action = "JobDetails", projectid = job.Task.ProjectId, taskid = job.TaskId, id = job.Id }));
            }

            return View(job);
        }

        [Route("/Project/{projectid}/Task/{taskid}/Job/{id}/Edit/",
            Name = "jobedit")]
        public async Task<IActionResult> JobEdit(int projectid, int? id)
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

            job.Status = await _context.Statuses
                .Where(s => s.Id == job.StatusId)
                .FirstOrDefaultAsync();
            job.CreatedByUser = await _context.Users
                .Where(u => u.Id == job.CreatedBy)
                .FirstOrDefaultAsync();
            job.UpdatedByUser = await _context.Users
                .Where(u => u.Id == job.UpdatedBy)
                .FirstOrDefaultAsync();

            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", job.StatusId);

            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Project/{projectid}/Task/{taskid}/Job/{id}/Edit/",
            Name = "jobedit")]
        public async Task<IActionResult> JobEdit(int id, [Bind("Id,ProjectId,Name,Decription,StatusId")] Models.Job updateForJob)
        {
            if (id != updateForJob.Id)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);

            job.Decription = updateForJob.Decription;
            job.StatusId = updateForJob.StatusId;

            //TODO add logged user id
            job.UpdatedBy = 1;
            job.UpdatedDate = DateTime.Now;

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
                return RedirectToAction("JobDetails", new RouteValueDictionary(
                    new { controller = "Jobs", action = "JobDetails", projectid = job.Task.ProjectId, taskid = job.TaskId, id = job.Id }));
            }

            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", job.StatusId);

            return View(job);
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
