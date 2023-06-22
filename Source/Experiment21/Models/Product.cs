using System.ComponentModel.DataAnnotations;

namespace Experiment21.Models;

public class Product
{
    [Key]
    [Required(ErrorMessage = "ID is required")]
    public required int Id { get; set; }
    [Required(ErrorMessage = "Product name is required")]
    public required string Name { get; set; }
    [Required(ErrorMessage = "Product price is required")]
    public required decimal Price { get; set; }
}
