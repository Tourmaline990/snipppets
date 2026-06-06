public class EnrollmentService
{
    private CourseSession _CourseSession;
    private Learner _learner;
    private ForumManager _forumManager;
    private AccountManager _accountManager;
    public EnrollmentService(ForumManager forumManager,CourseSession CourseSession,Learner learner,AccountManager accountManager)
    {
        _forumManager = forumManager;
        _CourseSession = ValidateCourseSession(CourseSession);
        _learner = learner;
        _accountManager = accountManager;
    }
    public Enrolled? EnrollLearner()
    {
        Enrolled enroll;
        if (!_accountManager.VerifyLearner(_learner.GetLearnerId()))
        {
            throw new Exception("Learner not recognised.");
        }
        try
        {   
            enroll =  _CourseSession.GetEnrolledById(_learner.GetLearnerId());
            if (enroll != null)
            {
                throw new Exception($"{_learner.GetLearnerId()} is currently enrolled in this session.");
            }
        }
        catch
        {   
            _learner.AddCourseSession(_CourseSession);
            enroll = new Enrolled(_CourseSession,_learner);
            _CourseSession.AddEnrolled(enroll);
        }
         return enroll;
    }
   
    public ForumMember? AssignForum(Enrolled enrolled)
    {
        ForumMember? member = _forumManager.AddEnrolled(enrolled,_CourseSession);
        return member;
    }
    private CourseSession ValidateCourseSession(CourseSession courseSession)
    {
        if (!courseSession.EnrollmentIsOngoing())
        {
            throw new Exception("Enrollment is closed");
        }
        return courseSession;
    }
    

 ///
}
