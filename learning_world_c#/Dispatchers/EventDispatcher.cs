using System.Reflection;

public class EventDispatcher
{
    private readonly EmailHandler _emailHandler;
    private readonly StaffingHandler _staffingHandler;
    
 
    public EventDispatcher(StaffManagement staffManagement,AccountManager manager)
    {
       _emailHandler = new EmailHandler(manager);
       _staffingHandler = new StaffingHandler (staffManagement);
    }
    public void Dispatch(InstructorDeactivatedEvent deactivatedEvent)
    {
        _emailHandler.Handle(deactivatedEvent);
        _staffingHandler.Handle(deactivatedEvent);
    }
}