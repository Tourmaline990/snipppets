public class Course
{
    private string _courseId;
    private string _difficulty_level;
    private List<Lesson> _lessons = new List<Lesson>();
    private string _brief_Info;
  
    public Course(string courseId,string level,string info)
    {
        _courseId = courseId;
        _difficulty_level = level;
        _brief_Info = info; 
    }
    public Lesson GetLesson(int Index)
    {
        if (Index >= LessonCount())
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
        if (Index >= LessonCount())
        {
            throw new ArgumentOutOfRangeException(nameof(Index),"Index out of range");
        }
        _lessons.RemoveAt(Index);
    }
    public void DisplayCourseInfo()
    {
        Console.WriteLine($"CourseId: {_courseId}");
        Console.WriteLine($"Difficulty Level: {_difficulty_level}");
        Console.WriteLine($"Course Outcome: {_brief_Info}");
        
    }
    public string GetId()
    {
        return _courseId;
    }
    public int LessonCount()
    {
        return _lessons.Count - 1;
    }
    public void DisplayAllLessons()
    {
        foreach (Lesson item in _lessons)
        {
            item.DisplayLesson();
        }
    }
   
}
