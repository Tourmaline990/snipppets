using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

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
        if (_responses.Count < 1)
        {   
          _question.SetIsCompleted(true);
        }
        _responses.Add(response);
        
    }
    public void Display()
    {
        if (_responses.Count > 0)
        {
          Console.WriteLine($"{_question.Display()} \n \n {_question.IsCompleteDisplay()} ============ {_responses.Count}");
          foreach (Response item in _responses)
          {
            Console.WriteLine(item.Display());
           }  
        }
        else
        {
           Console.WriteLine($"{_question.Display()} \n {_question.IsCompleteDisplay()} =========== {_responses.Count}"); 
        }
        
    }
    public Question GetQuestion()
    {
        return _question;
    }
    public void FilterResponse(string CallerName)
    {
        if (string.IsNullOrEmpty(CallerName))
        {
            throw new ArgumentNullException(nameof(CallerName),"Empty Input");
        }
        CallerName = CallerName.ToLower();
        List<Response> CallerNameResponse = new List<Response>();
        
        foreach (Response item in _responses)
        {
            if (item.GetCaller() == CallerName)
            {
               CallerNameResponse.Add(item);
            }
        }
        if (CallerNameResponse.Count > 0)
        {
            Console.WriteLine("Search results: ");
            foreach (Response item in CallerNameResponse)
            {
                Console.WriteLine(item.Display());
            }
        }
        else
        {
            throw new ArgumentException(nameof(CallerName), "No search result matches query criteria");
        }
    }
   ///
}