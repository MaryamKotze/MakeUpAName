using System;
using System.Collections.Generic;
using System.Linq;
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
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            var model = new Patient
            {
                LocationOptions = _context.Locations.Select(l => new SelectListItem
                {
                    Value = l.LocationId.ToString(),
                    Text = l.locationName // Adjust this property name based on your Location model
                }),
                DepartmentOptions = _context.Departments.Select(d => new SelectListItem
                {
                    Value = d.DepartmentId.ToString(),
                    Text = d.Name
                }),
                DoctorOptions = new List<SelectListItem>(), // Initially empty, to be populated via AJAX
                RoomOptions = _context.Rooms.Select(r => new SelectListItem
                {
                    Value = r.RoomId.ToString(),
                    Text = r.RoomNo
                }),
                BedOptions = new List<SelectListItem>() // Initially empty, to be populated via AJAX
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient model)
        {
            if (ModelState.IsValid)
            {
                // Check if the selected bed is occupied
                var selectedBed = await _context.Beds.FindAsync(model.SelectedBedId);
                if (selectedBed != null && selectedBed.IsOccupied)
                {
                    ModelState.AddModelError("SelectedBedId", "The selected bed is already occupied.");
                }
                else
                {
                    // Create a new Patient entity and populate its properties from the view model
                    var patient = new Patient
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Phone = model.Phone,
                        Bloodtype = model.Bloodtype,
                        Diagnosis = model.Diagnosis,
                        Medications = model.Medications,
                        AdditionalNotes = model.AdditionalNotes,
                        DoctorId = model.SelectedDoctorId,
                        BedId = model.SelectedBedId
                    };
                    var bloodTypes = new List<string>();
                    if (model.IsBloodTypeA) bloodTypes.Add("A");
                    if (model.IsBloodTypeB) bloodTypes.Add("B");
                    if (model.IsBloodTypeAB) bloodTypes.Add("AB");
                    if (model.IsBloodTypeO) bloodTypes.Add("O");
                    patient.Bloodtype = string.Join(", ", bloodTypes);
                    patient.LocationId = model.LocationId; // Assign the selected location ID
                    _context.Patients.Add(patient);
                    // Mark the selected bed as occupied
                    if (selectedBed != null)
                    {
                        selectedBed.IsOccupied = true;
                        _context.Entry(selectedBed).State = EntityState.Modified;
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            // If the model state is not valid or the bed is occupied, reload the dropdown lists
            model.DepartmentOptions = _context.Departments.Select(d => new SelectListItem
            {
                Value = d.DepartmentId.ToString(),
                Text = d.Name
            });
            model.DoctorOptions = new List<SelectListItem>(); // Initially empty, to be populated via 
            //AJAX 
                model.RoomOptions = _context.Rooms.Select(r => new SelectListItem
 {
     Value = r.RoomId.ToString(),
     Text = r.RoomNo
 });
            model.BedOptions = new List<SelectListItem>(); // Initially empty, to be populated via 
            //AJAX
        return View(model);
        }
        [HttpGet]
        public JsonResult FetchDoctors(int departmentId)
        {
            var doctors = _context.Doctors
            .Where(d => d.DepartmentId == departmentId)
            .Select(d => new SelectListItem
            {
                Value = d.DoctorId.ToString(),
                Text = d.Name
            })
            .ToList();
            return Json(doctors);
        }
        [HttpGet]
        public JsonResult FetchRoomsAndBeds(int roomId)
        {
            var room = _context.Rooms
            .Include(r => r.Beds)
            .FirstOrDefault(r => r.RoomId == roomId);
            if (room != null)
            {
                var beds = room.Beds.Select(b => new SelectListItem
                {
                    Value = b.BedId.ToString(),
                    Text = b.BedNo
                }).ToList();
                return Json(new { beds });
            }
            return Json(null);
        }

        // Other actions for patient-related views go here...
    }
}