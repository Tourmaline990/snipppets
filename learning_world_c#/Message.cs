public abstract class Message
{
    private string _text;
    private DateTime _date; 
    private string _callerName;
    private MessageStatus _messageStatus;
    
    public Message(string text,string caller,DateTime date)
    {
        _text = text;
        _callerName = caller;
        _date = date;
        _messageStatus = MessageStatus._draft;
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
    public void Send()
    {
        if (_messageStatus == MessageStatus._sent)
        {
            throw new Exception("Already sent");
        }
        if (_messageStatus != MessageStatus._draft)
        {
            throw new Exception("Only drafted message can be sent");
        }
       ValidateInput(_text);
       _callerName =  ValidateInput(_callerName).ToLower();
        if (_date != DateTime.Now)
        {
            throw new Exception("Unrecognised date format");
        }
        _messageStatus = MessageStatus._sent;
    }
    public MessageStatus GetMessageStatus()
    {
        return _messageStatus;
    }
    public void Delete()
    {
        if (_messageStatus == MessageStatus._deleted)
        {
            throw new Exception("Already deleted.");
        }
        _messageStatus = MessageStatus._deleted;
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
    //
}