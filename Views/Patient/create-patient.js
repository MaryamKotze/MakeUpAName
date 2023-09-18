$(document).ready(function () {
    // Fetch doctors based on selected department
    $('input[name="SelectedDepartmentId"]').change(function () {
        var departmentId = $(this).val();
        $.ajax({
            url: '/Patient/FetchDoctors',
            type: 'GET',
            data: { departmentId: departmentId },
            success: function (data) {
                var doctorDropdown = $('#SelectedDoctorId');
                doctorDropdown.empty();
                doctorDropdown.append($('<option>').val('').text('Select a Doctor'));
                $.each(data, function (i, doctor) {
                    doctorDropdown.append($('<option>').val(doctor.value).text(doctor.text));
                });
            }
        });
    });
    // Fetch rooms and beds based on selected room
    $('#SelectedRoomId').change(function () {
        var roomId = $(this).val();
        $.ajax({
            url: '/Patient/FetchRoomsAndBeds',
            type: 'GET',
            data: { roomId: roomId },
            success: function (data) {
                var bedDropdown = $('#SelectedBedId');
                bedDropdown.empty();
                bedDropdown.append($('<option>').val('').text('Select a Bed'));
                $.each(data.beds, function (i, bed) {
                    bedDropdown.append($('<option>').val(bed.value).text(bed.text));
                });
            }
        });
    });
});