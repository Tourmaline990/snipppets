using System.ComponentModel.DataAnnotations;

public class Question : Message
{
    private bool _isComplete = false;
   
    public Question(string text,string caller,DateTime date):base(text,caller,date)
    {
       
    }
    public string IsCompleteDisplay()
    {
        if (_isComplete)
        {
            return $"[√]";
        }
        else
        {
            return $"[X]";
        }
    }
    public void SetIsCompleted(bool value)
    {
        _isComplete = value;
    }
    public override string Display()
    {
        return $"{GetCaller()} Asked: {GetText()} \n {GetDate()}";
    }
    
    
}