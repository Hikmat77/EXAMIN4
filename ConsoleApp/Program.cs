using Application.Services;
using Domain.Models;
using Infrastructure.Repositories;

while (true)
{
    System.Console.WriteLine("======================================");
    System.Console.WriteLine(@"--- MAIN MENU ---
1) Customers.
2) Orders.
3) Reports (JOIN, SUM, COUNT, TOP 3).
0) Exit.");
    var main = Console.ReadLine();
    switch (main)
    {
        case "1":
            await CustomersMenu();
            break;
        case "2":
            await OrdersMenu();
            break;
        case "3":
            await ReportsMenu();
            break;
        case "0":
            System.Console.WriteLine("Exiting....");
            return;
    }
}


///////                                                           PART 1  (CUSTOMERS)

async Task CustomersMenu()
{
    var customerService = new CustomerService(new CustomerRepository());

    while (true)
    {
        System.Console.WriteLine("======================================");
        System.Console.WriteLine(@"--- CUSTOMERS MENU ---
1) Add new customer.
2) Show customer by id.
3) Show all customers.
4) Delete customer.
0) Back.");
        var a = Console.ReadLine();
        System.Console.WriteLine("======================================");
        switch (a)
        {
            case "1":
                System.Console.Write("Enter Fullname --> ");
                var name = Console.ReadLine()!;
                var newCustomer = new Customer()
                {
                    Fullname = name
                };
                await customerService.AddCustomerAsync(newCustomer);
                break;
            case "2":
                System.Console.Write("Enter customer id --> ");
                var id = Convert.ToInt32(Console.ReadLine());
                await customerService.GetCustomerAsync(id);
                break;
            case "3":
                await customerService.GetCustomersAsync();
                break;
            case "4":
                System.Console.Write("Enter id to delete --> ");
                var did = Convert.ToInt32(Console.ReadLine());
                await customerService.DeleteCustomerAsync(did);
                break;
            case "0":
                return;
        }
    }
}


///////                                                           PART 2  (ORDERS)

async Task OrdersMenu()
{
    var orderService = new OrderService(new OrderRepository());

    while (true)
    {
        System.Console.WriteLine("======================================");
        System.Console.WriteLine(@"--- ORDERS MENU ---
1) Create new order.
2) Show order by id.
3) Show all orders of customer.
4) Delete order.
0) Back.");
        var a = Console.ReadLine();
        System.Console.WriteLine("======================================");
        switch (a)
        {
            case "1":
                System.Console.Write("Enter Customer id --> ");
                var customerId = Convert.ToInt32(Console.ReadLine());
                System.Console.Write("Enter Amount --> ");
                var amount = Convert.ToDecimal(Console.ReadLine());
                var newOrder = new Order()
                {
                    CustomerId = customerId,
                    Amount = amount
                };
                await orderService.AddOrderAsync(newOrder);
                break;
            case "2":
                System.Console.Write("Enter order id --> ");
                var id = Convert.ToInt32(Console.ReadLine());
                await orderService.GetOrderAsync(id);
                break;
            case "3":
                System.Console.Write("Enter customer id --> ");
                var cid = Convert.ToInt32(Console.ReadLine());
                await orderService.GetOrdersByCustomerAsync(cid);
                break;
            case "4":
                System.Console.Write("Enter id to delete --> ");
                var did = Convert.ToInt32(Console.ReadLine());
                await orderService.DeleteOrderAsync(did);
                break;
            case "0":
                return;
        }
    }
}


///////                                                           PART 3 + 4  (JOIN, SUM, COUNT, TOP 3)

async Task ReportsMenu()
{
    var orderService = new OrderService(new OrderRepository());

    while (true)
    {
        System.Console.WriteLine("======================================");
        System.Console.WriteLine(@"--- REPORTS MENU ---
1) Orders with customer names (JOIN).
2) Total amount of all orders (SUM).
3) Orders count of each customer (COUNT).
4) Top 3 customers by orders amount.
0) Back.");
        var a = Console.ReadLine();
        System.Console.WriteLine("======================================");
        switch (a)
        {
            case "1":
                await orderService.GetOrdersWithCustomersAsync();
                break;
            case "2":
                await orderService.GetTotalAmountAsync();
                break;
            case "3":
                await orderService.GetOrdersCountByCustomerAsync();
                break;
            case "4":
                await orderService.GetTop3CustomersAsync();
                break;
            case "0":
                return;
        }
    }
}
