using EmployeeManagementSystem.Models;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeRepository:IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        // 1. READ ALL (SELECT)
        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            var employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT EmployeeId, FirstName, LastName, Email, DepartmentId, Salary, JoiningDate FROM tbl_Employee";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new Employee
                            {
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                FirstName = reader["FirstName"].ToString() ?? string.Empty,
                                LastName = reader["LastName"].ToString() ?? string.Empty,
                                Email = reader["Email"].ToString() ?? string.Empty,
                                DepartmentId = Convert.ToInt32(reader["DepartmentId"]),
                                Salary = Convert.ToDecimal(reader["Salary"]),
                                JoiningDate = Convert.ToDateTime(reader["JoiningDate"])
                            });
                        }
                    }
                }
            }
            return employees;
        }

    }
}
