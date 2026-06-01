public class Enrolled
{
    private CourseSession _courseSession;
    private Learner _learner;
    private List<Notification> _notifications = new List<Notification>();
    private int _lesson_progress = 0;
    public Enrolled(CourseSession courseSession,Learner learner)
    {
        _courseSession = courseSession;
        _learner = learner;
    }
    public Learner GetLearner()
    {
        return _learner;
    }
    public bool CourseStatus()
    {
        int num = 1;
        for (int i = 0; i < _courseSession.GetCourse().LessonCount(); i++)
        {
           Lesson lesson = _courseSession.GetCourse().GetLesson(i);
           if (lesson.GetIsCompleted())
            {
                num++;
            }

        }
        if (num == _courseSession.GetCourse().LessonCount())
        {
            return true;
        }
        return false;
    }
    public string CheckGradeProgress()
    {
        int val = 0;
        for (int i = 0; i < _lesson_progress; i++)
        {
           val += _courseSession.GetCourse().GetLesson(i).CalculateLessonScore();

        }
       int totalLessonsScore = _courseSession.GetCourse().LessonCount() * _courseSession.GetCourse().GetLesson(1).ExerciseNumber() * 2;
       int value = val/totalLessonsScore * 100;
       return $"{value}%";
    }
    public void Submit(Lesson lesson)
    {
        if (_courseSession.GetIsClosed())
        {
            throw new ArgumentException("Submission Failed. Course is closed for the block.");
        }
        if (_courseSession.SubmissionDeadline(_lesson_progress))  
        {
            throw new ArgumentException(" Submission Failed. Assignment is locked.");
        }
        if(!CourseStatus())
        {
            if (_courseSession.GetCourse().GetLesson(_lesson_progress) == lesson)
              {
               if (lesson.GetIsCompleted())
                {
                  _lesson_progress ++;
                }
                else
                {   
                  throw new ArgumentException(nameof(lesson),"Lesson is not complete.");
                }
             }
            else
            {
                throw new ArgumentException(nameof(lesson),"Lesson Object Not Recognized.");
            }
        }
        else{
        Notification notification = new Notification("Course Update","You have completed your course activity for this block. ",DateTime.Now);
        AddNotification(notification);
        // Notify.
        }
    }
    public Lesson TakeLesson()
    {
        Lesson lesson;
        if (_lesson_progress > _courseSession.GetCourse().LessonCount())
        {
            throw new ArgumentOutOfRangeException("Index out of range (Present Progress is greater than available course Lessons).");

        }
        if (!_courseSession.GetCourse().GetLesson(_lesson_progress).GetIsCompleted() == true)
        {
            lesson = _courseSession.GetCourse().GetLesson(_lesson_progress);
        }
        else
        {
            if (_courseSession.SubmissionDeadline(_lesson_progress))
            {
                _lesson_progress = _lesson_progress + 1;
                lesson = _courseSession.GetCourse().GetLesson(_lesson_progress);
                return lesson;
            }
            throw new ArgumentException("Last Lesson has not been submitted.");
        }

        return lesson;
        
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
    public int GetLessonProgress()
    {
        return _lesson_progress;
    }
    public void ViewLessonScores()
    {
        for (int i = 0; i < _lesson_progress; i++)
        {
           Console.WriteLine ($"Lesson {i + 1} ==== {_courseSession.GetCourse().GetLesson(i).CalculateLessonScore()}");
        }
    }
}