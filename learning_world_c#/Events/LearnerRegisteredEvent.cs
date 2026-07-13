public class LearnerRegisteredEvent : Event
{
    private string _accId;
    public LearnerRegisteredEvent(string accId, DateTime occuredOn): base(occuredOn)
    {
        _accId = Utility.ValidateString(accId);
    }
    public string GetId()
    {
        return _accId;
    }
}