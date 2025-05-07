using SmartLife.Models;

namespace SmartLife.Data;

public class ProductsMenu
{
    public IDictionary<string, IList<Product>> Products { get; set; }
    public IList<string> Categories { get; set; }
}