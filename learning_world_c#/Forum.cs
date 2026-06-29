using System.Globalization;

public class Forum
{
    private List<ForumMember> _members = new  List<ForumMember>();
    private string _sessionId = null!;
    private List<Thread> _threads = new List<Thread>();
    private string _name;
    private  ForumInitialStatus _forumInitialStatus;
    private ForumActivityStatus _activityStatus;
    public Forum(string name)
    {
        _forumInitialStatus = ForumInitialStatus._drafed;
        _name = name;
    }
    public ForumInitialStatus GetInitialStatus()
    {
        return _forumInitialStatus;
    }
    public List<Dictionary<string,MemberStatus>> GetMemberParticipationStatus()
    {
        if (_forumInitialStatus == ForumInitialStatus._drafed)
        {
            throw new InvalidOperationException("Failed! Not available");
        }
        List<Dictionary<string,MemberStatus>> data = new List<Dictionary<string, MemberStatus>>();
        foreach (ForumMember item in _members)
        {
           data.Add(new Dictionary<string, MemberStatus>{{item.GetId(),item.GetStatus()}});
        }
        return data;
    }
    public void publish(string sessionId, CourseCatalog catalog)
    {
        // Possible design endpoint: Different department may need a forum, a baseclass catalog can be 
        // created, where we validate sessionId before publishing forum.
        if (_forumInitialStatus != ForumInitialStatus._drafed)
        {
            throw new InvalidOperationException("failed!, not drafted");
        }
        CourseSession courseSession = catalog.GetCourseSession(sessionId);
        courseSession.ConfirmEligibility();
        if (_members.Count != 0 || _threads.Count != 0)
        {
            throw new InvalidOperationException("Unidentified storage format");
        }
        _name = $"{courseSession.GetSessionId()} Forum";
        _activityStatus = ForumActivityStatus._available;
    }
    public int ThreadCount()
    {
        if (_forumInitialStatus == ForumInitialStatus._drafed)
        {
            throw new InvalidOperationException("Failed! Not available");
        }
        return _threads.Count;
    }
    public void AddMember(ForumMember forumMember)
    {
        if (_forumInitialStatus != ForumInitialStatus._published)
        {
            throw new InvalidOperationException("Not published");
        }
        _members.Add(forumMember);
        Notify("A new member has been admitted.");
    }
    public string GetSessionId()
    {
        if (_forumInitialStatus == ForumInitialStatus._drafed)
        {
            throw new InvalidOperationException("Failed! Not available");
        }
        return _sessionId;
    }
    public ForumActivityStatus GetForumState()
    {
        if (_forumInitialStatus != ForumInitialStatus._published)
        {
            throw new InvalidOperationException("Not published");
        }
        return _activityStatus;
    }
    public Thread GetThread(int Index)
    {
        if (_forumInitialStatus != ForumInitialStatus._published)
        {
            throw new InvalidOperationException("Not published");
        }
        if (_activityStatus != ForumActivityStatus._open)
        {
            throw new InvalidOperationException("Failed! Not available.");
        }
        if (Index >= _threads.Count || Index < 1 )
        {
            throw new ArgumentOutOfRangeException(nameof(Index),"Index out of Range");
        }
        return _threads[Index];
    }
    public void AddThread(Thread thread)
    {
        if (_forumInitialStatus != ForumInitialStatus._published)
        {
            throw new InvalidOperationException("Not published");
        }
        if (_activityStatus != ForumActivityStatus._open)
        {
            throw new InvalidOperationException("Failed! Not available.");
        }
        _threads.Add(thread);
    }
    public void AddQuestion(Question question)
    {
        if (_forumInitialStatus != ForumInitialStatus._published)
        {
            throw new InvalidOperationException("Not published");
        }
        if (_activityStatus != ForumActivityStatus._open)
        {
            throw new InvalidOperationException("Failed! Not available.");
        }
        Thread thread = new Thread(question);
        AddThread(thread);
        string caller = question.GetCaller();
        string text = $"{caller} Added a question in forum, view forum for more details.";
        Notify(text);
    }
    public void DisplayForum()
    {
        if (_forumInitialStatus == ForumInitialStatus._drafed)
        {
            throw new InvalidOperationException("Failed! Not available");
        }
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
        if (_forumInitialStatus == ForumInitialStatus._drafed)
        {
            throw new InvalidOperationException("Failed! Not available");
        }
        caller = ValidateInput(caller);
        caller = caller.ToLower();
        int num = 0;
        foreach (Thread item in _threads)
        {
            if (item.GetQuestion().GetCaller() == caller)
            {
                Console.WriteLine(item.GetQuestion().Display());
                Console.WriteLine(item.GetQuestion().DisplayState());
                Console.WriteLine();
                num++;
            }
        }
        if (num == 0)
        {
            throw new Exception($"No questions from caller {caller}");
        }
    }
    public void GetResponsesByCaller(string Caller)
    {
        if (_forumInitialStatus == ForumInitialStatus._drafed)
        {
            throw new InvalidOperationException("Failed! Not available");
        }
        Caller = ValidateInput(Caller);
        Caller = Caller.ToLower();
        foreach (Thread completed in _threads) 
        {
            completed.FilterResponse(Caller);
        }
    }
    public void Notify(string text)
    {
        foreach (ForumMember item in _members)
        {
            item.AddNotification(new Notification("Forum",text,DateTime.UtcNow));
        }
    }
    public string GetForumName()
    {
        return _name;
    }
    public ForumMember GetMember(string LearnerId)
    {
        if (_forumInitialStatus == ForumInitialStatus._drafed)
        {
            throw new InvalidOperationException("Failed! Not available");
        }
        if (!VerifyLearner(LearnerId))
        {
            throw new ArgumentNullException(nameof(LearnerId), "No result matches query");
        }
        ForumMember member = _members.Find(E => E.GetId() == LearnerId)!;
        return member;
    }
    public bool VerifyLearner(string learnerId)
    {
        if (_forumInitialStatus == ForumInitialStatus._drafed)
        {
            throw new InvalidOperationException("Failed! Not available");
        }
        if (string.IsNullOrEmpty(learnerId))
        {
            throw new ArgumentNullException(nameof(learnerId),"Provided  id is empty");
        }
        ForumMember? member = _members.Find(E => E.GetId() == learnerId);
        if (member == null)
        {
            return false;
        }
        return true;
    }
    public int PresentMembers()
    {
        if (_forumInitialStatus != ForumInitialStatus._published)
        {
            throw new InvalidOperationException("Not published");
        }
        return _members.Count;
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
    public void Archive()
    {
        if (_forumInitialStatus != ForumInitialStatus._archived)
        {
            throw new InvalidOperationException("Already archived");
        }
        if (_forumInitialStatus != ForumInitialStatus._published)
        {
            throw new InvalidOperationException("Cannot archive forum, not published ");
        }
        if (_activityStatus != ForumActivityStatus._closed)
        {
             throw new  InvalidOperationException("Activity is ongoing cannot archive");
        }
        _forumInitialStatus = ForumInitialStatus._archived;
    }
}
