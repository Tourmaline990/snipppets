using System.Runtime.CompilerServices;

public class Thread
{
    private Question _question;
    private List<Response> _responses = new List<Response>();

    public Thread(Question question)
    {
        _question = question;
    }
    public void AddResponse(Response response)
    {
        _responses.Add(response);
        _question.SetIsCompleted(true);
        
    }
    public void Display()
    {
        if (_responses.Count > 0)
        {
          Console.WriteLine($"{_question.Display()} \n {_question.IsCompleteDisplay()} ============ {_responses.Count}");
          foreach (Response item in _responses)
          {
            Console.WriteLine(item.Display());
           }  
        }
        else
        {
           Console.WriteLine($"{_question.Display()} \n {_question.IsCompleteDisplay()} =========== 0"); 
        }
        
    }
    public Question GetQuestion()
    {
        return _question;
    }
    public void FilterResponse(string CallerName)
    {
        foreach (Response item in _responses)
        {
            if (item.GetCaller() == CallerName)
            {
                Console.WriteLine(item.Display());
            }
        }
    }
   
}