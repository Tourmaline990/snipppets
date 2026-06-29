public abstract class Session
{
    private  DateTime _sessionStartDate;
    private DateTime _sessionEndDate;
    private DateTime _sessionEnrollStartDate;
    private DateTime _sessionEnrollEndDate;
    private InitialState _initialState;
    private DispositionState  _dispositionState;
    private ActiveStatus _activeStatus = ActiveStatus._inActive;

      public Session(DateTime sessionStartDate,DateTime sessionEnrollStartDate, DateTime sessionEnrollEndDate, DateTime sessionEndDate)
      {
        _sessionEnrollEndDate = sessionEnrollEndDate;
        _sessionEnrollStartDate = sessionEnrollStartDate;
        _sessionStartDate = sessionStartDate;
        _sessionEndDate = sessionEndDate;
        _initialState = InitialState._drafted;
      }
    public DateTime GetEnrollmentStartDate()
    {
        return _sessionEnrollStartDate.Date;
    }
    public InitialState GetInitialState()
    {
       return _initialState; 
    }
    public DispositionState GetDispositionState()
    {
        return _dispositionState;
    }
    protected abstract void Validation();
    public void LaunchSession()
    {
        Validation();
        if (_initialState != InitialState._drafted)
        {
            throw new Exception("Only drafted sessions can be launched.");
        }
        if (_sessionEnrollStartDate.Date <= DateTime.Today)
        {
            throw new ArgumentException(nameof(_sessionEnrollStartDate),"Invalid date format.");
        }
        if (_sessionEnrollEndDate.Date <= _sessionEnrollStartDate.Date)
        {
            throw new ArgumentException(nameof(_sessionEnrollEndDate), "Invalid date format.");
        }
        if (_sessionStartDate.Date <= _sessionEnrollEndDate.Date)
        {
            throw new ArgumentException(nameof(_sessionStartDate),"Invalid date format");
        }
        if (_sessionEndDate.Date <= _sessionStartDate.Date)
        {
            throw new ArgumentException(nameof(_sessionEndDate),"Invalid date format");
        }
        _initialState = InitialState._launched;
        _dispositionState = DispositionState._available;
    }
    public void CancelSession()
    {
        if (_dispositionState != DispositionState._available)
        {
            throw new Exception("Session cannot be cancelled, operations already in progress");
        }
        if (_initialState == InitialState._cancelled)
        {
            throw new Exception("Already Cancelled.");
        }
        _initialState = InitialState._cancelled;
    }
    public DispositionState GetRegistrationStatus()
    {
        if (_initialState != InitialState._launched)
        {
            throw new Exception("Unrecognised session, it has not been launched.");
        }
        if (_dispositionState == DispositionState._available)
        {
            if (DateTime.Today < _sessionEnrollStartDate.Date)
             {
                 return _dispositionState;
             }
            if (DateTime.Today >=  _sessionEnrollStartDate.Date && DateTime.Today <= _sessionEnrollEndDate.Date )
             {
                return _dispositionState = DispositionState._admitting;
             }
        }
        if (DateTime.Today > _sessionEnrollEndDate.Date)
        {
           return _dispositionState = DispositionState._locked;
        }
        return _dispositionState;
    }
    public void CloseRegistration()
    {
        if (_initialState != InitialState._launched)
        {
            throw new Exception("Unrecognised session, it has not been launched.");
        }
        if (_dispositionState == DispositionState._locked)
        {
            throw new Exception("Already Locked.");
        }
        _dispositionState = DispositionState._locked;
    }
    public void PauseRegistration()
    {
        if (_initialState != InitialState._launched)
        {
            throw new Exception("Unrecognised session, it has not been launched.");
        }
        if (_dispositionState != DispositionState._admitting)
        {
            throw new Exception("Failed! Session cannot be paused.");
        }
        _dispositionState = DispositionState._paused;
    }
    public void ResumeRegistration()
    {
        if (_initialState != InitialState._launched)
        {
            throw new Exception("Unrecognised session, it has not been launched.");
        }
        if (_dispositionState != DispositionState._paused)
        {
            throw new Exception("Cannot resume session. Not paused.");
        }
        if (DateTime.Today > _sessionEnrollEndDate )
        {
            throw new Exception("Specified date has been met.");
        }
        _dispositionState = DispositionState._admitting;
    }
    public ActiveStatus GetActiveStatus()
    {
        if (_initialState != InitialState._launched)
        {
            throw new Exception("Unrecognised session, it has not been launched.");
        }
        if (DateTime.Today >= _sessionStartDate.Date && DateTime.Today < _sessionEndDate.Date)
        {
            _activeStatus = ActiveStatus._ongoing;
        }
        else if (DateTime.Today >= _sessionEndDate.Date)
        {
            _activeStatus = ActiveStatus._completed;
        }
        return _activeStatus;
    }
    public void PauseSession()
    {
        if (_activeStatus == ActiveStatus._paused)
        {
            throw new Exception("Already Paused");
        }
        if (_activeStatus != ActiveStatus._ongoing)
        {
            throw new Exception("Cannot pause session. Failed");
        }
        _activeStatus = ActiveStatus._paused;
    }
    public virtual DateTime Deadline() 
    {
       int deadlineGrace = 2 * 7;
       return _sessionStartDate.Date.AddDays(deadlineGrace);
    }
    public bool LastWeek() 
    {
        if (_sessionEndDate.Date.AddDays(-7) == DateTime.Today)
        {
            return true;
        }
        return false;
    }
    public string DisplaySession()
    {
        return $"{_sessionEnrollStartDate} / {_sessionEnrollEndDate}  |  {_sessionStartDate} / {_sessionEndDate}";
    }
   //
}