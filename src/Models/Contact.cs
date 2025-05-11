using System.ComponentModel.DataAnnotations;

namespace SmartLife.Models;

public class Contact
{
    [Key]
    public string Country { get; set; }
    public List<string> Addresses { get; set; } = [];
    public List<string> Emails { get; set; } = [];
    public List<string> Phones { get; set; } = [];
    public List<string> WhatsApps { get; set; } = [];
}
