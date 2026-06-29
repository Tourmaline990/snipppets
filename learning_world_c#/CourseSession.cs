using System.Data.SqlTypes;

public class CourseSession : Session
{
    private List<Enrollment> _enrolleds = new List<Enrollment>();
    private Course _course;
    private string _term;
    private Instructor _instructor;
    private string _sessionId;
    private int _classCapacity;
    public CourseSession(Course course,DateTime SessionStartdate,string term, Instructor instructor, string sessionId,DateTime enrollmentStartperiod,DateTime enrollmentEndperiod,int classcapacity) : base(SessionStartdate,enrollmentStartperiod,enrollmentEndperiod,SessionStartdate.AddDays(course.LessonCount() * 7))
    {
        _course = course;
         _term= term;
        _instructor = instructor;
        _sessionId = sessionId;
        _classCapacity = classcapacity;
    }
    public int Availableslots()
    {
        return _classCapacity - _enrolleds.Count;
    }
    protected override void Validation()
    {
        ValidateInput(_term);
        ValidateInput(_sessionId);
        if (_classCapacity <= 0 || _classCapacity > 30)
        {
            throw new ArgumentOutOfRangeException(nameof(_classCapacity), "Class capacity ranges from 0 -30 ");
        }
        if (_course.GetStatus() != CourseMaterialStatus._published)
        {
            throw new ArgumentException(nameof(_course), "Course has not been published cannot schedule a session for it.");
        }
        if (_enrolleds.Count != 0)
        {
            throw new Exception("Cannot launch a coursession with registered student.");
        }
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
    public string DisplayCourseSession()
    {
        return $"{_sessionId}  |   {_course.GetId()}  |  {_course.GetTitle()}  | Instructor  {_instructor.GetProfileName()}  |  {DisplaySession()}  | {_enrolleds.Count} of {_classCapacity}  |  {_term} | {GetRegistrationStatus()}";
    }
    public Course GetCourse()
    {
        if (GetActiveStatus() != ActiveStatus._ongoing)
        {
            throw new Exception("Course is currently unavailable.");
        }
        return _course;
    }
    public bool SubmissionDeadline(int index)
    {
        if (GetActiveStatus() != ActiveStatus._ongoing)
        {
            throw new Exception("Course is currently unavailable.");
        }
        if (index >= _course.LessonCount() || index < 0)
        {
           throw new ArgumentOutOfRangeException(nameof(index),"Index out of range.");
        }
        if (Deadline().AddDays(index * 7) == DateTime.Today)
        {
            return true;
        }
        return false; 
    }
    
    public void Notify(Notification notification) 
    {
        foreach (Enrollment item in _enrolleds)
        {
            item.AddNotification(notification);
        }
    }
     public void AddEnrolled(Enrollment enrolled)
     {
        switch (GetRegistrationStatus())
        {
            case DispositionState._locked:
              throw new Exception("Registration is closed for this session");
            case DispositionState._paused:
               throw new Exception("Registration is on hold");
            case DispositionState._available:
             throw new Exception("Registration has not begun");
        }
        if (_enrolleds.Count  == _classCapacity)
        {
            CloseRegistration();
            throw new ArgumentException(nameof(enrolled),"Class is full");
        }
        _enrolleds.Add(enrolled);
     }
    public void AllEnrolledDetails()
    {
        if (GetActiveStatus() == ActiveStatus._inActive)
        {
            throw new Exception("No data yet. Session is not active.");
        }
        foreach (Enrollment item in _enrolleds)
        {
            Console.WriteLine($">>>  Learner-Id: {item.GetLearnerId()}");
            item.ViewGrade();  
            Console.WriteLine();
        }
    }
    public Enrollment? GetEnrolledById(string learnerId)
    {
        if (GetInitialState() != InitialState._launched)
        {
            throw new  Exception("Unrecognised session.");
        }
        if (_enrolleds.Count == 0)
        {
           throw new Exception("No enrolled student in session.");
        }
        if (string.IsNullOrEmpty(learnerId))
        {
            throw  new ArgumentException(nameof(learnerId), "Learner Id is empty");
        }
        Enrollment? enrolled = _enrolleds.Find( E => E.GetLearnerId() == learnerId);
        return  enrolled;   
    }
    public string GetSessionId()
    {
        return _sessionId;
    }
     public string GetCourseId()
    {
        return _course.GetId();
    }
    public string GetInstructorInfo()
    {
        return _instructor.DisplayInstructor();
    }
    public string GetTerm()
    {
        return _term;
    }
    public void ConfirmEligibility()
    {
      if (GetInitialState() != InitialState._launched)
        {
            throw new InvalidOperationException("Provided session is not available");
        }
        if (GetDispositionState() != DispositionState._available)
        {
            throw new InvalidOperationException("Failed! Activity is ongoing for provided session");
        }  
    }
    public void SetInstructor(Instructor instructor)
    {
        if (GetInitialState() == InitialState._launched && GetActiveStatus() != ActiveStatus._completed)
        {
            _instructor = instructor;
            return;
        }
        throw new InvalidOperationException("CourseSession is not launched or not active");
    }
    //
}