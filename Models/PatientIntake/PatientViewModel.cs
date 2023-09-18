using System;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MakeUpAName.Models.PatientIntake
{
    // PatientViewModel.cs
    public class PatientViewModel
    {
        public int SelectedRoomId { get; set; }
        public IEnumerable<SelectListItem> RoomOptions { get; set; } // Dropdown list for rooms
        public int SelectedBedId { get; set; }
        public IEnumerable<SelectListItem> BedOptions { get; set; } // Dropdown list for beds
                                                                    // Other properties specific to the view, if needed
    }
}