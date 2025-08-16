namespace SmartLife.Data;

public class AboutData
{
    public string Vision1 { get; set; }
    public string Vision1Ar { get; set; }
    public string Vision2 { get; set; }
    public string Vision2Ar { get; set; }
    public string Mission1 { get; set; }
    public string Mission1Ar { get; set; }
    public string Mission2 { get; set; }
    public string Mission2Ar { get; set; }
    public string Values { get; set; }
    public string ValuesAr { get; set; }
    public string Since { get; set; }
    public string SinceAr { get; set; }

    public string Countries { get; set; } = "4";
    public string Projects { get; set; } = "17K";
    public string Consultants { get; set; } = "15";
    public string Employees { get; set; } = "35";
    public string Clients { get; set; } = "10K";
    public string Branches { get; set; } = "7";
    public IList<Bar> Growth { get; set; } = [
        new("2008", "1"),
        new("2017", "6"),
        new("2020", "13"),
        new("2022", "25"),
        new("2024", "35"),
    ];

    public AboutData()
    {
    }
}

public class Bar
{
    public string Year { get; set; }
    public string Value { get; set; }

    public Bar()
    {
    }

    public Bar(string year, string val)
    {
        Year = year;
        Value = val;
    }
}
