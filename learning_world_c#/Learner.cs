public class Learner
{
    private List<CourseSession> _Enrolled_courses = new List<CourseSession>();
    private string _learner_id;
    public Learner(string learner_id)
    {
        _learner_id = ValidateInput(learner_id);
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
    public string GetLearnerId()
    {
        return _learner_id;
    }
    
    public void ViewEnrolledCourses()
    {
        foreach (CourseSession courseSession in _Enrolled_courses)
        {
            Console.WriteLine($"Session Id:  {courseSession.GetSessionId()}  | Instructor: {courseSession.GetInstructorInfo()}  |  Course:  {courseSession.GetCourseId()}  | Term:  {courseSession.GetTerm()}  | Closed:  {courseSession.GetEndDate()}| Final Grade:  {courseSession.GetEnrolledById(_learner_id).FinalGrade()}");
        }
    }
    public void AddCourseSession(CourseSession courseSession)
    {
        _Enrolled_courses.Add(courseSession);
    }
    ///
}