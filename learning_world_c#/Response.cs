public class Response: Message
{
  

   public Response(string text,string caller,DateTime date): base(text,caller,date)
    {
       
        
    }
    public override string Display()
    {
        return $"{GetCaller()} Responded: {GetText()}\n {GetDate()}";
    }
    
}