using SmartLife.Data;

namespace SmartLife.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public List<SubModule> Features { get; set; } = [];
    public List<SubModule> Models { get; set; } = [];
    public List<GalleryEntry> Photos { get; set; } = [];
    public List<GalleryEntry> Videos { get; set; } = [];

}
