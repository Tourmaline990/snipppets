public class EnrollmentService
{
    private string _courseSessionId;
    private string _learnerId;
    private ForumManager _forumManager;
    private AccountManager _accountManager;
    private CourseCatalog _courseCatalog;
    private EnrollmentApplicationStatus _enrollmentApplicationStatus;
    public EnrollmentService(ForumManager forumManager,string sessionId,string learnerId,AccountManager accountManager,CourseCatalog courseCatalog)
    {
        _forumManager = forumManager;
        _courseSessionId = sessionId;
         _learnerId = learnerId;
        _accountManager = accountManager;
        _courseCatalog = courseCatalog;
        _enrollmentApplicationStatus = EnrollmentApplicationStatus._applied;
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
    public void ReviewApplication()
    {
        if (_enrollmentApplicationStatus != EnrollmentApplicationStatus._applied)
        {
            throw new InvalidOperationException("No ongoing application. cannot review");
        }
        ValidateInput(_courseSessionId);
        ValidateInput(_learnerId);
        _enrollmentApplicationStatus = EnrollmentApplicationStatus._pendingApproval;
    }
    public void AcceptApplication()
    {
        if (_enrollmentApplicationStatus != EnrollmentApplicationStatus._pendingApproval)
        {
            throw new InvalidOperationException("Not awaiting approval, cannot accept");
        }
        if (_accountManager.VerifyAccount(_learnerId) != typeof(Learner))
        {
            RejectApplication();
            throw new ArgumentNullException(nameof(_learnerId),"Application rejected, Learner not found.");
        }
        if (!_courseCatalog.VerifyCourseSession(_courseSessionId))
        {
            throw new ArgumentNullException(nameof(_courseSessionId),"CourseSession not found.");
        }
        _enrollmentApplicationStatus = EnrollmentApplicationStatus._accepted;
        Notify("Your application has been accepted. Enrollment in progress.");
    }
    public void RejectApplication()
    {
        if (_enrollmentApplicationStatus == EnrollmentApplicationStatus._rejected)
        {
            throw new Exception("Already Rejected.");
        }
        _enrollmentApplicationStatus = EnrollmentApplicationStatus._rejected;
    }
    public Enrollment? Enroll()
    {
        if (_enrollmentApplicationStatus != EnrollmentApplicationStatus._accepted)
        {
            throw new InvalidOperationException("Candidate has not been accepted, cannot enroll.");
        }
        Profile profile =  _accountManager.GetAccountProfile(_learnerId)!;
        Learner learner;
        if (profile is Learner learner1)
        {
            learner = learner1;
        }
        else
        {
            throw new InvalidOperationException("failed!");
        }
        CourseSession courseSession = _courseCatalog.GetCourseSession(_courseSessionId);
        if (courseSession.GetRegistrationStatus() != DispositionState._admitting)
        {
            switch (courseSession.GetRegistrationStatus())
            {
                case DispositionState._locked:
                   Notify("Enrollment failed! Admission is closed for this session. Check out other available sessions.");
                   break;
                case DispositionState._available:
                   Notify($"Enrollment failed! Admission is yet to begin for this session. Commences on {courseSession.GetEnrollmentStartDate()}");
                   break;
                case DispositionState._paused:
                   Notify("Enrollment failed! Admission is currently on hold for this session");
                   break;
            }
            RejectApplication();
            throw new Exception("Enrollment failed!");
            // an alternative for this action, save registration progress or figure something out.
        }
        if (courseSession.Availableslots() == 0)
        {
            RejectApplication();
            Notify("Class is full, check out other available sessions.");
            throw new Exception("Enrollment failed!");
        }
        Enrollment? enrolled = courseSession.GetEnrolledById(_learnerId);
        if (enrolled != null)
        {
            RejectApplication();
            Notify("You have been admitted into this session  already.");
            throw new Exception("Learner already registered in session");
        }
        Enrollment enrollment = new Enrollment(courseSession,_learnerId);
        courseSession.AddEnrolled(enrollment);
        learner.AddEnrollment(enrollment);
        Notify("Congratulation! You are now enrolled in this coursesession");
        return enrollment;
    }
    private void Notify(string text)
    {
       Profile profile = _accountManager.GetAccountProfile(_learnerId)!;
       Learner learner;
        if (profile is Learner learner1)
        {
            learner = learner1;
        }
        else
        {
            throw new InvalidOperationException("failed!");
        }
       learner.AddNotification(new Notification("Enrollment Team",$"{_courseSessionId}  => {text}",DateTime.Now));
    }
    
    public ForumMember? AssignForum()
    {
        CourseSession courseSession = _courseCatalog.GetCourseSession(_courseSessionId);
        ForumMember? member = _forumManager.AddEnrolled(_learnerId,_courseSessionId,_accountManager);
        return member;
    }

}
