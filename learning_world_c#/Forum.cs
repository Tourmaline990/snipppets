using System.Globalization;

public class Forum
{
    private List<ForumMember> _members = new  List<ForumMember>();
    private CourseSession _courseSession;
    private List<Thread> _threads = new List<Thread>();
    private bool _IsClosed = false;
    private string _name;
    public Forum(CourseSession courseSession, string name)
    {
        _courseSession = courseSession;
        _name = name;
    }
    public int ThreadCount()
    {
        return _threads.Count - 1;
    }
    public void AddMember(ForumMember forumMember)
    {
        if (GetIsClosed())
        {
            throw new Exception("Forum is closed for the block.");
        }
        _members.Add(forumMember);
        Notify("A new member has been admitted.",DateTime.Now);
    }
    public CourseSession GetCourseSession()
    {
        return _courseSession;
    }
    
    public Thread GetThread(int Index)
    {
        if (GetIsClosed())
        {
            throw new Exception("Forum is closed for the block.");
        }
        if (Index > _threads.Count || Index < 1 )
        {
            throw new ArgumentOutOfRangeException(nameof(Index),"Index out of Range");
        }
        return _threads[Index - 1];
    }
    public void AddThread(Thread thread)
    {
        if (GetIsClosed())
        {
            throw new Exception("Forum is closed for the block.");
        }
        _threads.Add(thread);
    }
    public void AddQuestion(Question question)
    {
        if (GetIsClosed())
        {
            throw new Exception("Forum is closed for the block.");
        }
        Thread thread = new Thread(question);
        AddThread(thread);
        string caller = question.GetCaller();
        string text = $"{caller} Added a question in forum, view forum for more details.";
        Notify(text,DateTime.Now);
    }
    public void DisplayForum()
    {
        int num = 1;
        Console.WriteLine($"{_name}");
        foreach (Thread item in _threads)
        { 
            Console.WriteLine($"{num}.");
            item.Display();
            Console.WriteLine();
            num ++;
        }
       
    }
    public void GetQuestionByCaller(string caller)
    {
        foreach (Thread item in _threads)
        {
            if (item.GetQuestion().GetCaller() == caller)
            {
                Console.WriteLine(item.GetQuestion().Display());
                Console.WriteLine(item.GetQuestion().IsCompleteDisplay());
                Console.WriteLine();
            }
        }
    }
    public void GetResponsesByCaller(string Caller)
    {
        foreach (Thread completed in _threads) 
        {
            completed.FilterResponse(Caller);
        }
    }
    public void Notify(string text,DateTime date)
    {
        Notification notification  = new Notification("Forum",text,date);
        foreach (ForumMember item in _members)
        {
            item.GetEnrolled().AddNotification(notification);
        }
    }
    public bool GetIsClosed()
    {
        if (_courseSession.GetIsClosed())
        {
            return _IsClosed = true;
        }
        return _IsClosed;
    }
    public string GetForumName()
    {
        return _name;
    }
    
        
    
}
