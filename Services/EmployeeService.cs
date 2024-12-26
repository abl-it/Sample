using Dapper;
using Sample.Models;
using Sample.Services.IServices;
using System.Data;
using static Dapper.SimpleCRUD;

namespace Sample.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IDbConnection _db;
        public EmployeeService(IDbConnection db)
        {
            _db = db;
            // Ensure Dapper.SimpleCRUD knows which SQL dialect to use
            SimpleCRUD.SetDialect(Dialect.SQLServer);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var employee = await _db.GetAsync<Employee>(id);
            if (employee == null)
                return 0;

            var affectedRows = await _db.DeleteAsync(employee);
            return affectedRows;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var employees = await _db.GetListAsync<Employee>();
            return employees;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var employee = await _db.GetAsync<Employee>(id);
            return employee;
        }

        public async Task<int> InsertAsync(Employee employee)
        {
            var id = await _db.InsertAsync(employee);
            if (id == null)
            {
                throw new InvalidOperationException("Failed to retrieve the inserted Employee ID.");
            }
            return (int)id;
        }

        public async Task<int> UpdateAsync(Employee employee)
        {
            var affectedRows = await _db.UpdateAsync(employee);
            return affectedRows;
        }


    }
}
