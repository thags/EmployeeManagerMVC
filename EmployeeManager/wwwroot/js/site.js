function RemoveEmployee(i)
{
    $.ajax({
        url: 'Home/RemoveEmployee',
        type: 'POST',
        data: {
            employeeId: i
        },
        success: function(){
            window.location.reload();
        }
    });
}