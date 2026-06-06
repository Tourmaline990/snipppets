using System.Globalization;

public class CourseCatalog
{
    private List<CourseSession> _courseSessions = new List<CourseSession>();
     
    
    public void AddCourseSession(CourseSession courseSession)
    {
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
            Console.WriteLine($"{item.DisplayCourseSession()}");  
        }
    }
    public void FilterByCourseId(string courseid)
    {
        if (string.IsNullOrEmpty(courseid))
        {
            throw new ArgumentException(nameof(courseid), "CourseId is Empty");
        }
        int num = 0;
       Console.WriteLine("Session Id  |  CourseId  | CourseTitle  |  Instructor  |   Enrollment Date  |  Commencement  |  Class Capacity  |  Term");
        foreach (CourseSession session in _courseSessions)
        {
            if (session.GetCourseId() == courseid)
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
        if (courseSession == null)
        {
            throw new ArgumentNullException(nameof(sessionId), "No result matches query");
        }
        return courseSession;
    }
    
 ///
}
