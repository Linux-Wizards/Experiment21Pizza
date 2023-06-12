public enum Status
{
    Preparation = 1,
    Delivery = 2,
    Delivered = 3,
}

public class Order
{
    public List<FoodItem> FoodItems { get; set; } = new();
}
