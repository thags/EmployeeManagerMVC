namespace EmployeeManager.Models
{
    public interface IEmployeeInterface
    {
        IEnumerable<Employee> GetAll();
        Employee Get(int employeeId);
        void Add(Employee newEmployee);
        void Remove(int employeeId);
        bool Update(Employee updatedEmployee);
    }
}