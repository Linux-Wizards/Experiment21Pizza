/// <summary>
/// Enum <c>Status</c> describes the current status of the order.
/// </summary>
public enum Status
{
    /// <summary>
    /// Value <c>Preparation</c> means that the order is being prepared.
    /// </summary>
    Preparation = 1,
    /// <summary>
    /// Value <c>Delivery</c> means that the order is being delivered.
    /// </summary>
    Delivery = 2,
    /// <summary>
    /// Value <c>Delivered</c> means that the order has been delivered.
    /// </summary>
    Delivered = 3,
}

/// <summary>
/// Class <c>Order</c> represents an order.
/// </summary>
public class Order
{
    /// <summary>
    /// List <c>FoodItems</c> is a list of all items ordered by the customer.
    /// </summary>
    public List<FoodItem> FoodItems { get; set; } = new();
    /// <summary>
    /// Variable <c>Status</c> stores the current status of the order.
    /// </summary>
    public Status Status { get; set; }
    /// <summary>
    /// Variable <c>Address</c> stores the address where the order needs to be delivered to.
    /// </summary>
    public Address Address { get; set; }
    /// <summary>
    /// Variable <c>PhoneNumber</c> stores the phone number of the person who placed the order.
    /// </summary>
    public String PhoneNumber { get; set; }
    /// <summary>
    /// Variable <c>CustomerName</c> stores the name of the customer placing the order.
    /// </summary>
    public String CustomerName { get; set; }
}
