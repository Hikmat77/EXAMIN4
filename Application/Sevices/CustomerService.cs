using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private ICustomerRepository _repository = customerRepository;

    public async Task AddCustomerAsync(Customer customer)
    {
        var result = await _repository.AddCustomerAsync(customer);
        if (result) System.Console.WriteLine("Customer added successfully!");
        else System.Console.WriteLine("Something went wrong!");
    }

    public async Task GetCustomerAsync(int id)
    {
        var customer = await _repository.GetCustomerAsync(id);
        if (customer == null) System.Console.WriteLine("Nothing found!");
        else System.Console.WriteLine(customer.Id + " | " + customer.Fullname);
    }

    public async Task GetCustomersAsync()
    {
        var customers = await _repository.GetCustomersAsync();
        foreach (var customer in customers)
        {
            System.Console.WriteLine(customer.Id + " | " + customer.Fullname);
        }
    }

    public async Task DeleteCustomerAsync(int id)
    {
        var result = await _repository.DeleteCustomerAsync(id);
        if (result) System.Console.WriteLine("Customer deleted successfully!");
        else System.Console.WriteLine("Something went wrong!");
    }
}
