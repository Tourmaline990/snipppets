public class LearnerDeactivatedEvent : Event
{
    private string _learnerId;
    private  readonly List<string> _allEnrollmentsId;
    public LearnerDeactivatedEvent(string learnerId, DateTime occuredOn, List<string> ids) : base(occuredOn)
    {
        _learnerId = Utility.ValidateString(learnerId);
        _allEnrollmentsId = ids;
    }
    public string GetLearnerId()
    {
        return _learnerId;
    }
    public List<string> GetEnrollmentIds()
    {
        return _allEnrollmentsId;
    }
}