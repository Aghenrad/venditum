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
    public class ProjectsController : Controller
    {
        private readonly VenditiunDbContext _context;

        public ProjectsController(VenditiunDbContext context)
        {
            _context = context;

        }

        [Route("/Projects/",
            Name = "projectslist")]
        public async Task<IActionResult> ProjectsList()
        {
            return View(await _context.Projects.ToListAsync());
        }

        [Route("/Project/{id}/Tasks/",
            Name = "projectdetails")]
        public async Task<IActionResult> ProjectDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            project.Tasks = _context.Tasks
                .Where(t => t.ProjectId == id)
                .ToList();
            project.Status = _context.Statuses
                .Where(s => s.Id == project.StatusId)
                .FirstOrDefault();

            return View(project);
        }

        [Route("/Project/Create",
            Name = "projectcreate")]
        public IActionResult ProjectCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Project/Create",
            Name = "projectcreate")]
        public async Task<IActionResult> ProjectCreate([Bind("Id,Name,Decription,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                // TODO add user id after authorization complete
                project.CreatedBy = 1;
                project.UpdatedBy = 1;
                project.StatusId = 1;

                project.CreatedDate = DateTime.Now;
                project.UpdatedDate = DateTime.Now;

                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProjectsList));
            }
            return View(project);
        }

        [Route("/Project/{id}/Edit/",
            Name = "projectedit")]
        public async Task<IActionResult> ProjectEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Project/{id}/Edit/",
            Name = "projectedit")]
        public async Task<IActionResult> ProjectEdit(int id, [Bind("Id,Name,Decription,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate")] Project updateForPoject)
        {
            if (id != updateForPoject.Id)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            project.Decription = updateForPoject.Decription;
            project.Name = updateForPoject.Name;

            project.UpdatedBy = 1;
            project.UpdatedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ProjectsList));
            }
            return View(project);
        }

        [Route("/Project/{id}/Delete",
            Name = "projectdelete")]
        public async Task<IActionResult> ProjectDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("/Project/{id}/Delete/",
            Name = "projectdelete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ProjectsList));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
