
public class Course :  LearningMaterial
{
    private string _courseId;
    private string _courseTitle;
    private List<Lesson> _lessons = new List<Lesson>();
    // private List<Lesson> _removedLesson = new List<Lesson>();
    private string _brief_Info;
    private GradingPolicy _gradingPolicy;
    private Exam _exam;
  
    public Course(string courseId,string info,string courseTitle,GradingPolicy gradingPolicy,Exam exam)
    {
        _courseId = courseId;
        _brief_Info = info; 
        _courseTitle = courseTitle;
        _gradingPolicy = gradingPolicy;
        _exam = exam;
    }
    public string GetTitle()
    {
        return _courseTitle;
    }
    public Lesson GetLesson(int Index)
    {
        if (Index >= LessonCount() || Index < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(Index),"Index out of range");
         }
        return _lessons[Index];
    }
    public void AddLesson(Lesson newLesson)
    {
        AddMaterial(_lessons,newLesson); 
    }
    public void RemoveLesson(int Index)
    {
        Lesson lesson = RemoveMaterial(Index,_lessons);
        // _removedLesson.Add(lesson);  
    }
    public void DisplayCourseInfo()
    {
        Console.WriteLine($"CourseId: {_courseId}");
        Console.WriteLine($"CourseId: {_courseTitle}");
        Console.WriteLine($"Course Outcome: {_brief_Info}");
    }
    public string GetId()
    {
        return _courseId;
    }
    public int LessonCount()
    {
        return _lessons.Count;
    }
    public void DisplayAllLessons()
    {
        foreach (Lesson item in _lessons)
        {
            item.DisplayLesson();
        }
    }
    private string ValidateInput(string param)
    {
        if (string.IsNullOrEmpty(param))
        {
            throw new ArgumentNullException(nameof(param),"Empty Input");
        }
        return param;
    }
    // public void ViewRemovedLessons()
    // {
    //     foreach (Lesson item in _removedLesson)
    //     {
    //         item.DisplayLesson();
    //     }
    // }
    public List<string> GetLessonNames()
    {
        List<string> values = new  List<string>();
        if (_lessons.Count  == 0)
        { 
            throw new Exception("No lesson in course.");
        }
        foreach (Lesson item in _lessons)
        {
            values.Add(item.GetTopic());
        }
        return values;
    }
   public Exam GetExam()
    {
        return _exam;
    }
    public GradingPolicy GetGradingPolicy()
    {
        if (_gradingPolicy.GetStatus() != CourseMaterialStatus._published)
        {
            throw new InvalidOperationException("Grading policy is not published.");
        }
        return _gradingPolicy;
    }
    protected override void ValidatePublish()
    {
       ValidateInput(_courseId);
       ValidateInput(_courseTitle);
       ValidateInput(_brief_Info);
       if (_lessons.Count == 0)
       {
            throw new Exception("Cannot publish a course without lessons.");
       }
       int num = 1;
       foreach (Lesson item in _lessons)
       {
            if (item.GetStatus() != CourseMaterialStatus._published)
            {
                throw new Exception($"Lesson {num} has not been published");
            }
       }
        if (_exam.GetStatus() != CourseMaterialStatus._published)
        {
            throw  new Exception("Exam has not been published.");
        }
        if (_gradingPolicy.GetStatus() != CourseMaterialStatus._published)
        {
            throw new Exception("Grading Policy is not published.");
        }
    }
    //
}
