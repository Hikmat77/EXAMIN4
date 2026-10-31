using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    private IOrderRepository _repository = orderRepository;

    public async Task AddOrderAsync(Order order)
    {
        var result = await _repository.AddOrderAsync(order);
        if (result) System.Console.WriteLine("Order added successfully!");
        else System.Console.WriteLine("Something went wrong!");
    }

    public async Task GetOrderAsync(int id)
    {
        var order = await _repository.GetOrderAsync(id);
        if (order == null) System.Console.WriteLine("Nothing found!");
        else System.Console.WriteLine(order.Id + " | " + order.CustomerId + " | " + order.Amount + " | " + order.CreatedAt);
    }

    public async Task GetOrdersByCustomerAsync(int customerId)
    {
        var orders = await _repository.GetOrdersByCustomerAsync(customerId);
        foreach (var order in orders)
        {
            System.Console.WriteLine(order.Id + " | " + order.CustomerId + " | " + order.Amount + " | " + order.CreatedAt);
        }
    }

    public async Task DeleteOrderAsync(int id)
    {
        var result = await _repository.DeleteOrderAsync(id);
        if (result) System.Console.WriteLine("Order deleted successfully!");
        else System.Console.WriteLine("Something went wrong!");
    }

    // ---------------- JOIN ----------------
    public async Task GetOrdersWithCustomersAsync()
    {
        var orders = await _repository.GetOrdersWithCustomersAsync();
        System.Console.WriteLine("OrderId | CustomerName | Amount | CreatedAt");
        foreach (var order in orders)
        {
            System.Console.WriteLine(order.Id + " | " + order.CustomerName + " | " + order.Amount + " | " + order.CreatedAt);
        }
    }

    // ---------------- SUM ----------------
    public async Task GetTotalAmountAsync()
    {
        var total = await _repository.GetTotalAmountAsync();
        System.Console.WriteLine("Total amount of all orders: " + total);
    }

    // ---------------- COUNT + GROUP BY ----------------
    public async Task GetOrdersCountByCustomerAsync()
    {
        var customers = await _repository.GetOrdersCountByCustomerAsync();
        foreach (var customer in customers)
        {
            System.Console.WriteLine(customer.Id + " | " + customer.Fullname + " | " + customer.OrdersCount);
        }
    }

    // ---------------- TOP 3 ----------------
    public async Task GetTop3CustomersAsync()
    {
        var customers = await _repository.GetTop3CustomersAsync();
        foreach (var customer in customers)
        {
            System.Console.WriteLine(customer.Id + " | " + customer.Fullname + " | " + customer.TotalAmount);
        }
    }
}
