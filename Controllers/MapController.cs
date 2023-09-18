using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeUpAName.Models.PatientIntake;
using System.Collections.Generic;
using System.Linq;
using MakeUpAName.Models;
using MakeUpAName.Data;

public class MapController : Controller
{
    private readonly ApplicationDbContext _context;
    public MapController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        List<Location> locations = _context.Locations.ToList();
        List<Patient> patients = _context.Patients.Include(p => p.Location).ToList();
        var viewModel = new LocationViewModel
        {
            Locations = locations,
            Patients = patients
        };
        return View(viewModel);
    }
    [HttpPost]
    public IActionResult MovePatient(int patientId, int newLocationId)
    {
        var patient = _context.Patients.Find(patientId);
        var newLocation = _context.Locations.Find(newLocationId);
        if (patient != null && newLocation != null)
        {
            patient.Location = newLocation;
            _context.SaveChanges();
        }
        return Json(new { success = true });
    }
    public IActionResult GetPatientsByLocation(int locationId)
    {
        var patients = _context.Patients.Where(p => p.LocationId == locationId).ToList();
        return PartialView("_PatientListPartial", patients);
    }
}