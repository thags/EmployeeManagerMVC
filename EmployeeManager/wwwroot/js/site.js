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

function PopulateForm(i)
{
    $.ajax({
        url: 'Home/PopulateForm',
        type: 'GET',
        data: {
            id: i
        },
        dataType: 'json',
        success: function (response){
            $("#Employee_FName").val(response.FName);
            $("#Employee_LName").val(response.LName);
            $("#Employee_Id").val(response.Id);
            $("#Employee_DepartmentId").val(response.DepartmentId);


            $("#form-button").val("Update Employee");
            $("#form-action").attr("action", "/Home/Update");
        }
    });
}