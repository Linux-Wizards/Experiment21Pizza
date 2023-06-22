public enum Size
{
    XS = 0,
    S = 1,
    M = 2,
    L = 3,
    XL = 4,
}

public class FoodItem
{
    public required string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
    public Size Size { get; set; }
    public double Price { get; set; }
    public bool IsDone { get; set; } // remove if not needed
}
