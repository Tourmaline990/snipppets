public class InstructorDeactivatedEvent : Event
{
    private string _profileId;
    private InstructorActiveStatus _status;
    public InstructorDeactivatedEvent(string profileId,InstructorActiveStatus activeStatus,DateTime dateTime): base(dateTime)
    {
        _profileId = Utility.ValidateString(profileId);
        _status = activeStatus;
    }
    public string GetprofileId()
    {
        return _profileId;
    }
    public InstructorActiveStatus GetInstructorActiveStatus()
    {
        return _status;
    }
    
}