namespace SmartLife.Data;

public class ScriptModel
{
    public string Name { get; set; } = null!;
    public string Content { get; set; } = null!;
    public ScriptLocation Location { get; set; }
}
