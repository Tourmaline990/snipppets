public class InstructorDeactivatedEvent
{
    private string _profileId;
    private InstructorActiveStatus _status;
    private DateTime _dateTime;

    public InstructorDeactivatedEvent(string profileId,InstructorActiveStatus activeStatus,DateTime dateTime)
    {
        _profileId = Utility.ValidateString(profileId);
        _status = activeStatus;
        _dateTime = dateTime;
    }
    public string GetprofileId()
    {
        return _profileId;
    }
    public InstructorActiveStatus GetInstructorActiveStatus()
    {
        return _status;
    }
    public DateTime GetEventDate()
    {
        return _dateTime;
    }
}