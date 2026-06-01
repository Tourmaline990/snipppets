public class ForumManager
{
    private List<Forum> _availableForums = new List<Forum>();
    private List<Forum> _closedForums = new List<Forum>();
    public ForumMember AddEnrolled(Enrolled enrolled,CourseSession courseSession)
    {
       Forum forum =  GetForum(courseSession);
       ForumMember forumMember = new ForumMember(enrolled,forum);
       forum.AddMember(forumMember);
       return forumMember;
    }
    public void AddForum(Forum forum)
    {
        _availableForums.Add(forum);
    }
    public Forum GetForum(CourseSession courseSession)
    {
        Forum? forum = _availableForums.Find(f => f.GetCourseSession() == courseSession);
        if (forum == null)
        {
            throw new ArgumentOutOfRangeException(nameof(courseSession),"Forum does not exist for that course.");
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
}