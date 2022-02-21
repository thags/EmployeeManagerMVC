using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmployeeManager.Models
{
    public class EmployeeRepository : IEmployeeManagerInterface
    {
        private readonly string connectionString;
        private readonly string connectionStringNoDBName;

        public EmployeeRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("WebDatabase");
            connectionStringNoDBName = configuration.GetConnectionString("WebDatabaseNoCatalog");
            createDatabase();
            CreateEmployeeTable();
            CreateDepartmentTable();

        }

        public void createDatabase()
        {
            using (var connection = new SqlConnection(connectionStringNoDBName))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = "CREATE DATABASE EmployeeManager";
                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("DataBase is Created Successfully");
                    }
                    catch (System.Exception ex)
                    {
                        if (ex.HResult == -2146232060)
                        {
                            Console.WriteLine("Database already Exists!");
                        }
                        else
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        public void CreateEmployeeTable()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $@"CREATE TABLE [dbo].[Employees](
                        [Id][int] IDENTITY(1, 1) NOT NULL,
                        [DepartmentId][int] NOT NULL,
                        [fName][nvarchar](max) NOT NULL,
                        [lName][nvarchar](max) NOT NULL)";

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Employee table created Successfully");
                    }
                    catch (System.Exception ex)
                    {
                        if (ex.Message == "There is already an object named 'EmployeeItems' in the database.")
                        {
                            Console.WriteLine("EmployeeItems Table already Existed!");
                        }
                        else
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
        }
        public void CreateDepartmentTable()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $@"CREATE TABLE [dbo].[Department](
                        [Id][int] IDENTITY(1, 1) NOT NULL,
                        [Name][nvarchar](max) NOT NULL)";

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Department table created Successfully");
                    }
                    catch (System.Exception ex)
                    {
                        if (ex.Message == "There is already an object named 'Department' in the database.")
                        {
                            Console.WriteLine("DepartmentItems Table already Existed!");
                        }
                        else
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
        }

        void IEmployeeManagerInterface.AddEmployee(Employee newEmployee)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"Insert into EmployeeItems (Employee) values ('{newEmployee}') ";
                    command.ExecuteNonQuery();
                }
            }
        }

        Employee IEmployeeManagerInterface.GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Employee> IEmployeeManagerInterface.GetAllEmployees()
        {
            var EmployeeList = new List<Employee> { };
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT * FROM EmployeeItems ORDER BY Id DESC";
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int itemId = (int)dataReader[0];
                            int departmentId = (int)dataReader[1];
                            string fName = (string)dataReader[2];
                            string lName = (string)dataReader[3];

                            Employee newItem = new Employee
                            {
                                Id = itemId,
                                DepartmentId = departmentId,
                                FName = fName,
                                LName = lName
                            };
                            EmployeeList.Add(newItem);
                        }
                    }
                }
            }
            return EmployeeList;
        }

        void IEmployeeManagerInterface.RemoveEmployee(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM EmployeeItems WHERE Id = {id}";
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

            }
        }

        bool IEmployeeManagerInterface.UpdateEmployee(Employee employeeUpdate)
        {
            throw new NotImplementedException();
        }

        void IEmployeeManagerInterface.AddDepartment(Department newDepartment)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"Insert into DepartmentItems (Department) values ('{newDepartment}') ";
                    command.ExecuteNonQuery();
                }
            }
        }

        Department IEmployeeManagerInterface.GetDepartment(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Department> IEmployeeManagerInterface.GetAllDepartments()
        {
            var DepartmentList = new List<Department> { };
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"SELECT * FROM DepartmentItems ORDER BY Id DESC";
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            int itemId = (int)dataReader[0];
                            string name = (string)dataReader[2];

                            Department newItem = new Department
                            {
                                Id = itemId,
                                Name = name
                            };
                            DepartmentList.Add(newItem);
                        }
                    }
                }
            }
            return DepartmentList;
        }

        void IEmployeeManagerInterface.RemoveDepartment(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM DepartmentItems WHERE Id = {id}";
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

            }
        }

        bool IEmployeeManagerInterface.UpdateDepartment(Department DepartmentUpdate)
        {
            throw new NotImplementedException();
        }
    }
}