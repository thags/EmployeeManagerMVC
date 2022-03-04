using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeeManager.Models;

namespace EmployeeManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IEmployeeManagerInterface _repository;
    public HomeController(IEmployeeManagerInterface repository, ILogger<HomeController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(_repository.GetAllEmployees());
    }
    public RedirectResult InsertEmployee(EmployeeList newEmployee)
    {
        Department targetDepartment = _repository.GetDepartment(newEmployee.Employee.DepartmentId);
        if(targetDepartment.Name == null)
        {
            _repository.AddDepartment(new Department{Name=$"test{newEmployee.Employee.DepartmentId}"});
        }
        
        _repository.AddEmployee(newEmployee.Employee);
        return Redirect("https://localhost:7016");
    }

    [HttpPost]
    public JsonResult RemoveEmployee(int employeeId)
    {
        _repository.RemoveEmployee(employeeId);
        return Json(new{});
    }
    [HttpGet]
    public JsonResult PopulateForm(int id)
    {
        Employee employee = _repository.GetEmployee(id);
        return Json(employee);
    }
    public RedirectResult Update(Employee employee)
    {
        _repository.UpdateEmployee(employee);
        return Redirect("https://localhost:7016/");
    }

}
