public class ForumMember
{
    private string _learnerId;
    private Forum _forum;
    private List<Notification> _inbox = new List<Notification>();
    private MemberStatus _status;
    private MemberInterruptionState _interruptionState;
    public ForumMember(string learnerId,Forum forum)
    {
        _learnerId = learnerId;
        _forum = forum;
        _status = MemberStatus._NA;
        _interruptionState = MemberInterruptionState._none;
    }
    public void AddNotification(Notification notification)
    {
        _inbox.Add(notification);
    }
    public string GetId()
    {
        return _learnerId;
    }
    public void AskQuestion(string question)
    {
        if (_status == MemberStatus._removed || _interruptionState == MemberInterruptionState._freezed)
        {
            throw new InvalidOperationException("Not available");
        }
        if (string.IsNullOrEmpty(question))
        {
            throw new ArgumentNullException(nameof(question),"Question is Empty");
        }
        Question question1 = new Question(question,_learnerId,DateTime.UtcNow);
        _forum.AddQuestion(question1);
        if (_status == MemberStatus._NA)
        {
            _status = MemberStatus._active;
        }
    }
    public void RespondToQuestion(int threadIndex,string text)
    {
        if (_status == MemberStatus._removed || _interruptionState == MemberInterruptionState._freezed)
        {
            throw new InvalidOperationException("Not available");
        }
        if (threadIndex < 1 || threadIndex >= _forum.ThreadCount())
        {
            throw new ArgumentException(nameof(threadIndex),"Index out of range");
        }
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text),"Question is Empty");
        }
       Response response = new Response(text,_learnerId,DateTime.UtcNow);
       Thread thread  = _forum.GetThread(threadIndex);
       thread.AddResponse(response);
       string text1 = $"{response.GetCaller()} Responded to a question. For more details, view Forum";
       _forum.Notify(text1);
       if (_status == MemberStatus._NA)
        {
            _status = MemberStatus._active;
        }
    }
    public void ViewForum()
    {
        if (_status == MemberStatus._removed || _interruptionState == MemberInterruptionState._freezed)
        {
            throw new InvalidOperationException("Not available");
        }
        _forum.DisplayForum();
    }
    public void ViewYourResponses()
    {
        if (_status == MemberStatus._removed || _interruptionState == MemberInterruptionState._freezed)
        {
            throw new InvalidOperationException("Not available");
        }
        _forum.GetResponsesByCaller(_learnerId);
    }
    public void ViewYourQuestions()
    {
        if (_status == MemberStatus._removed || _interruptionState == MemberInterruptionState._freezed)
        {
            throw new InvalidOperationException("Not available");
        }
        _forum.GetQuestionByCaller(_learnerId);
    }
    public MemberStatus GetStatus()
    {
        if (_forum.GetForumState() == ForumActivityStatus._closed && _status == MemberStatus._NA)
        {
            _status = MemberStatus._dormant;
        }
        return _status;
    }
    public void Remove()
    {
        if (_status == MemberStatus._removed)
        {
            throw new  InvalidOperationException("Already Deleted");
        }
        _status = MemberStatus._removed;
    }
    public void FreezeAccount()
    {
        if (_interruptionState == MemberInterruptionState._freezed)
        {
            throw new InvalidOperationException("Already Frozen");
        }
        _interruptionState = MemberInterruptionState._freezed;
    }
}