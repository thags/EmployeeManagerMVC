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
        return View();
    }

}
