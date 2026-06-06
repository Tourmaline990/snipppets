public abstract class Message
{
    private string _text;
    private DateTime _date; 
    private string _callerName;
    
    public Message(string text,string caller,DateTime date)
    {
        _text = ValidateInput(text);
        _callerName = ValidateInput(caller).ToLower();
        _date = date;
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
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
    ///
}