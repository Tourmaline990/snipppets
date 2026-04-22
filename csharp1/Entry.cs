using System.Data.SqlTypes;


    
public class Entry
{
    private string _reflection;
    private string _date;

    public Entry(string date, string reflection)
    {
        _date = date;
        _reflection =  reflection;
    }

    public string DisplayFormat()
    {
        return $"{_date}\n > {_reflection}";
    }

}