using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public async Task<bool> AddCustomerAsync(Customer customer)
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = "insert into customers (fullname) values (@fullname)";
        var result = await connection.ExecuteAsync(query, customer);
        return result > 0;
    }

    public async Task<Customer?> GetCustomerAsync(int id)
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = "select id, fullname from customers where id = @id";
        var customer = await connection.QuerySingleOrDefaultAsync<Customer>(query, new { id = id });
        return customer;
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = "select id, fullname from customers order by id";
        var customers = await connection.QueryAsync<Customer>(query);
        return customers.ToList();
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = "delete from customers where id = @id";
        var result = await connection.ExecuteAsync(query, new { id = id });
        return result > 0;
    }
}