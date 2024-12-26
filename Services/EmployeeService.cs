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
            var employee = await _db.GetAsync<Employees>(id);
            if (employee == null)
                return 0;

            var affectedRows = await _db.DeleteAsync(employee);
            return affectedRows;
        }

        public async Task<IEnumerable<Employees>> GetAllAsync()
        {
            var employees = await _db.GetListAsync<Employees>();
            return employees;
        }

        public async Task<Employees> GetByIdAsync(int id)
        {
            var employee = await _db.GetAsync<Employees>(id);
            return employee;
        }

        public async Task<int> InsertAsync(Employees employee)
        {
            var id = await _db.InsertAsync(employee);
            if (id == null)
            {
                throw new InvalidOperationException("Failed to retrieve the inserted Employee ID.");
            }
            return (int)id;
        }

        public async Task<int> UpdateAsync(Employees employee)
        {
            var affectedRows = await _db.UpdateAsync(employee);
            return affectedRows;
        }


    }
}
