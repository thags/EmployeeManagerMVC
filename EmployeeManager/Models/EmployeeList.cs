namespace EmployeeManager.Models
{
    public class EmployeeList
    {
        public List<Employee> Employees {get;set;}
        public List<Department> Departments {get;set;}
        public Employee Employee {get;set;}
        public Department Department {get;set;}
    }
}