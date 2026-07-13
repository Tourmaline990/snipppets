public abstract class Event
{
    private string _eventId;
    private DateTime _dateOccured;
    public Event(DateTime occuredOn)
    {
        _dateOccured = occuredOn;
        _eventId = "";
    }
    public DateTime GetEventDate()
    {
        return _dateOccured;
    }
}