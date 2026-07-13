public class ForumMemberHandler
{
    private readonly ForumManager _forumManager;
    private readonly AccountManager _accountManager;
    public ForumMemberHandler(ForumManager forumManager,AccountManager manager)
    {
        _forumManager = forumManager;
        _accountManager = manager;
    }
    public void Handle(Event evt)
    {
        switch (evt)
        {
           case LearnerDeactivatedEvent learnerDeactivated:
              foreach (string item in learnerDeactivated.GetEnrollmentIds())
                {
                  _forumManager.GetForum(item).RemoveMember(learnerDeactivated.GetLearnerId());
                } 
                break;
            case InstructorRegisteredEvent instructorRegistered:
               _forumManager.AddMember(instructorRegistered.GetId(),"working on it",_accountManager);
               break;
               // a forum for all instructors
        }
        
    }
}