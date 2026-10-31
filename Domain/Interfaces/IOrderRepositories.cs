using Domain.Models;

namespace Domain.Interfaces;

public interface IOrderRepository
{
    Task<bool> AddOrderAsync(Order order);
    Task<Order?> GetOrderAsync(int id);
    Task<List<Order>> GetOrdersByCustomerAsync(int customerId);
    Task<bool> DeleteOrderAsync(int id);

    // JOIN
    Task<List<Order>> GetOrdersWithCustomersAsync();

    // SUM, COUNT, GROUP BY, ORDER BY
    Task<decimal> GetTotalAmountAsync();
    Task<List<Customer>> GetOrdersCountByCustomerAsync();
    Task<List<Customer>> GetTop3CustomersAsync();
}