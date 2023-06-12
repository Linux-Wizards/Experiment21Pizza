/// <summary>
/// Class <c>Address</c> represents an address where food can be delivered.
/// </summary>
public class Address
{
    /// <summary>
    /// Variable <c>Street</c> stores the street - one part of the address.
    /// </summary>
    public String Street { get; set; }
    /// <summary>
    /// Variable <c>City</c> stores the city - one part of the address.
    /// </summary>
    public String City { get; set; }
    /// <summary>
    /// Variable <c>Number</c> stores the building and (if required) room number - one part of the address.
    /// </summary>
    public String Number { get; set; }
}
