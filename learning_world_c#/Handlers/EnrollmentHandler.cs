public class EnrollmentHandler
{
    private readonly CourseCatalog _courseCatalog;
    public EnrollmentHandler(CourseCatalog courseCatalog)
    {
        _courseCatalog = courseCatalog;
    }
    public void Handle(LearnerDeactivatedEvent learnerEvt)
    {
        foreach (string item in learnerEvt.GetEnrollmentIds())
        {
            _courseCatalog.GetCourseSession(item).CancelEnrollment(learnerEvt.GetLearnerId());
        }
    }
}