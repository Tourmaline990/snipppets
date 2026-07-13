public class StaffManagement
{
    private readonly AccountManager _accountManager;
    private readonly ForumManager _forumManager;
    private List<Notification> _inbox = new List<Notification>();
    public StaffManagement(AccountManager manager,ForumManager forumManager)
    {
        _accountManager = manager;
        _forumManager = forumManager;
    }
    public InstructorActiveStatus? InstructorLookup(string ProfileId)
    {
       if(_accountManager.VerifyAccount(ProfileId) == typeof(Instructor))
        {
           Instructor instructor = _accountManager.GetInstructor(ProfileId)!;
           return instructor.GetInstructorActiveStatus()!;
        }
        return null;
    }
    public void ReplaceInstructor(string ProfileId)
    {
        InstructorActiveStatus? activeStatus = InstructorLookup(ProfileId);
        if (activeStatus == InstructorActiveStatus.revoked)
        {
            throw new InvalidOperationException("Instructor is not recognised. Revoked already.");
        }
        if (activeStatus == InstructorActiveStatus._available)
        {
            return;
        }
        // account for a suspended instructor;
        Instructor instructor = _accountManager.GetInstructor(ProfileId)!;
        string sessionId = instructor.GetCourseSession().GetSessionId();
        CourseSession courseSession = instructor.GetCourseSession();
        Instructor replacement = _accountManager.GetAvailableInstructor();
        replacement.AllocateSession(courseSession,_forumManager.GetForum(sessionId));
    }
    public void AddNotification(Notification notification)
    {
        _inbox.Add(notification);
    }
}