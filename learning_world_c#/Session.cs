public class Session
{
    private  DateTime _sessionStartDate;
    private DateTime _sessionEndDate;
    private DateTime _sessionEnrollStartDate;
    private DateTime _sessionEnrollEndDate;

     public Session(DateTime sessionStartDate,DateTime sessionEnrollStartDate, DateTime sessionEnrollEndDate)
      {
        _sessionEnrollEndDate = sessionEnrollEndDate;
        _sessionEnrollStartDate = sessionEnrollStartDate;
        _sessionStartDate = sessionStartDate;
      }
      public Session(DateTime sessionStartDate,DateTime sessionEnrollStartDate, DateTime sessionEnrollEndDate, DateTime sessionEndDate)
      {
        _sessionEnrollEndDate = ValidateStartDate(sessionEnrollEndDate,sessionEnrollEndDate);
        _sessionEnrollStartDate = sessionEnrollStartDate;
        _sessionStartDate = ValidateStartDate(sessionStartDate,sessionEndDate);
        _sessionEndDate = sessionEndDate;
      }
    private DateTime ValidateStartDate(DateTime startdate,DateTime enddate)
    {
        DateTime date;
       if(enddate < startdate && startdate < DateTime.Today || startdate != DateTime.Today)
        {
            throw new ArgumentOutOfRangeException(nameof(startdate),"Invalid date format");
        }
       
        return date = startdate;
    }
    public virtual DateTime Deadline() 
    {
       int deadlineGrace = 2 * 7;
       return _sessionStartDate.Date.AddDays(deadlineGrace);
    }
    public void SetSessionEndDate(DateTime date) 
    {
        _sessionEndDate = date;
    }
    public bool GetSessionIsClosed() 
    {
        if (_sessionEndDate.Date >= DateTime.Today)
        {
            return true;
        }
        return false;
    }
    public bool LastWeek() 
    {
        if (_sessionEndDate.Date.AddDays(-7) == DateTime.Today)
        {
            return true;
        }
        return false;
    }
    public bool SessionIsOpen() 
    {
        if (DateTime.Today >= _sessionStartDate)
        {
            return true;
        }
        return false;
    }
    public bool EnrollmentIsOngoing() 
    {
        if (DateTime.Today <= _sessionEnrollEndDate && DateTime.Today >= _sessionEnrollStartDate)
           {
               return true;
           }
        return false;
    }
    public string DisplaySession()
    {
        return $"{_sessionEnrollStartDate} / {_sessionEnrollEndDate}  |  {_sessionStartDate} / {_sessionEndDate}";
    }
    public DateTime GetSessionStartDate()
    {
        return _sessionStartDate;
    }
    public DateTime GetEndDate()
    {
        return _sessionEndDate;
    }
    ///
}