using System.Globalization;

public class CourseCatalog
{
    private List<CourseSession> _courseSessions = new List<CourseSession>();
     
     public bool CheckSessionAvailability(string sessionId)
     {
       if (string.IsNullOrEmpty(sessionId))
       {
            throw new ArgumentNullException(nameof(sessionId),"Invalid Session Id.");
       }
       CourseSession? session = _courseSessions.Find(s => s.GetSessionId() == sessionId);
        if (session == null)
        {
            throw new ArgumentNullException(nameof(sessionId), "Invalid Id");
        }
       if (DateTime.Today < session?.GetEnrollmentEndDate().Date && DateTime.Today > session?.GetEnrollmentStartDate().Date)
       {
          return true;
       }
        return false;
     }
    public void AddCourseSession(CourseSession courseSession)
    {
        _courseSessions.Add(courseSession);
    }

    public void DisplayCourseSessions()
    {
        Console.WriteLine("Session Id  |  CourseId  |  Instructor  |   StartDate  | EnrollMent Period  |  Class Capacity  |  Term");
        foreach (CourseSession item in _courseSessions)
        {
            Console.WriteLine($"{item.DisplayCourseSession()}");
            
        }
    }
    public void FilterByCourseId(string courseid)
    {
        if (string.IsNullOrEmpty(courseid))
        {
            throw new ArgumentException(nameof(courseid), "CourseId is Empty");
        }
        Console.WriteLine("Session Id  |  CourseId  |  Instructor  |   StartDate  | EnrollMent Period  |  Class Capacity  |  Term");
        foreach (CourseSession session in _courseSessions)
        {
            if (session.GetCourseId() == courseid)
            {
                Console.WriteLine(session.DisplayCourseSession());
            }
        }
    }
    public CourseSession GetCourseSession(string sessionId)
    {
        CourseSession? courseSession = _courseSessions.Find(S => S.GetSessionId() == sessionId);
        if (courseSession == null)
        {
            throw new ArgumentNullException(nameof(sessionId), "Session Id Inaccurate");
        }
        return courseSession;
    }
    

}
