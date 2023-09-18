using MakeUpAName.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeUpAName.Models.PatientIntake;
using System;
using System.Threading.Tasks;
namespace MakeUpAName.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,Name")] Department
       department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to the department listing or any 
                //other appropriate action
            }
            return View(department);
        }
        public IActionResult Delete(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _context.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Index()
        {
            var departments = _context.Departments.ToList();
            return View(departments);
        }
    }
}