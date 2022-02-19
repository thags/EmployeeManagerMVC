using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmployeeManager.Models
{
    public class EmployeeRepository : IEmployeeInterface
    {
        private readonly string connectionString;
        private readonly string connectionStringNoDBName;

        public EmployeeRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("WebDatabase");
            connectionStringNoDBName = configuration.GetConnectionString("WebDatabaseNoCatalog");
            createDatabase();
            CreateEmployeeTable();

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

        void IEmployeeInterface.Add(Employee newEmployee)
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

        Employee IEmployeeInterface.Get(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Employee> IEmployeeInterface.GetAll()
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

        void IEmployeeInterface.Remove(int id)
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

        bool IEmployeeInterface.Update(Employee employeeUpdate)
        {
            throw new NotImplementedException();
        }
    }
}