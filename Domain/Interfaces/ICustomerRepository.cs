using Domain.Models;

namespace Domain.Interfaces;

public interface ICustomerRepository
{
    Task<bool> AddCustomerAsync(Customer customer);
    Task<Customer?> GetCustomerAsync(int id);
    Task<List<Customer>> GetCustomersAsync();
    Task<bool> DeleteCustomerAsync(int id);
}