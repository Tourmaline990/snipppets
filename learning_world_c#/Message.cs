public abstract class Message
{
    private string _text;
    private DateTime _date; 
    private string _callerName;
    
    public Message(string text,string caller,DateTime date)
    {
        _text = text;
        _callerName = caller;
        _date = date;
    }
    
    public abstract string Display();
    
    public string GetCaller()
    {
        return _callerName;
    }
    public string GetText()
    {
        return _text;
    }
    public DateTime GetDate()
    {
        return _date;
    }
}