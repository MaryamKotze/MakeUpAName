using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using MakeUpAName.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MakeUpAName.Models.PatientIntake;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/
//fwlink /? LinkID = 397860
namespace MakeUpAName.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            // Populate ViewBag.Departments with the list of available departments
            ViewBag.Departments = _context.Departments
            .Select(d => new SelectListItem { Value = d.DepartmentId.ToString(), Text = d.Name })
            .ToList();
            return View();
        }
        // POST: Doctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                // Add the new doctor to the database
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to the doctor list page
            }
            // If the model state is not valid, repopulate ViewBag.Departments
            ViewBag.Departments = _context.Departments
            .Select(d => new SelectListItem { Value = d.DepartmentId.ToString(), Text = d.Name })
            .ToList();
            return View(doctor); // Return to the create view with validation errors
        }
        // Add other action methods as needed (e.g., Edit, Details, Delete, Index, etc.)
        // Make sure to update the routes and views accordingly for these additional actions.
        public IActionResult Edit(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "Name",
           doctor.DepartmentId);
            return View(doctor);
        }
        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(d => d.DoctorId == id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
       [Bind("DoctorId,Name,IdentificationNumber,Email,CellphoneNumber,OfficeNumber,Qualifications,DepartmentId")] Doctor doctor)
        {
            if (id != doctor.DoctorId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.DoctorId))
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
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentId", "Name",
           doctor.DepartmentId);
            return View(doctor);
        }
        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Index()
        {
            var doctors = _context.Doctors.Include(d => d.Department).ToList();
            return View(doctors);
        }
        public JsonResult GetDoctorsByDepartment(int departmentId)
        {
            var doctors = _context.Doctors
            .Where(d => d.DepartmentId == departmentId)
            .Select(d => new
            {
                DoctorId = d.DoctorId,
                Name = d.Name
            })
            .ToList();
            return Json(doctors);
        }
        // Other actions for doctor-related views go here...
    }
}
