using SmartLife.Models;

namespace SmartLife.Data;

public class ProductsMenu
{
    public Dictionary<string, List<Product>> Products { get; set; } = null!;
    public List<string> Categories { get; set; } = null!;
}