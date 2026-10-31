namespace Domain.Models;

public class Customer
{
    public int Id { get; set; }
    public string Fullname { get; set; } = string.Empty;

    // Барои COUNT ва SUM (Part 4)
    public int OrdersCount { get; set; }
    public decimal TotalAmount { get; set; }
}