using SmartLife.Models;

namespace SmartLife.Data;

public class ProductsMenu
{
    public IDictionary<int, IList<Product>> Products { get; set; }
    public IList<Category> Categories { get; set; }
}