namespace EmployeeManager.Models
{
    public interface IDepartmentInterface
    {
        IEnumerable<Department> GetAll();
        Department Get(int departmentId);
        void Add(Department newDepartment);
        void Remove(int departmentId);
        bool Update(Department updatedDepartment);
    }
}