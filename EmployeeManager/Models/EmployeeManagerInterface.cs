namespace EmployeeManager.Models
{
    public interface IEmployeeManagerInterface
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int employeeId);
        void AddEmployee(Employee newEmployee);
        void RemoveEmployee(int employeeId);
        bool UpdateEmployee(Employee updatedEmployee);
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartment(int departmentId);
        void AddDepartment(Department newDepartment);
        void RemoveDepartment(int departmentId);
        bool UpdateDepartment(Department updatedDepartment);
    }
}