public class Enrolled
{
    private CourseSession _courseSession;
    private Learner _learner;
    private Grade _grade;
    private bool _inForum;
    private List<Notification> _notifications = new List<Notification>();
    private int _lesson_progress = 0;
    public Enrolled(CourseSession courseSession,Learner learner)
    {
        _courseSession = courseSession;
        _learner = learner;
        _grade = new Grade(_courseSession.GetCourse(true));
        _inForum = false;
    }
    public Learner GetLearner()
    {
        return _learner;
    }
    public void SetInForum(bool value)
    {
        _inForum = value;
    }
    public bool GetInForum()
    {
        return _inForum;
    }
    public void ViewGrade()
    {
            _grade.PercentageCompleted(_lesson_progress);
            _grade.CompletedScorePercent(_lesson_progress);
            _grade.TotalGrade();
            _grade.ViewGrade();  
    }
    public void Submit(Lesson lesson)
    {
        if (_courseSession.SubmissionDeadline(_lesson_progress))  
        {
            throw new ArgumentException(" Submission Failed. Assignment is locked.");
        }
        if (_courseSession.GetCourse().GetLesson(_lesson_progress) != lesson)
         {
            throw new ArgumentException(nameof(lesson),"Lesson Object Not Recognized.");
         }
        if (!lesson.GetIsCompleted())
        {
            throw new ArgumentException(nameof(lesson),"Lesson is not complete.");
        }
        _grade.updateLessonScore(_lesson_progress,lesson.GetLessonScore());
        _lesson_progress ++;
}
    public Lesson TakeLesson()
    {
        Lesson lesson;
        if (_lesson_progress >= _courseSession.GetCourse().LessonCount())
        {
            throw new ArgumentOutOfRangeException("Index out of range (Present Progress is greater than available course Lessons).");
        }
        if (_courseSession.GetCourse().GetLesson(_lesson_progress).GetIsCompleted())
        {
            throw new Exception("Error! learner's progress corrupted.");
        }
        if (!_courseSession.GetCourse().GetLesson(_lesson_progress).GetIsCompleted() == true)
        {
            if (_courseSession.SubmissionDeadline(_lesson_progress))
              {
                  _lesson_progress = _lesson_progress + 1;
                  lesson = _courseSession.GetCourse().GetLesson(_lesson_progress);
              }
            else
            {
                lesson = _courseSession.GetCourse().GetLesson(_lesson_progress);
            }
            return lesson;     
        }
        
        return  _courseSession.GetCourse().GetLesson(_lesson_progress);
    }
    public void ViewNotifications()
    {
        foreach (Notification item in _notifications)
        {
            Console.WriteLine(item.Display());
        }
    }
    public void AddNotification(Notification notification) 
    {
        _notifications.Add(notification);
    }
    public string FinalGrade()
    {
        return  _grade.GetTotalGrade();
    }
   ///
}