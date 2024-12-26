using Sample.Models;

namespace Sample.Services.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employees>> GetAllAsync();
        Task<Employees> GetByIdAsync(int id);
        Task<int> InsertAsync(Employees employee);
        Task<int> UpdateAsync(Employees employee);
        Task<int> DeleteAsync(int id);

    }
}
