public class StaffingHandler
{
    private readonly StaffManagement _staffManagement;
   
    public StaffingHandler(StaffManagement management)
    {
        _staffManagement = management;
    } 
    public void Handle(Event evt)
    {
        switch (evt)
        {
            case InstructorDeactivatedEvent instructorDeactivated:
                _staffManagement.ReplaceInstructor(instructorDeactivated.GetprofileId());
                break;
            case InstructorRegisteredEvent instructorRegistered:
               _staffManagement.AddNotification(new Notification("Registration team",$"New Instructor Alert : ID - {instructorRegistered.GetId()} time: {instructorRegistered.GetEventDate()}",DateTime.UtcNow));
               break;
        }
    }
}