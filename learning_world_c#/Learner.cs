public class Learner
{
    private List<CourseSession> _Enrolled_courses = new List<CourseSession>();
    private string _learner_id;
    public Learner(string learner_id)
    {
        _learner_id = learner_id;
    }
    public string GetLearnerId()
    {
        return _learner_id;
    }
    
    public void ViewEnrolledCourses()
    {
        foreach (CourseSession courseSession in _Enrolled_courses)
        {
            Console.WriteLine(courseSession.DisplayCourseSession());
        }
    }
    public void AddCourseSession(CourseSession courseSession)
    {
        _Enrolled_courses.Add(courseSession);
    }
}