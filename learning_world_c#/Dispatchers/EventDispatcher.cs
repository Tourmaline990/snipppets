using System.Reflection;

public class EventDispatcher
{
    private readonly EmailHandler _emailHandler;
    private readonly StaffingHandler _staffingHandler;
    private readonly EnrollmentHandler _enrollmentHandler;
    private readonly ForumMemberHandler _forumMemberHandler;
    
 
    public EventDispatcher(StaffManagement staffManagement,AccountManager manager,ForumManager forumManager,CourseCatalog courseCatalog)
    {
       _emailHandler = new EmailHandler(manager);
       _staffingHandler = new StaffingHandler (staffManagement);
       _enrollmentHandler = new EnrollmentHandler(courseCatalog);
       _forumMemberHandler = new ForumMemberHandler(forumManager,manager);
    }
    public void Dispatch(Event evt)
    {
        switch (evt)
        {
            case LearnerDeactivatedEvent learnerDeactivated:
               _enrollmentHandler.Handle(learnerDeactivated);
               _forumMemberHandler.Handle(learnerDeactivated);
               _emailHandler.Handle(learnerDeactivated);
               break;
            case InstructorDeactivatedEvent instructorDeactivated:
              _emailHandler.Handle(instructorDeactivated);
              _staffingHandler.Handle(instructorDeactivated);
              break;
            case InstructorRegisteredEvent instructorRegistered:
              _emailHandler.Handle(instructorRegistered);
              _forumMemberHandler.Handle(instructorRegistered);
              _staffingHandler.Handle(instructorRegistered);
              break;
            case LearnerRegisteredEvent learnerRegistered:
              _emailHandler.Handle(learnerRegistered);
              break;
        }
    }
    
}