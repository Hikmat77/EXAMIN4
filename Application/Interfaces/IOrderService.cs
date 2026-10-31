using Domain.Models;

namespace Application.Interfaces;

public interface IOrderService
{
    Task AddOrderAsync(Order order);
    Task GetOrderAsync(int id);
    Task GetOrdersByCustomerAsync(int customerId);
    Task DeleteOrderAsync(int id);

    // JOIN
    Task GetOrdersWithCustomersAsync();

    // SUM, COUNT, GROUP BY, ORDER BY
    Task GetTotalAmountAsync();
    Task GetOrdersCountByCustomerAsync();
    Task GetTop3CustomersAsync();
}