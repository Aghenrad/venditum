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

        [Route("/Project/{projetctid}/Task/{id}",
            Name = "taskdetails")]
        public async Task<IActionResult> TaskDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [Route("/Project/{projectid}/Task/Create",
            Name = "taskcreate")]
        public IActionResult TaskCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Project/{projectid}/Task/Create",
            Name = "taskcreate")]
        public async Task<IActionResult> TaskCreate([Bind("Id,ProjectId,Name,Decription,Status,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                // TODO add user id after authorization complete
                task.CreatedBy = 1;
                task.UpdatedBy = 1;
                task.StatusId = 1;

                task.CreatedDate = DateTime.Now;
                task.UpdatedDate = DateTime.Now;

                _context.Add(task);
                await _context.SaveChangesAsync();

                return RedirectToAction("TaskDetails", new RouteValueDictionary(
                    new { controller = "Tasks", action = "TaskDetails", projetctid = task.ProjectId, id = task.Id}));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", task.ProjectId);
            return View(task);
        }

        [Route("/Project/{projectid}/Task/{id}/Edit/",
            Name = "taskedit")]
        public async Task<IActionResult> TaskEdit(int? id)
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", task.ProjectId);
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Project/{projectid}/Task/{id}/Edit/",
            Name = "taskedit")]
        public async Task<IActionResult> TaskEdit(int id, [Bind("Id,ProjectId,Name,Decription,Status,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

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
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", task.ProjectId);
            return View(task);
        }

        [Route("/Project/{projectid}/Task/{id}/Delete/",
            Name = "taskdelete")]
        public async Task<IActionResult> TaskDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("/Project/{projectid}/Task/{id}/Delete/",
            Name = "taskdelete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
