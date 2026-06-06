using System.Data.SqlTypes;

public class CourseSession : Session
{
    private List<Enrolled> _enrolleds = new List<Enrolled>();
    private Course _course;
    private string _term;
    private Instructor _instructor;
    private string _sessionId;
    private int _classCapacity;
   

    public CourseSession(Course course,DateTime SessionStartdate,string term, Instructor instructor, string sessionId,DateTime enrollmentStartperiod,DateTime enrollmentEndperiod,int classcapacity) : base(SessionStartdate,enrollmentStartperiod,enrollmentEndperiod,SessionStartdate.AddDays(course.LessonCount() * 7))
    {
        _course = course;
         _term= ValidateInput(term);
        _instructor = instructor;
        _sessionId = ValidateInput(sessionId);
        _classCapacity = classcapacity;
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
    public bool GetIsClosed()
    { 
        return GetSessionIsClosed();
    }
    public string DisplayCourseSession()
    {
        return $"{_sessionId}  |   {_course.GetId()}  |  {_course.GetTitle()}  |  {_instructor.GetName()}  |  {DisplaySession()}  | {_enrolleds.Count} of {_classCapacity}  |  {_term} ";
    }
    public string GetSessionId()
    {
        return _sessionId;
    }
    public Course GetCourse(bool isAnOfficial = false)
    {
        if (isAnOfficial)
        {
            if (SessionIsOpen())
            {
                throw new Exception("Course Session is ongoing, changes cannot be made to course");
            }
            return _course;
        }
        if (!SessionIsOpen())
        {
            throw new ArgumentException($"Course is currently unavailable, Opens {GetSessionStartDate()} ");
        }
        if (GetSessionIsClosed())
        {
            throw new Exception("CourseSession is closed.");
        }
        
        return _course;
    }
    public bool SubmissionDeadline(int index)
    {
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
   
    public string GetCourseId()
    {
        return _course.GetId();
    }
    public string GetInstructorInfo()
    {
        return _instructor.DisplayInstructor();
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
        if (_enrolleds.Count  == _classCapacity)
        {
            throw new ArgumentException(nameof(enrolled),"Class capacity cannot be exceeded.");
        }
        _enrolleds.Add(enrolled);
     }
    public void AllEnrolledDetails()
    {
        foreach (Enrolled item in _enrolleds)
        {
            Console.WriteLine($">>>  Learner-Id: {item.GetLearner().GetLearnerId()}");
            item.ViewGrade();  
            Console.WriteLine();
        }
    }
    public string GetTerm()
    {
        return _term;
    }
    public Enrolled GetEnrolledById(string learnerId)
    {
        if (string.IsNullOrEmpty(learnerId))
        {
            throw  new ArgumentException(nameof(learnerId), "Learner Id is empty");
        }
        Enrolled? enrolled = _enrolleds.Find( E => E.GetLearner().GetLearnerId() == learnerId);
        if (enrolled == null)
        {
            throw new ArgumentException(nameof(learnerId), " No such learner.");
        }
        return  enrolled;   
        
    }
    public int EnrolledStudentsCount()
    {
        return _enrolleds.Count;
    }
    public void ViewEnrolledNotInForum()
    {
        int num = 1;
        foreach (Enrolled item in _enrolleds)
        {
            if (!item.GetInForum())
            {
                Console.WriteLine(item.GetLearner().GetLearnerId());
                num++;
            }
        }
        if (num == 1)
        {
            throw new Exception("All Enrolled Students are in forum.");
        }
    }
 ///
}