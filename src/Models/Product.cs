using SmartLife.Data;

namespace SmartLife.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NameAr { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string DescriptionAr { get; set; }
    public string Category { get; set; }
    public string CategoryAr { get; set; }
    public int OrderIndex { get; set; }
    public List<SubModule> Features { get; set; } = [];
    public List<SubModule> Models { get; set; } = [];
    public List<GalleryEntry> Photos { get; set; } = [];
    public List<string> Videos { get; set; } = [];
}
