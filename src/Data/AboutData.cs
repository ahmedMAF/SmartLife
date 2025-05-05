namespace SmartLife.Data;

public struct AboutData
{
    public string Countries = "4";
    public string Projects = "17K";
    public string Consultants = "15";
    public string Employees = "35";
    public string Clients = "10K";
    public string Branches = "7";
    public IList<(int, int)> Growth = [
        (2008, 1),
        (2017, 6),
        (2020, 13),
        (2022, 25),
        (2024, 35),
    ];

    public AboutData()
    {
    }
}
