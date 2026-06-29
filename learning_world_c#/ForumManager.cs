public class ForumManager
{
    private List<Forum> _availableForums = new List<Forum>();

    public ForumMember? AddEnrolled(string LearnerId,string SessionId,AccountManager manager)
    {
        Profile profile = manager.GetAccountProfile(LearnerId)!;
        Learner learner;
        if (profile is Learner learner1)
        {
            learner = learner1;
        }
        else
        {
            throw new InvalidOperationException("Failed!");
        }
        ForumMember forumMember;
        Forum forum =  GetForum(SessionId);
        if (forum.VerifyLearner(LearnerId))
        {
            throw  new InvalidOperationException("Already in forum");
        }
        forumMember = new ForumMember(LearnerId,forum);
        forum.AddMember(forumMember); 
        learner.AddNotification(new Notification("Enrollment Team",$"You have been added to {forum.GetForumName()}.",DateTime.UtcNow));
        return forumMember;
    }
    public void AssignSessionToForum(string sessionId,CourseCatalog catalog)
    {
        Forum? forum = _availableForums.Find(F => F.GetInitialStatus() == ForumInitialStatus._drafed);
        if (forum == null)
        {
            throw new NullReferenceException("No available forums at the moment");
        }
        forum.publish(sessionId,catalog);
    }
    public void AddForum(Forum forum)
    {
        if (forum.GetInitialStatus() != ForumInitialStatus._drafed)
        {
            throw new InvalidOperationException("Forum is not drafted, failed!");
        }
        _availableForums.Add(forum);
    }
    public Forum GetForum(string SessionId)
    {
        if (string.IsNullOrEmpty(SessionId))
        {
            throw new ArgumentNullException(nameof(SessionId), "Provided id is empty");
        }
        Forum? forum = _availableForums.Find(f => f.GetInitialStatus() == ForumInitialStatus._published && f.GetSessionId() == SessionId);
        if (forum == null)
        {
            throw new NullReferenceException("Not found");
        }
        return forum;
    }
    public void RemoveClosedForum()
    {
        foreach (Forum item in _availableForums)
        {
            if (item.GetInitialStatus() == ForumInitialStatus._published && item.GetForumState() == ForumActivityStatus._closed)
            {
                item.Archive();
            }
        }
    }
    public int GetAvailableSessionCount()
    {
       return _availableForums.FindAll(f=> f.GetInitialStatus() == ForumInitialStatus._drafed).Count;
    }
    public void ViewActiveForumDetail()
    {
        int num = 1;
        foreach(Forum item in _availableForums.FindAll(f => f.GetInitialStatus() == ForumInitialStatus._published))
        {
            Console.WriteLine($"Name:  {item.GetForumName()}");
            Console.WriteLine($"CourseSession  details:   {item.GetSessionId()}");
            num ++;
        }
        Console.WriteLine($"Total:  {num}");
    }
}