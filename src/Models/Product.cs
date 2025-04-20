namespace SmartLife.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public List<ProductFeature> Features { get; set; }
    public List<ProductModel> Models { get; set; }
    public List<string> Photos { get; set; }
    public List<string> Videos { get; set; }

}
