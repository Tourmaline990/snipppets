public class Notification : Message
{
    
    public Notification(string caller,string message,DateTime date) : base(message,caller,date)
    {
       
    }
    public override string Display()
    {
        return $"From {GetCaller()} \n >>>> {GetText()}\n{GetDate()}";
    }
///
}