using System.Data.SqlTypes;

public class CourseSession
{
    private List<Enrolled> _enrolleds = new List<Enrolled>();
    private Course _course;
    private DateTime _startDate;
    private bool _isClosed;
    private string _term;
    private Instructor _instructor;
    private string _sessionId;
    public DateTime  _enrollmentStartPeriod;
    public DateTime  _enrollmentEndPeriod;
    public int _classCapacity;
  

    public CourseSession(Course course,DateTime date,string term, Instructor instructor, string sessionId,DateTime enrollmentStartperiod,DateTime enrollmentEndperiod,int classcapacity)
    {
        _course = course;
        _startDate =  date;
        _isClosed = false;
         _term= term;
        _instructor = instructor;
        _sessionId = sessionId;
        _enrollmentStartPeriod = enrollmentStartperiod;
        _enrollmentEndPeriod = enrollmentEndperiod;
        _classCapacity = classcapacity;
    }
    
    public bool GetIsClosed()
    { 
        int num = 0;
        for (int i = 0; i < _course.LessonCount(); i++)
        {
            num += 7;
        }
        DateTime expectedTime =  _startDate.Date.AddDays(num);
        if (expectedTime == DateTime.Today)
        {
            return _isClosed = true;
        }
        return _isClosed;
    }
    public void SetIsClosed(bool value)
    {
        _isClosed = value;
    }
    public string DisplayCourseSession()
    {
        return $"{_sessionId}  |   {_course.GetId()}  |  {_instructor}  |  {_startDate}  | {_enrollmentStartPeriod} - {_enrollmentEndPeriod}  |  {_classCapacity}  |  {_term} ";
    }
    public string GetSessionId()
    {
        return _sessionId;
    }
    public Course GetCourse()
    {
        if (DateTime.Today < _startDate.Date)
        {
            throw new ArgumentException($"Course is currently unavailable, Opens {_startDate} ");
        }
        if (GetIsClosed())
        {
            throw new Exception("CourseSession is closed.");
        }
        
        return _course;
    }
    public bool SubmissionDeadline(int index)
    {
        if (index >= _course.LessonCount())
        {
           throw new ArgumentOutOfRangeException(nameof(index),"Index out of range.");
        }
        int actualDays = index * 7;
        int deadlinegrace = 2 * 7;
        int result = actualDays + deadlinegrace;
        if (_startDate.Date.AddDays(result) >= DateTime.Today)
        {
            return true;
        }
        return false;
    }
    public bool IsLastWeek()
    {
        int val = _course.LessonCount() - 1;
        if (DateTime.Today == _startDate.Date.AddDays(val * 7))
        {
             return true;
        }
        return false;
    
    }
    public DateTime GetStartDate()
    {
        return _startDate;
    }
    public string GetCourseId()
    {
        return _course.GetId();
    }
    public Instructor GetInstructor()
    {
        return _instructor;
    }
    public void Notify(Notification notification) 
    {
        foreach (Enrolled item in _enrolleds)
        {
            item.AddNotification(notification);
        }
    }
     public void AddEnrolled(Enrolled enrolled)
    {
        if (_enrolleds.Count - 1 == _classCapacity)
        {
            throw new ArgumentException(nameof(enrolled),"Class capacity cannot be exceeded.");
        }
        _enrolleds.Add(enrolled);
    }
    public void EnrolledDetails()
    {
        foreach (Enrolled item in _enrolleds)
        {
            Console.WriteLine($"Learner-Id: {item.GetLearner().GetLearnerId()}, Grade: {item.CheckGradeProgress()}, Course-Status: {item.CourseStatus()}, Lesson-Progress: {item.GetLessonProgress() + 1}/{_course.LessonCount()} ");
        }
    }
    public Enrolled GetEnrolledById(string learnerId)
    {
         int val = 0;
         bool state = false;
         for (int i = 0; i < _enrolleds.Count - 1; i++)
          {
            if (_enrolleds[i].GetLearner().GetLearnerId() == learnerId)
            {
                val = i;
                state = true;
                break;
            }
          }
        if (!state)
        {
            throw new ArgumentException(nameof(learnerId),"Invalid Learner Id");
        }
        return _enrolleds[val];
        
    }
    public DateTime GetEnrollmentEndDate()
    {
        return _enrollmentEndPeriod;
    }
    public DateTime GetEnrollmentStartDate()
    {
        return _enrollmentStartPeriod;
    }

    
}