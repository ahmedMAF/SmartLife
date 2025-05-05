namespace SmartLife.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string> Images { get; set; }
    public DateTime Time { get; set; }
}
