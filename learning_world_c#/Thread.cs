using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public class Thread
{
    private Question _question;
    private List<Response> _responses = new List<Response>();
    private ThreadVisibilityStatus _VisibilityStatus;
    private ThreadActiveStatus _activeStatus;

    public Thread(Question question)
    {
        _question = StartThread(question);
        _activeStatus = ThreadActiveStatus._new;
        _VisibilityStatus = ThreadVisibilityStatus._open;
    }
    private Question StartThread(Question question)
    {
        if (question.GetMessageStatus() != MessageStatus._sent)
        {
            throw new Exception("Failed! Unsent Message.");
        }
        return question;
    }
    public void AddResponse(Response response)
    {
        if (_activeStatus == ThreadActiveStatus._closed || _VisibilityStatus == ThreadVisibilityStatus._locked)
        {
            throw new Exception("Upload failed. Thread is closed.");
        }
        if (response.GetMessageStatus() != MessageStatus._sent)
        {
            throw new Exception("Failed! unsent message");
        }
         _responses.Add(response);
         _activeStatus = ThreadActiveStatus._ongoing;
    }
    private string ActiveStatusDisplay()
    {
        if (_activeStatus == ThreadActiveStatus._new)
        {
            return $"[New]";
        }
        else if (_activeStatus == ThreadActiveStatus._ongoing)
        {
            return $"[√]";
        }
        else
        {
            return $"[X]";
        }
    }
    private string VisibilityDisplay()
    {
        if (_VisibilityStatus == ThreadVisibilityStatus._open)
        {
            return $"[========]";
        }
        else
        {
            return $"[===== LOCKED]";
        }
    }
    public void Display()
    {
        if (_question.GetMessageStatus() == MessageStatus._deleted)
        {
            Console.WriteLine($"{_question.GetCaller()} deleted a question.");
        }
        else
        {
          Console.WriteLine(VisibilityDisplay());
          Console.WriteLine($"{_question.Display()} \n \n {ActiveStatusDisplay()} ============ {_responses.Count}");
          foreach (Response item in _responses)
          {
                if (item.GetMessageStatus() == MessageStatus._deleted)
                {
                    Console.WriteLine($"{item.GetCaller()} deleted a question.");
                }
                else
                {
                  Console.WriteLine(item.Display());
                }
           }   
        }
    }
    public void SetVisibilityStatus(ThreadVisibilityStatus state)
    {
        _VisibilityStatus = state;
    }
    public void CloseThread()
    {
        _activeStatus = ThreadActiveStatus._closed;
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
        int num = 0;
        Console.WriteLine("Search results: ");
        foreach (Response item in _responses)
        {
            if (item.GetCaller() == CallerName && item.GetMessageStatus() == MessageStatus._sent )
            {
               Console.WriteLine(item.Display());
               num++;
            }
        }
        if(num == 0)
        {
            throw new ArgumentException(nameof(CallerName), "No search result matches query criteria");
        }
    }
}