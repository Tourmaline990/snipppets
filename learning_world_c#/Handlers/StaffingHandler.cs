public class StaffingHandler
{
    private readonly StaffManagement _staffManagement;
   
    public StaffingHandler(StaffManagement management)
    {
        _staffManagement = management;
    } 
    public void Handle(InstructorDeactivatedEvent deactivatedEvent)
    {
        _staffManagement.ReplaceInstructor(deactivatedEvent.GetprofileId());
    }
}