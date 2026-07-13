public class InstructorRegisteredEvent : Event
{
    private string _accId;

    public InstructorRegisteredEvent(string accid,DateTime occuredOn): base(occuredOn)
    {
        _accId = Utility.ValidateString(accid);
    }
    public string GetId()
    {
        return _accId;
    }
}