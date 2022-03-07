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
            $("#Employee_FName").val(response.fName);
            $("#Employee_LName").val(response.lName);
            $("#Employee_Id").val(response.id);
            $("#Employee_DepartmentId").val(response.departmentId);


            $("#Employee-form-button").val("Update Employee");
            $("#Employee-form-action").attr("action", "/Home/Update");
        }
    });
}