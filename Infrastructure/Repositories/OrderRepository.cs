using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    public async Task<bool> AddOrderAsync(Order order)
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = "insert into orders (customer_id, amount) values (@customerId, @amount)";
        var result = await connection.ExecuteAsync(query, order);
        return result > 0;
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = "select id, customer_id CustomerId, amount, created_at CreatedAt from orders where id = @id";
        var order = await connection.QuerySingleOrDefaultAsync<Order>(query, new { id = id });
        return order;
    }

    public async Task<List<Order>> GetOrdersByCustomerAsync(int customerId)
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = "select id, customer_id CustomerId, amount, created_at CreatedAt from orders where customer_id = @customerId order by id";
        var orders = await connection.QueryAsync<Order>(query, new { customerId = customerId });
        return orders.ToList();
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = "delete from orders where id = @id";
        var result = await connection.ExecuteAsync(query, new { id = id });
        return result > 0;
    }

    // ---------------- JOIN ----------------
    public async Task<List<Order>> GetOrdersWithCustomersAsync()
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = @"select o.id Id, o.customer_id CustomerId, c.fullname CustomerName, o.amount Amount, o.created_at CreatedAt
                      from orders o
                      join customers c on c.id = o.customer_id
                      order by o.id";
        var list = await connection.QueryAsync<Order>(query);
        return list.ToList();
    }

    // ---------------- SUM ----------------
    public async Task<decimal> GetTotalAmountAsync()
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = "select sum(amount) from orders";
        var total = await connection.QuerySingleOrDefaultAsync<decimal>(query);
        return total;
    }

    // ---------------- COUNT + GROUP BY ----------------
    public async Task<List<Customer>> GetOrdersCountByCustomerAsync()
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = @"select c.id Id, c.fullname Fullname, count(o.id)::int as OrdersCount
                      from customers c
                      left join orders o on o.customer_id = c.id
                      group by c.id, c.fullname
                      order by c.id";
        var list = await connection.QueryAsync<Customer>(query);
        return list.ToList();
    }

    // ---------------- TOP 3 (SUM + GROUP BY + ORDER BY) ----------------
    public async Task<List<Customer>> GetTop3CustomersAsync()
    {
        using var connection = Database.Connection();
        await connection.OpenAsync();
        var query = @"select c.id Id, c.fullname Fullname, sum(o.amount) as TotalAmount
                      from customers c
                      join orders o on o.customer_id = c.id
                      group by c.id, c.fullname
                      order by TotalAmount desc
                      limit 3";
        var list = await connection.QueryAsync<Customer>(query);
        return list.ToList();
    }
}