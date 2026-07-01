public class Learner : Profile
{
    private List<Enrollment> _Enrollments = new List<Enrollment>();
    private LearnerStatus _learnerStatus;
    private List<Notification> _inbox = new List<Notification>();
    public Learner(string learner_id,string name): base(name,learner_id)
    {
        _learnerStatus = LearnerStatus._live;
    }

    public void Apply(EnrollmentService enrollmentService)
    {
        if (_learnerStatus != LearnerStatus._live)
        {
            throw new InvalidOperationException("Unknown Learner");
        }
        enrollmentService.ReviewApplication();
        enrollmentService.AcceptApplication();
        enrollmentService.Enroll();
        enrollmentService.AssignForum();
    }
    public void AddNotification(Notification notification)
    {
        _inbox.Add(notification);
    }
    public void Expell()
    {
        _learnerStatus = LearnerStatus._expelled;
    }
    public void DeleteAccount(ForumManager manager)
    {
        foreach (Enrollment item in _Enrollments)
        {  
            if (item.GetEnrollmentStatus() == EnrollmentStatus._inProgress || item.GetEnrollmentStatus() == EnrollmentStatus._available)
            {
                item.Delete(manager);
            }
        }
        _learnerStatus = LearnerStatus._deleted;
        
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
    
    public LearnerStatus GetLearnerStatus()
    {
        return _learnerStatus;
    }
    
    public Enrollment ViewEnrollment(string sesssionId)
    {
        ValidateInput(sesssionId);
       Enrollment? enrollment =  _Enrollments.Find(E => E.GetSessonId() == sesssionId);
        if (enrollment == null)
        {
            throw new NullReferenceException("Not found.");
        }
       return enrollment;
    }
    public void AddEnrollment(Enrollment enrollment)
    {
        if (_learnerStatus != LearnerStatus._live)
        {
            throw new InvalidOperationException("Learner does not exist");
        }
        _Enrollments.Add(enrollment);
    }
}