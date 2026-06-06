public class ForumManager
{
    private List<Forum> _availableForums = new List<Forum>();
    private List<Forum> _closedForums = new List<Forum>();
    public ForumMember? AddEnrolled(Enrolled enrolled,CourseSession courseSession)
    {
        ForumMember forumMember;
        Forum forum =  GetForum(courseSession);
        try
        {
          forumMember =  forum.GetMember(enrolled);
            if (forumMember != null)
            {
                throw new ArgumentNullException(nameof(enrolled)," Enrolled student exists in forum");
            }
        }
        catch
        {
           forumMember = new ForumMember(enrolled,forum);
           forum.AddMember(forumMember); 
        }
       return forumMember;
    }
    public void AddForum(Forum forum)
    {
       Forum? ForumExist =  _availableForums.Find(F => F.GetCourseSession().GetSessionId() == forum.GetCourseSession().GetSessionId());
        if (ForumExist != null)
        {
            throw new ArgumentException("Forum already exists for courseSession");
        }
        if (forum.GetCourseSession().SessionIsOpen())
        {
            throw new Exception("CourseSession has begun, Forum cannot be added");
        }
        _availableForums.Add(forum);
    }
    public Forum GetForum(CourseSession courseSession)
    {
        Forum? forum = _availableForums.Find(f => f.GetCourseSession() == courseSession);
        if (forum == null)
        {
            throw new ArgumentOutOfRangeException(nameof(courseSession),"Forum does not exist for  courseSession.");
        }
        return forum;
    }
    public void RemoveClosedForum()
    {
        foreach (Forum item in _availableForums)
        {
            if (item.GetIsClosed())
            {
                _closedForums.Add(item);
                Console.WriteLine($"{item.GetForumName()} is closed and has been removed.");
                _availableForums.Remove(item);
            }
        }
    }
    ///
}