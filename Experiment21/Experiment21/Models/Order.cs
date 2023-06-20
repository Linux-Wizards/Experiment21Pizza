using System.ComponentModel.DataAnnotations;

namespace Experiment21.Models;
public class Order
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Customer Name is required")]
    public string CustomerName { get; set; }
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
    public decimal ShippingCost { get; set; }
    public OrderStatus Status { get; set; } //Enum
    public ICollection<OrderDetail> OrderDetails { get; set; }
    public decimal TotalCost { get; set; }
}

public class OrderDetail
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}

public enum OrderStatus
{
    Received,
    BeingPrepared,
    ReadyForPickup,
    InDelivery,
    Delivered
}
