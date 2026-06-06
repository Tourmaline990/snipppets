public class Course
{
    private string _courseId;
    private string _difficulty_level;
    private string _courseTitle;
    private List<Lesson> _lessons = new List<Lesson>();
    private List<Lesson> _removedLesson = new List<Lesson>();
    private string _brief_Info;
  
    public Course(string courseId,string level,string info,string courseTitle)
    {
        _courseId = ValidateInput(courseId);
        _difficulty_level = ValidateInput(level);
        _brief_Info = ValidateInput(info); 
        _courseTitle = ValidateInput(courseTitle);
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
        _lessons.Add(newLesson); 
    }
    public void RemoveLesson(int Index)
    {
        if (Index >= LessonCount() || Index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(Index),"Index out of range");
        }
        _removedLesson.Add(_lessons[Index]);
        _lessons.RemoveAt(Index);
    }
    public void DisplayCourseInfo()
    {
        Console.WriteLine($"CourseId: {_courseId}");
        Console.WriteLine($"CourseId: {_courseTitle}");
        Console.WriteLine($"Difficulty Level: {_difficulty_level}");
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
    public void ViewRemovedLessons()
    {
        foreach (Lesson item in _removedLesson)
        {
            item.DisplayLesson();
        }
    }
   ///
}
