using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Experiment21.Models;
public class Order
{
    [Key]
    [Required(ErrorMessage = "ID is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Customer name is required")]
    public string CustomerName { get; set; } = default!;
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; } = default!;
    [Required(ErrorMessage = "Phone number is required")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = default!;

    public ICollection<OrderDetail> OrderDetails { get; set; } = default!;
    [AllowNull]
    public string? Notes { get; set; }
    [Required(ErrorMessage = "Order status is required")]
    public OrderStatus Status { get; set; } // Enum

    [Required(ErrorMessage = "Total cost is required")]
    public decimal TotalCost { get; set; }
    [Required(ErrorMessage = "Shipping costs are required")]
    public decimal ShippingCost { get; set; }
}

public class OrderDetail
{
    [Key]
    [Required(ErrorMessage = "ID is required")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Order ID is required")]
    public int OrderId { get; set; }
    [Required(ErrorMessage = "Product ID is required")]
    public required int ProductId { get; set; }

    [Required(ErrorMessage = "Order is required")]
    public Order Order { get; set; } = default!;
    [Required(ErrorMessage = "Product is required")]
    public Product Product { get; set; } = default!;
    [Required(ErrorMessage = "Quantity is required")]
    public required int Quantity { get; set; }
}

public enum OrderStatus
{
    Received = 0,
    BeingPrepared = 1,
    ReadyForPickup = 2,
    InDelivery = 3,
    Delivered = 4
}
