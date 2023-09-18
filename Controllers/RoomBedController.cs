using MakeUpAName.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MakeUpAName.Models.PatientIntake;
public class RoomBedController : Controller
{
    private readonly ApplicationDbContext _context;
    public RoomBedController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Create()
    {
        var viewModel = new RoomBedViewModel
        {
            Room = new Room(),
            Bed = new Bed()
        };
        return View(viewModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoomBedViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            // Add the room to the database
            _context.Rooms.Add(viewModel.Room);
            await _context.SaveChangesAsync();
            // Assign the room id to the bed
            viewModel.Bed.RoomId = viewModel.Room.RoomId;
            // Add the bed to the database
            _context.Beds.Add(viewModel.Bed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(viewModel);
    }
    public IActionResult Index()
    {
        var roomsAndBeds = _context.Rooms.Include(r => r.Beds).ToList();
        return View(roomsAndBeds);
    }
}
