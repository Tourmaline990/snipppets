public class EnrollmentService
{
    private CourseSession _CourseSession;
    private Learner _learner;
    private ForumManager _forumManager;
    public EnrollmentService(ForumManager forumManager,CourseSession CourseSession,Learner learner)
    {
        _forumManager = forumManager;
        _CourseSession = CourseSession;
        _learner = learner;
    }
    public Enrolled EnrollLearner()
    {
        Enrolled enroll;
        _learner.AddCourseSession(_CourseSession);
         enroll = new Enrolled(_CourseSession,_learner);
         _CourseSession.AddEnrolled(enroll);
         return enroll;
    }
   
    public ForumMember AssignForum()
    {
        ForumMember member = _forumManager.AddEnrolled(EnrollLearner(),_CourseSession);
        return member;
    }
    
    


}
