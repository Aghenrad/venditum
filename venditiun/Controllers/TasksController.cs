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
    public class TasksController : Controller
    {
        private readonly VenditiunDbContext _context;

        public TasksController(VenditiunDbContext context)
        {
            _context = context;
        }

        [Route("/Project/{projectid}/Task/{id}",
            Name = "taskdetails")]
        public async Task<IActionResult> TaskDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            task.Status = await _context.Statuses
                .Where(s => s.Id == task.StatusId)
                .FirstOrDefaultAsync();
            task.Project = await _context.Projects
                .Where(p => p.Id == task.ProjectId)
                .FirstOrDefaultAsync();
            task.CreatedByUser = await _context.Users
                .Where(u => u.Id == task.CreatedBy)
                .FirstOrDefaultAsync();
            task.UpdatedByUser = await _context.Users
                .Where(u => u.Id == task.UpdatedBy)
                .FirstOrDefaultAsync();
            task.Jobs = _context.Jobs
                .Where(j => j.TaskId == id)
                .ToList();

            return View(task);
        }

        [Route("/Project/{projectid}/Task/Create",
            Name = "taskcreate")]
        public IActionResult TaskCreate(int projectid)
        {
            return View(new Models.Task() { ProjectId = projectid });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Project/{projectid}/Task/Create",
            Name = "taskcreate")]
        public async Task<IActionResult> TaskCreate([Bind("Id,ProjectId,Name,Decription")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                //TODO add logged user id
                task.CreatedBy = 1;
                task.UpdatedBy = 1;

                task.StatusId = 1;
                task.CreatedDate = DateTime.Now;
                task.UpdatedDate = DateTime.Now;

                _context.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction("TaskDetails", new RouteValueDictionary(
                    new { controller = "Tasks", action = "TaskDetails", projectid = task.ProjectId, id = task.Id}));
            }
            
            return View(task);
        }

        [Route("/Project/{projectid}/Task/{id}/Edit/",
            Name = "taskedit")]
        public async Task<IActionResult> TaskEdit(int projectid, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            task.Status = await _context.Statuses
                .Where(s => s.Id == task.StatusId)
                .FirstOrDefaultAsync();
            task.CreatedByUser = await _context.Users
                .Where(u => u.Id == task.CreatedBy)
                .FirstOrDefaultAsync();
            task.UpdatedByUser = await _context.Users
                .Where(u => u.Id == task.UpdatedBy)
                .FirstOrDefaultAsync();
            task.Jobs = _context.Jobs
                .Where(j => j.TaskId == id)
                .ToList();
            
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", task.StatusId);
            
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Project/{projectid}/Task/{id}/Edit/",
            Name = "taskedit")]
        public async Task<IActionResult> TaskEdit(int id, [Bind("Id,ProjectId,Name,Decription,StatusId")] Models.Task updateForTask)
        {
            if (id != updateForTask.Id)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);

            task.Name = updateForTask.Name;
            task.Decription = updateForTask.Decription;
            task.StatusId = updateForTask.StatusId;

            //TODO add logged user id
            task.UpdatedBy = 1;
            task.UpdatedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("TaskDetails", new RouteValueDictionary(
                    new { controller = "Tasks", action = "TaskDetails", projectid = task.ProjectId, id = task.Id }));
            }

            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", task.StatusId);

            return View(task);
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
