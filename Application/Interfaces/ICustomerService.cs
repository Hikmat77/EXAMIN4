using Domain.Models;

namespace Application.Interfaces;

public interface ICustomerService
{
    Task AddCustomerAsync(Customer customer);
    Task GetCustomerAsync(int id);
    Task GetCustomersAsync();
    Task DeleteCustomerAsync(int id);
}