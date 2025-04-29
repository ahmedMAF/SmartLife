using SmartLife.Data;

namespace SmartLife.Models;

public class PartnerClient
{
    public int Id { get; set; }
    public string Image { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public PcType Type { get; set; }
}
