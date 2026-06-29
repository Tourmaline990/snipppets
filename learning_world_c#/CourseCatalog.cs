using System.Globalization;

public class CourseCatalog
{
    private List<CourseSession> _courseSessions = new List<CourseSession>();
     
    public void AddCourseSession(CourseSession courseSession)
    {
        if (courseSession.GetInitialState() != InitialState._launched)
        {
            throw new InvalidOperationException("Coursession is not launched.");
        }
        CourseSession? session = _courseSessions.Find(s => s.GetSessionId() == courseSession.GetSessionId());
        if (session != null)
        {
            throw new ArgumentException(nameof(courseSession), " SessionId is not unique.");
        }
        _courseSessions.Add(courseSession);
    }

    public void DisplayCourseSessions()
    {
        Console.WriteLine("Session Id  |  CourseId  | CourseTitle  |  Instructor  |   Enrollment Date  |  Commencement  |  Class Capacity  |  Term");
        foreach (CourseSession item in _courseSessions)
        {
            if (item.GetInitialState() == InitialState._launched && item.GetActiveStatus() != ActiveStatus._completed)
            {   
               Console.WriteLine($"{item.DisplayCourseSession()}");  
            }
        }
    }
    public void FilterByCourseId(string courseid)
    {
        if (string.IsNullOrEmpty(courseid))
        {
            throw new ArgumentNullException(nameof(courseid), "CourseId is Empty");
        }
        int num = 0;
       Console.WriteLine("Session Id  |  CourseId  | CourseTitle  |  Instructor  |   Enrollment Date  |  Commencement  |  Class Capacity  |  Term");
        foreach (CourseSession session in _courseSessions)
        {
            if (session.GetInitialState() == InitialState._launched && session.GetCourseId() == courseid)
            {
                Console.WriteLine(session.DisplayCourseSession());
                num++;
            }
        }
        if (num == 0)
        {
            throw new ArgumentNullException(nameof(courseid),$"No results for {courseid}");
        }
    }
    public CourseSession GetCourseSession(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
          throw new ArgumentNullException(nameof(sessionId), "Session Id is empty");
        }
        CourseSession? courseSession = _courseSessions.Find(S => S.GetSessionId() == sessionId);
        if (courseSession == null || courseSession.GetInitialState() != InitialState._launched)
        {
            throw new ArgumentNullException(nameof(sessionId), "No result matches query");
        }
        return courseSession;
    }
    public bool VerifyCourseSession(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
        {
            throw new ArgumentNullException(nameof(sessionId), "provided Id is Empty");
        }
        CourseSession? session = _courseSessions.Find(S => S.GetSessionId() == sessionId);
        if (session != null && session.GetInitialState() == InitialState._launched )
        {
            return true;
        }
        return false;
    }
}
